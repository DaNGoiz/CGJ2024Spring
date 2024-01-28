using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// 数据映射模型类
    /// </summary>
    [Serializable]
    public class DataMappingModel
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public int ID;
        /// <summary>
        /// 映射名称
        /// </summary>
        public string MappingName;
        /// <summary>
        /// 映射简介
        /// </summary>
        public string MappingInfor;
        /// <summary>
        /// 映射的通讯数据状态
        /// </summary>
        public CommunicationDataStatus Status;
        /// <summary>
        /// 映射的通讯数据类型
        /// </summary>
        public CommunicationDataType Type;
        /// <summary>
        /// 映射地址
        /// </summary>
        public string Address;
        /// <summary>
        /// 映射的数值
        /// </summary>
        public string Value;
        /// <summary>
        /// 映射所属的通讯名称
        /// </summary>
        public string CommunicationName;
        /// <summary>
        /// 映射附加属性
        /// </summary>
        public string AttachedProperty;

    }
}