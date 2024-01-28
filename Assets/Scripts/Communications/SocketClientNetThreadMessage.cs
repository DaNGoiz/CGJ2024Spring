using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using Common;
using System.Linq;
using LitJson;

namespace YSFramework
{
    /// <summary>
    /// Socket客户端传输数据处理类，用于处理粘包分包问题
    /// </summary>
    public class SocketClientNetThreadMessage
    {

        private byte[] data = new byte[1024];
        private int startIndex = 0;//我们存取了多少个字节的数据在数组里面
        public byte[] Data
        {
            get
            {
                return data;
            }
        }
        public int StartIndex
        {
            get
            {
                return startIndex;
            }
        }
        public int RemainSize
        {
            get
            {
                return data.Length - startIndex;
            }
        }


        /// <summary>
        /// 解析数据方法
        /// </summary>
        /// <param name="newDataAmount"></param>
        /// <param name="processDataCallback"></param>
        public void ReadMessage(int newDataAmount, Action<string> processDataCallback)
        {
            startIndex += newDataAmount;//每当有新的数据读取成功时进行加量
            while (true)//通过死循环来实现数据的读取
            {
                if (startIndex <= 4)
                {
                    return;
                }
                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)
                {
                    string s = Encoding.UTF8.GetString(data, 4, count);
                    processDataCallback(s);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= count + 4;
                }
                else
                {
                    break;
                }
            }

        }
        /// <summary>
        /// 打包数据方法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] PackData(DataMappingModel data)
        {
            string dataStr = JsonMapper.ToJson(data);
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataStr);
            int dataAmount = dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            byte[] newBytes = dataAmountBytes
                .Concat(dataBytes).ToArray();

            return newBytes;
        }
    }
}