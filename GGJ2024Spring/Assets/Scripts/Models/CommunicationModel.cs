using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// 通讯模型类
    /// </summary>
    public class CommunicationModel
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 在场景中的唯一ID
        /// </summary>
        public int SceneID { get; set; }
        /// <summary>
        /// 通讯名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 通讯参数
        /// </summary>
        public string Parameter { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationModel() { }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sceneid"></param>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        public CommunicationModel(int id, int sceneid, string name, string parameter)
        {
            this.ID = id;
            this.SceneID = sceneid;
            this.Name = name;
            this.Parameter = parameter;
        }
    }
}