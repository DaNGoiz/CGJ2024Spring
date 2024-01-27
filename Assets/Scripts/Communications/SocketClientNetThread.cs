using Common;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// Socket通讯类
    /// </summary>
    public class SocketClientNetThread
    {
        //public const string IP = "47.103.201.6";//"127.0.0.1";"192.168.43.98";
        //private const int PORT = 1997;
        private Socket clientSocket;
        private SocketClientNetThreadMessage msg = new SocketClientNetThreadMessage();

        private Thread threadRead = null;
        private Thread threadWrite = null;
#pragma warning disable IDE0052 // 删除未读的私有成员
        private bool isThreadReadRun;
#pragma warning restore IDE0052 // 删除未读的私有成员
        private bool isThreadWriteRun;
        private string communicationName;
        private List<DataMappingModel> writeList = new List<DataMappingModel>();//保存需要写入的数据列表
        private bool isConnect;
        private int timeSleep = 10;
        /// <summary>
        /// 构造函数，初始化数据
        /// </summary>
        /// <param name="communicationName"></param>
        /// <param name="protocolType"></param>
        public SocketClientNetThread(string communicationName, ProtocolType protocolType)
        {
            this.communicationName = communicationName;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType);

            threadRead = new Thread(StartReceive);
            threadWrite = new Thread(ThreadWriteServer);
            EventCenter.AddListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);
        }
        /// <summary>
        /// 析构函数，断开连接并移除事件监听
        /// </summary>
        ~SocketClientNetThread()
        {
            EventCenter.RemoveListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);
            OnDisconnect();
        }
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool OnConnect(string ipAddress, int port)
        {
            try
            {
                clientSocket.Connect(ipAddress, port);//建立连接
                if (clientSocket.Connected)
                {
                    //StartReceive();
                    isConnect = true;
                    isThreadReadRun = true;
                    isThreadWriteRun = true;
                    threadRead.Start();
                    threadWrite.Start();
                    Debug.Log("socket client连接成功");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            return false;
        }
        /// <summary>
        /// 开始异步接收数据
        /// </summary>
        private void StartReceive()
        {
            if (clientSocket == null || clientSocket.Connected == false)
                return;
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }
        /// <summary>
        /// 通讯写方法，将映射数据写入到接口
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
                                SendRequest(writeIOs[i]);
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
        /// 接收数据回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false)
                    return;
                int count = clientSocket.EndReceive(ar);
                msg.ReadMessage(count, OnProcessDataCallBack);
                StartReceive();//继续监听服务器端的数据的发送
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        /// <summary>
        /// 数据处理回调函数
        /// </summary>
        /// <param name="data"></param>
        private void OnProcessDataCallBack(string data)
        {
            DataMappingModel ioModel = JsonMapper.ToObject<DataMappingModel>(data);//json转对象
            DataMappingModel io = new DataMappingModel();
            io.Address = ioModel.Address;
            io.Type = ioModel.Type;
            io.Value = ioModel.Value;
            io.Status = CommunicationDataStatus.Read;
            io.CommunicationName = this.communicationName;
            EventCenter.Broadcast<DataMappingModel>(EventCode.ReadCommunication, io);
        }

        /// <summary>
        /// 向服务器发送请求
        /// </summary>
        /// <param name="data"></param>
        public void SendRequest(DataMappingModel data)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false)
                {
                    return;
                }
                byte[] bytes = SocketClientNetThreadMessage.PackData(data);
                clientSocket.Send(bytes);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }


        /// <summary>
        /// 断开连接
        /// </summary>
        public void OnDisconnect()
        {
            isConnect = false;
            isThreadReadRun = false;
            isThreadWriteRun = false;
            try
            {
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }


    }
}