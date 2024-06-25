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
    /// 数据通讯管理类，用于对通讯方式和通讯进程进行管理
    /// </summary>
    public class CommunicationManager : Sington<CommunicationManager>
    {

        /// <summary>
        /// 开启或停止通讯进程
        /// </summary>
        /// <param name="isRun">是否开启通讯</param>
        private async void RunOrStopCommunication(bool isRun)
        {
            if (isRun == false)
            {
                foreach (var item in GlobalData.CommunicationParaDict)
                {
                    string[] paras = item.Value.Split('|');
                    CommunicationType communicationType = (CommunicationType)Enum.Parse(typeof(CommunicationType), paras[0]);
                    if (!bool.Parse(paras[paras.Length - 1]))//如果当前没有启用，则不销毁进程
                    {
                        continue;
                    }
                    switch (communicationType)
                    {
                        case CommunicationType.Siemens:
                            ((SiemensNetThread)GlobalData.CommunicationProcessDict.TryGet(item.Key)).OnDisconnect();
                            print("断开链接");
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
                List<string> msgList = new List<string>();//提示信息
                foreach (var item in GlobalData.CommunicationParaDict)
                {
                    string[] paras = item.Value.Split('|');
                    CommunicationType communicationType = (CommunicationType)Enum.Parse(typeof(CommunicationType), paras[0]);

                    if (!bool.Parse(paras[paras.Length - 1]))//如果当前没有启用，则不创建进程
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
                                print("连接成功");
                                msgList.Add("通讯进程：" + item.Key + " 运行连接成功");
                            }
                            else
                            {
                                print("连接失败");
                                msgList.Add("通讯进程：" + item.Key + " 运行连接失败");
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
                                print("连接成功");
                                msgList.Add("通讯进程：" + item.Key + " 运行连接成功");
                            }
                            else
                            {
                                print("连接失败");
                                msgList.Add("通讯进程：" + item.Key + " 运行连接失败");
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
                                print("连接成功");
                                msgList.Add("通讯进程：" + item.Key + " 运行连接成功");
                            }
                            else
                            {
                                print("连接失败");
                                msgList.Add("通讯进程：" + item.Key + " 运行连接失败");
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