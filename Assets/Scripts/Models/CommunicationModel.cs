using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// ͨѶģ����
    /// </summary>
    public class CommunicationModel
    {
        /// <summary>
        /// Ψһ����
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// �ڳ����е�ΨһID
        /// </summary>
        public int SceneID { get; set; }
        /// <summary>
        /// ͨѶ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ͨѶ����
        /// </summary>
        public string Parameter { get; set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        public CommunicationModel() { }
        /// <summary>
        /// �������Ĺ��캯��
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