using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using S7.Net;
using System.Threading.Tasks;
namespace YSFramework
{
    /// <summary>
    /// 西门子通讯类
    /// </summary>
    public class SiemensNetThread
    {
        #region 属性和字段
        /// <summary>
        /// 通讯速率，ms
        /// </summary>
        private int timeSleep = 8;
        /// <summary>
        /// 读取线程
        /// </summary>
        private Thread threadRead = null;
        /// <summary>
        /// 写线程
        /// </summary>
        private Thread threadWrite = null;
        /// <summary>
        /// 是否使用线程
        /// </summary>
        private bool isTread;
        /// <summary>
        /// 读线程是否运行中
        /// </summary>
        private bool isThreadReadRun;
        /// <summary>
        /// 写线程是否运行中
        /// </summary>
        private bool isThreadWriteRun;
        /// <summary>
        /// 西门子通讯对象
        /// </summary>
        private Plc siemensTcpNet;
        /// <summary>
        /// 是否建立了通讯连接
        /// </summary>
        private bool isConnect;
        /// <summary>
        /// 写数据列表
        /// </summary>
        private List<DataMappingModel> writeList = new List<DataMappingModel>();//保存需要写入的数据列表
        /// <summary>
        /// 读数据列表
        /// </summary>
        private List<DataMappingModel> readList = new List<DataMappingModel>();
        /// <summary>
        /// 关联通讯名称
        /// </summary>
        private string communicationName;

        /// <summary>
        /// 通讯IP地址
        /// </summary>
        private string ipAddress;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="communicationName"></param>
        /// <param name="plcModelType"></param>
        public SiemensNetThread(string ipAddress, string communicationName, string plcModelType)
        {
            EventCenter.AddListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);
            EventCenter.AddListener<List<DataMappingModel>>(EventCode.SendDataMappingList, SendDataMappingList);
            try
            {
                this.ipAddress = ipAddress;
                this.communicationName = communicationName;
                if (plcModelType.Equals("0"))//Siemens S7-1200
                {
                    siemensTcpNet = new Plc(CpuType.S71200, ipAddress, 0, 1);
                }
                else if (plcModelType.Equals("1"))//Siemens S7-1500
                {
                    siemensTcpNet = new Plc(CpuType.S71500, ipAddress, 0, 1);
                }
                else if (plcModelType.Equals("Siemens S7-300"))
                {
                    siemensTcpNet = new Plc(CpuType.S7300, ipAddress, 0, 1);
                }
                else if (plcModelType.Equals("Siemens S7-200"))
                {
                    siemensTcpNet = new Plc(CpuType.S7200, ipAddress, 0, 1);
                }
                else if (plcModelType.Equals("Siemens S7-400"))
                {
                    siemensTcpNet = new Plc(CpuType.S7400, ipAddress, 0, 1);

                }
                threadRead = new Thread(ThreadReadMethod);
                threadWrite = new Thread(ThreadWriteMethod);

            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        /// <summary>
        /// 添加映射数据方法，将映射添加到数据读取列表中
        /// </summary>
        /// <param name="list"></param>
        private void SendDataMappingList(List<DataMappingModel> list)
        {
            foreach (var io in list)
            {
                if (io.Status.Equals(CommunicationDataStatus.Read) && !string.IsNullOrEmpty(io.Address) && io.CommunicationName.Equals(communicationName))
                {
                    readList.Add(io);
                }
            }
        }
        /// <summary>
        /// 析构函数，断开连接并移除事件监听
        /// </summary>
        ~SiemensNetThread()
        {
            OnDisconnect();
            EventCenter.RemoveListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);
            EventCenter.RemoveListener<List<DataMappingModel>>(EventCode.SendDataMappingList, SendDataMappingList);

        }

        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <returns></returns>
        public async Task<bool> OnConnect()
        {
            try
            {
                // 连接
                IPAddress address;
                if (!IPAddress.TryParse(ipAddress, out address))
                    return false;
                await siemensTcpNet.OpenAsync();

                if (siemensTcpNet.IsConnected)
                {
                    //InitPlcList();
                    isConnect = true;
                    if (!isTread)
                    {
                        isThreadReadRun = true;
                        isThreadWriteRun = true;
                        threadRead.Start();
                        threadWrite.Start();
                        isTread = true;
                    }
                    return isConnect;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }

        }

        /// <summary>
        /// 断开长连接
        /// </summary>
        public void OnDisconnect()
        {
            try
            {
                isConnect = false;
                isThreadReadRun = false;
                isThreadWriteRun = false;
                siemensTcpNet.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        /// <summary>
        /// 将映射数据写入西门子PLC中
        /// </summary>
        /// <param name="model"></param>
        private void CommunicationWrite(DataMappingModel model)
        {
            if (!this.communicationName.Equals(model.CommunicationName)) return;

            writeList.Add(model);

        }


        /// <summary>
        /// 后台读数据
        /// </summary>
        private void ThreadReadMethod()
        {

            while (isThreadReadRun)
            {
                Thread.Sleep(timeSleep);
                try
                {
                    if (isConnect)
                        for (int i = 0; i < readList.Count; i++)
                        {
                            object result = siemensTcpNet.Read(readList[i].Address);
                            Debug.Log(readList[i].Address + "==" + result);
                            switch (readList[i].Type)
                            {
                                case CommunicationDataType.Bool:
                                    readList[i].Value = Convert.ToBoolean(result).ToString();
                                    break;
                                case CommunicationDataType.Byte:
                                    readList[i].Value = Convert.ToByte(result).ToString();
                                    break;
                                case CommunicationDataType.Short:
                                    readList[i].Value = Convert.ToInt16(result).ToString();
                                    break;
                                case CommunicationDataType.Int:
                                    readList[i].Value = Convert.ToInt32(result).ToString();
                                    break;
                                case CommunicationDataType.Float:
                                    readList[i].Value = Convert.ToSingle(result).ToString();
                                    break;
                                case CommunicationDataType.String:
                                    readList[i].Value = Convert.ToString(result).ToString();
                                    break;
                                default:
                                    break;
                            }
                            EventCenter.Broadcast<DataMappingModel>(EventCode.ReadCommunication, readList[i]);
                        }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        /// <summary>
        /// 后台写数据
        /// </summary>
        private void ThreadWriteMethod()
        {
            while (isThreadWriteRun)
            {
                Thread.Sleep(timeSleep);
                try
                {
                    if (isConnect)
                    {
                        DataMappingModel[] writeIOs = writeList.ToArray();
                        for (int i = 0; i < writeIOs.Length; i++)
                        {
                            if (writeIOs.Length > 0)
                            {
                                switch (writeList[i].Type)
                                {
                                    case CommunicationDataType.Bool:
                                        siemensTcpNet.Write(writeIOs[i].Address, bool.Parse(writeIOs[i].Value));
                                        break;
                                    case CommunicationDataType.Byte:
                                        siemensTcpNet.Write(writeIOs[i].Address, byte.Parse(writeIOs[i].Value));
                                        break;
                                    case CommunicationDataType.Short:
                                        siemensTcpNet.Write(writeIOs[i].Address, short.Parse(writeIOs[i].Value));
                                        break;
                                    case CommunicationDataType.Int:
                                        siemensTcpNet.Write(writeIOs[i].Address, int.Parse(writeIOs[i].Value));
                                        break;
                                    case CommunicationDataType.Float:
                                        siemensTcpNet.Write(writeIOs[i].Address, float.Parse(writeIOs[i].Value));
                                        break;
                                    case CommunicationDataType.String:
                                        siemensTcpNet.Write(writeIOs[i].Address, writeIOs[i].Value);
                                        break;
                                    default:
                                        break;
                                }
                                writeList.Remove(writeIOs[i]);
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

            }
        }


    }
}