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
    /// SocketͨѶ��
    /// </summary>
    public class SocketClientNetThread
    {
        //public const string IP = "47.103.201.6";//"127.0.0.1";"192.168.43.98";
        //private const int PORT = 1997;
        private Socket clientSocket;
        private SocketClientNetThreadMessage msg = new SocketClientNetThreadMessage();

        private Thread threadRead = null;
        private Thread threadWrite = null;
#pragma warning disable IDE0052 // ɾ��δ����˽�г�Ա
        private bool isThreadReadRun;
#pragma warning restore IDE0052 // ɾ��δ����˽�г�Ա
        private bool isThreadWriteRun;
        private string communicationName;
        private List<DataMappingModel> writeList = new List<DataMappingModel>();//������Ҫд��������б�
        private bool isConnect;
        private int timeSleep = 10;
        /// <summary>
        /// ���캯������ʼ������
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
        /// �����������Ͽ����Ӳ��Ƴ��¼�����
        /// </summary>
        ~SocketClientNetThread()
        {
            EventCenter.RemoveListener<DataMappingModel>(EventCode.WriteCommunication, CommunicationWrite);
            OnDisconnect();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool OnConnect(string ipAddress, int port)
        {
            try
            {
                clientSocket.Connect(ipAddress, port);//��������
                if (clientSocket.Connected)
                {
                    //StartReceive();
                    isConnect = true;
                    isThreadReadRun = true;
                    isThreadWriteRun = true;
                    threadRead.Start();
                    threadWrite.Start();
                    Debug.Log("socket client���ӳɹ�");
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
        /// ��ʼ�첽��������
        /// </summary>
        private void StartReceive()
        {
            if (clientSocket == null || clientSocket.Connected == false)
                return;
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }
        /// <summary>
        /// ͨѶд��������ӳ������д�뵽�ӿ�
        /// </summary>
        /// <param name="model"></param>
        private void CommunicationWrite(DataMappingModel model)
        {
            if (!this.communicationName.Equals(model.CommunicationName)) return;
            writeList.Add(model);
        }
        /// <summary>
        /// ��̨�߳�д����
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
        /// �������ݻص�����
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
                StartReceive();//���������������˵����ݵķ���
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        /// <summary>
        /// ���ݴ���ص�����
        /// </summary>
        /// <param name="data"></param>
        private void OnProcessDataCallBack(string data)
        {
            DataMappingModel ioModel = JsonMapper.ToObject<DataMappingModel>(data);//jsonת����
            DataMappingModel io = new DataMappingModel();
            io.Address = ioModel.Address;
            io.Type = ioModel.Type;
            io.Value = ioModel.Value;
            io.Status = CommunicationDataStatus.Read;
            io.CommunicationName = this.communicationName;
            EventCenter.Broadcast<DataMappingModel>(EventCode.ReadCommunication, io);
        }

        /// <summary>
        /// ���������������
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
        /// �Ͽ�����
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