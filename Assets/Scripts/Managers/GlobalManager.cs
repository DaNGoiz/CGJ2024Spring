
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
namespace YSFramework
{
    /// <summary>
    /// 项目全局管理类，处理项目中跨场景的全局管理
    /// </summary>
    public class GlobalManager : Sington<GlobalManager>
    {

        /// <summary>
        /// 在项目开始时完成所有管理类的生成和绑定，并确保在每个场景中只有一个GlobalManager类
        /// </summary>
        protected override void Awake()
        {

            if (GameObject.FindObjectsOfType<GlobalManager>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.AddComponent<LoadManager>();
                gameObject.AddComponent<UIManager>();
                gameObject.AddComponent<CommunicationManager>();
                gameObject.AddComponent<SqlManager>();
                gameObject.AddComponent<GameManager>();
                DontDestroyOnLoad(this.gameObject);
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            }
            base.Awake();
        }

        /// <summary>
        /// 脚本卸载时调用，将添加的场景载入监听事件移除
        /// </summary>
        protected override void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            base.OnDestroy();
        }
        /// <summary>
        /// 用于在场景载入时调用，每次加载完场景都会调用此方法
        /// </summary>
        /// <param name="arg0">载入的场景对象</param>
        /// <param name="arg1">载入场景模式</param>
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name.Equals("Main"))
            {
                
                //GameObject go= (GameObject)EventCenter.Broadcast_Return<ResourceType, string>(EventCode.LoadResource, ResourceType.Model, "TestModel");
                //go.transform.position = Vector3.zero;
                //print(go);
            }
            if (arg0.name.Equals("Menu"))
            {

                //GameObject go = (GameObject)EventCenter.Broadcast_Return<ResourceType, string>(EventCode.LoadResource, ResourceType.Model, "TestModel");
                //EventCenter.Broadcast<UIPanelType, object>(EventCode.PushPanel, UIPanelType.Start, null);
                //go.transform.position = Vector3.zero;
                //print(go);
            }
        }

    }
}