using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.IO.Ports;
using YSFramework;
namespace YSFramework
{
    /// <summary>
    /// ����ͨѶ�����࣬���ڶ�ͨѶ��ʽ��ͨѶ���̽��й���
    /// </summary>
    public class CommunicationManager : Sington<CommunicationManager>
    {

        /// <summary>
        /// ������ֹͣͨѶ����
        /// </summary>
        /// <param name="isRun">�Ƿ���ͨѶ</param>
        private async void RunOrStopCommunication(bool isRun)
        {
            if (isRun == false)
            {
                foreach (var item in GlobalData.CommunicationParaDict)
                {
                    string[] paras = item.Value.Split('|');
                    CommunicationType communicationType = (CommunicationType)Enum.Parse(typeof(CommunicationType), paras[0]);
                    if (!bool.Parse(paras[paras.Length - 1]))//�����ǰû�����ã������ٽ���
                    {
                        continue;
                    }
                    switch (communicationType)
                    {
                        case CommunicationType.Siemens:
                            ((SiemensNetThread)GlobalData.CommunicationProcessDict.TryGet(item.Key)).OnDisconnect();
                            print("�Ͽ�����");
                            break;

                        case CommunicationType.Serial:
                            //((SingleChip51NetThread)DataManager.CommunicationProcessDict.TryGet(item.Key)).OnDisconnect();
                            break;
                        case CommunicationType.Socket:
                            ((SocketClientNetThread)GlobalData.CommunicationProcessDict.TryGet(item.Key)).OnDisconnect();
                            break;
                        default:
                            break;
                    }
                }
                GlobalData.CommunicationProcessDict.Clear();
            }
            else
            {
                List<string> msgList = new List<string>();//��ʾ��Ϣ
                foreach (var item in GlobalData.CommunicationParaDict)
                {
                    string[] paras = item.Value.Split('|');
                    CommunicationType communicationType = (CommunicationType)Enum.Parse(typeof(CommunicationType), paras[0]);

                    if (!bool.Parse(paras[paras.Length - 1]))//�����ǰû�����ã��򲻴�������
                    {
                        continue;
                    }
                    switch (communicationType)
                    {
                        case CommunicationType.Siemens:
                            string ipAddress = paras[1];
                            string virsual = paras[2];
                            string type = paras[3];
                            SiemensNetThread thread = new SiemensNetThread(ipAddress, item.Key, type);
                            bool isconn = await thread.OnConnect();
                            GlobalData.CommunicationProcessDict.Add(item.Key, thread);
                            if (isconn)
                            {
                                print("���ӳɹ�");
                                msgList.Add("ͨѶ���̣�" + item.Key + " �������ӳɹ�");
                            }
                            else
                            {
                                print("����ʧ��");
                                msgList.Add("ͨѶ���̣�" + item.Key + " ��������ʧ��");
                            }
                            break;

                        case CommunicationType.Serial:
                            string portName = paras[1];
                            int baudRate = int.Parse(paras[2]);
                            int dataBits = int.Parse(paras[3]);
                            StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), paras[4]);
                            Parity parity = (Parity)Enum.Parse(typeof(Parity), paras[5]);
                            SingleChip51NetThread singleChip51thread = new SingleChip51NetThread(item.Key);
                            bool isconn1 = singleChip51thread.OnConnect(portName, baudRate, parity, dataBits, stopBits);
                            GlobalData.CommunicationProcessDict.Add(item.Key, singleChip51thread);
                            if (isconn1)
                            {
                                print("���ӳɹ�");
                                msgList.Add("ͨѶ���̣�" + item.Key + " �������ӳɹ�");
                            }
                            else
                            {
                                print("����ʧ��");
                                msgList.Add("ͨѶ���̣�" + item.Key + " ��������ʧ��");
                            }
                            break;
                        case CommunicationType.Socket:
                            string socketClientIpAddress = paras[1];
                            int port = int.Parse(paras[2]);
                            ProtocolType tcpOrUdp = (ProtocolType)Enum.Parse(typeof(ProtocolType), paras[3]);
                            SocketClientNetThread socketClientThead = new SocketClientNetThread(item.Key, tcpOrUdp);
                            bool isSocketClientConn = socketClientThead.OnConnect(socketClientIpAddress, port);
                            GlobalData.CommunicationProcessDict.Add(item.Key, socketClientThead);
                            if (isSocketClientConn)
                            {
                                print("���ӳɹ�");
                                msgList.Add("ͨѶ���̣�" + item.Key + " �������ӳɹ�");
                            }
                            else
                            {
                                print("����ʧ��");
                                msgList.Add("ͨѶ���̣�" + item.Key + " ��������ʧ��");
                            }
                            break;
                        default:
                            break;
                    }
                }

                //EventCenter.Broadcast<UIPanelType,object>(EventCode.PushPanel, UIPanelType.TipList,msgList);
            }
        }



    }
}