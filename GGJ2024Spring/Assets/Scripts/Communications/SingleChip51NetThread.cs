using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    ///  串口通讯类
    /// </summary>
    public class SingleChip51NetThread
    {
        #region 属性和字段
        byte[] data = new byte[128];//用于存储128位数据
        byte[] buffer = new byte[128];
        
        private SerialPort sp = null;
        private Thread threadRead = null;
        private Thread threadWrite = null;
        private bool isThreadReadRun;
        private bool isThreadWriteRun;
        private string communicationName;
        private List<DataMappingModel> writeList = new List<DataMappingModel>();//保存需要写入的数据列表
        private bool isConnect;
        private int timeSleep = 10;
        int readDataIndex = 0;//用于记录读取的数据次序



        #endregion


        /// <summary>
        /// 构造函数，用于数据初始化
        /// </summary>
        /// <param name="communicationName"></param>
        public SingleChip51NetThread(string communicationName)
        {
            this.communicationName = communicationName;
            threadRead = new Thread(ThreadReadServer);
            threadWrite = new Thread(ThreadWriteServer);
            EventCenter.AddListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);

        }
       /// <summary>
       /// 写通讯方法，将映射数据写入到串口
       /// </summary>
       /// <param name="model"></param>
        private void CommunicationWrite(DataMappingModel model)
        {
            if (!this.communicationName.Equals(model.CommunicationName)) return;
            writeList.Add(model);
        }
        /// <summary>
        /// 后台线程写数据
        /// </summary>
        private void ThreadWriteServer()
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
                                //Debug.Log(writeIOs[i].Address + "&" + writeIOList[i].Type + "&" + writeIOs[i].Value + "|");
                                WriteData(writeIOs[i].Address + "&" + writeList[i].Type + "&" + writeIOs[i].Value + "|");
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
        /// <summary>
        /// 析构函数，用于断开连接并移除事件监听
        /// </summary>
        ~SingleChip51NetThread()
        {
            EventCenter.RemoveListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);
            OnDisconnect();
        }
        /// <summary>
        /// 连接串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public bool OnConnect(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            sp.ReadTimeout = 400;
            try
            {
                sp.Open();

                if (sp.IsOpen)
                {
                    isConnect = true;
                    isThreadReadRun = true;
                    isThreadWriteRun = true;
                    threadRead.Start();
                    threadWrite.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void OnDisconnect()
        {
            try
            {
                isThreadReadRun = false;
                isThreadWriteRun = false;
                isConnect = false;
                if (sp != null && sp.IsOpen == true)
                    sp.Close();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        /// <summary>
        /// 写数据方法
        /// </summary>
        /// <param name="dataStr"></param>
        private void WriteData(string dataStr)
        {
            if (sp.IsOpen && sp != null)
            {
                sp.Write(dataStr);
            }
        }

        /// <summary>
        /// 后台线程读数据
        /// </summary>
        void ThreadReadServer()
        {
            int bytes = 0;
            //int flag0 = 0xFF;
            //int flag1 = 0xAA;
            while (isThreadReadRun)
            {
                Thread.Sleep(timeSleep);
                if (sp != null && sp.IsOpen)
                {
                    try
                    {
                        buffer = new byte[sp.BytesToRead];
                        bytes = sp.Read(buffer, 0, buffer.Length);
                    }
                    catch (Exception e)
                    {
                        //Debug.Log(e);
                    }
                    for (int i = 0; i < bytes; i++)
                    {
                        //if (buffer[i] == flag0 || buffer[i] == flag1)
                        //{
                        //    data = new byte[128];
                        //    readDataIndex = 0;//次序归0 
                        //    continue;
                        //}
                        //else
                        //{
                        if (readDataIndex >= data.Length) readDataIndex = data.Length - 1;//理论上不应该会进入此判断，但是由于传输的误码，导致数据的丢失，使得标志位与数据个数出错
                        data[readDataIndex] = buffer[i];//将数据存入data中
                                                        //Debug.Log(ReadData());
                        string str = Encoding.UTF8.GetString(data);
                        if (str.Contains("|"))
                        {
                            str = str.Remove(str.IndexOf('|'));
                            string[] splitStr = str.Split('&');
                            DataMappingModel io = new DataMappingModel();
                            io.Address = splitStr[0];
                            io.Type = (CommunicationDataType)Enum.Parse(typeof(CommunicationDataType), splitStr[1]);
                            io.Value = splitStr[2];
                            io.Status = CommunicationDataStatus.Read;
                            io.CommunicationName = this.communicationName;
                            EventCenter.Broadcast<DataMappingModel>(EventCode.ReadCommunication, io);
                            //Debug.Log(str);
                            data = new byte[128];
                            readDataIndex = 0;
                        }
                        else
                        {
                            readDataIndex++;
                        }
                        //}
                    }
                }
            }
        }
        /// <summary>
        /// 程序退出时调用，断开连接
        /// </summary>
        void OnApplicationQuit()
        {
            OnDisconnect();
        }


    }
}