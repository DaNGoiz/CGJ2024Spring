using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace YSFramework
{
    /// <summary>
    /// UI�л��۽��࣬����ͨ��Tab���л���ǰ�۽���UI����
    /// </summary>
    public class UISelectSwtitch : MonoBehaviour
    {
        /// <summary>
        /// ���л��۽���UI�б�
        /// </summary>
        [SerializeField]
        private List<Selectable> uiList = new List<Selectable>();
        /// <summary>
        /// UI�¼�
        /// </summary>
        private EventSystem eventSystem;
        /// <summary>
        /// ��ǰ�ľ۽����
        /// </summary>
        private int index = 0;
        /// <summary>
        /// ��ʼ�����������Զ���ȡUI�¼����Զ���ȡ���пɽ���UI
        /// </summary>
        void Start()
        {
            eventSystem = EventSystem.current;
            ////�����˻�����򽹵�
            //eventSystem.SetSelectedGameObject(inputFieldList[index].gameObject, new BaseEventData(eventSystem));
            StartCoroutine(OnInit());
        }
        /// <summary>
        /// ��ʼ����������������ݣ����Զ���ȡ�ɽ���UI
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnInit()
        {
            yield return null;
            uiList.Clear();
            FindChild(transform);
        }
        /// <summary>
        /// ���ҿɽ�����UI
        /// </summary>
        /// <param name="trans"></param>
        void FindChild(Transform trans)
        {
            if (trans.GetComponent<Selectable>() != null)
            {
                uiList.Add(trans.GetComponent<Selectable>());
            }
            //����forѭ�� ��ȡ�����µ�ȫ��������
            for (int i = 0; i < trans.childCount; i++)
            {
                FindChild(trans.GetChild(i));
            }
        }


        /// <summary>
        /// ���·���������Tab �����л�UI�����˳�� 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && eventSystem.currentSelectedGameObject != null && uiList.Contains(eventSystem.currentSelectedGameObject.GetComponent<Selectable>()))
            {
                index = uiList.IndexOf(eventSystem.currentSelectedGameObject.GetComponent<Selectable>());
                index++;
                if (index >= uiList.Count) index = 0;
                if (uiList[index] != null)
                    eventSystem.SetSelectedGameObject(uiList[index].gameObject, new BaseEventData(eventSystem));
            }
        }

    }
}