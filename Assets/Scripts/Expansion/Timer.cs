using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;
using static YSFramework.GlobalManager;

/// <summary>
/// ������ȫ��Manager�Ϲ����ļ�ʱ��
/// </summary>
public class Timer : MonoBehaviour
{
    private Dictionary<string, InnerTimer> commonTimers;
    private Dictionary<string, EventTimer> eventTimers;
    private List<string> removeList;
    /// <summary>
    /// ����һ�������ʱ��
    /// </summary>
    /// <param name="name">��ʱ��������</param>
    /// <returns>��ʱ����׼ȷ����</returns>
    public string CreateCommonTimer(string name)
    {
        do
        {
            name += UnityEngine.Random.Range(0, 100);
        } while (ContainCommonTimer(name));
        commonTimers.Add(name, new InnerTimer() { name = name });
        return name;
    }
    /// <summary>
    /// ����һ�����¼��ļ�ʱ������ʱ������ֵ���Զ������¼���Ĭ������´������ʱ���Զ����¿�ʼ��ʱ
    /// </summary>
    /// <param name="name">��ʱ������</param>
    /// <param name="threshold">�����¼�����ֵ</param>
    /// <param name="timerEvent">Ҫ�������¼�</param>
    /// <param name="eventArgs">���봥���¼��Ĳ���</param>
    /// <param name="isAutoReset">��ʱ�������Ƿ��Զ����¿�ʼ��ʱ</param>
    /// <returns>��ʱ����׼ȷ����</returns>
    public string CreateEventTimer(string name, float threshold, Action<object[]> timerEvent, object[] eventArgs, bool disposeSelf, bool isAutoReset = true)
    {
        do
        {
            name += UnityEngine.Random.Range(0, 100);
        } while (ContainEventTimer(name));
        eventTimers.Add(name, new EventTimer(threshold, timerEvent, eventArgs, disposeSelf, isAutoReset) { name = name });
        return name;
    }
    /// <summary>
    /// ���ټ�ʱ��
    /// </summary>
    /// <param name="name">��ʱ��������</param>
    public void RemoveTimer(string name)
    {
        if (commonTimers.ContainsKey(name))
            removeList.Add(name);
        else if (eventTimers.ContainsKey(name))
            removeList.Add(name);
        else
            Debug.LogError("Timer doesn't exist");
    }
    /// <summary>
    /// ��ʼ��ʱ������ѡ���ڼ�ʱǰ���ü�ʱ��
    /// </summary>
    /// <param name="name">��ʱ��������</param>
    /// <param name="reset">�Ƿ������ü�ʱ��</param>
    public void StartTimer(string name, bool reset = false)
    {
        if (commonTimers.ContainsKey(name))
            commonTimers[name].StartTimer(reset);
        else if (eventTimers.ContainsKey(name))
            eventTimers[name].StartTimer(reset);
    }
    public void StopTimer(string name)
    {
        if (commonTimers.ContainsKey(name))
            commonTimers[name].StopTimer();
        else if (eventTimers.ContainsKey(name))
            eventTimers[name].StopTimer();
    }
    public void ResetTimer(string name, bool startImmediately = false)
    {
        if (commonTimers.ContainsKey(name))
            commonTimers[name].ResetTimer(startImmediately);
        else if (eventTimers.ContainsKey(name))
            eventTimers[name].ResetTimer(startImmediately);
    }
    public float GetTime(string name)
    {
        if (commonTimers.ContainsKey(name))
            return commonTimers[name].RunningTime;
        else if (eventTimers.ContainsKey(name))
            return eventTimers[name].RunningTime;
        return -1f;
    }
    public bool ContainCommonTimer(string name) => commonTimers.ContainsKey(name);
    public bool ContainEventTimer(string name) => eventTimers.ContainsKey(name);
    private void Awake()
    {
        commonTimers = new Dictionary<string, InnerTimer>();
        eventTimers = new Dictionary<string, EventTimer>();
        removeList = new List<string>();
    }
    private void Update()
    {
        if (commonTimers != null)
            foreach (KeyValuePair<string, InnerTimer> timer in commonTimers)
            {
                if (timer.Value.isRunning)
                    timer.Value.RunningTime += Time.deltaTime;
            }
        if (eventTimers != null)
            foreach (KeyValuePair<string, EventTimer> timer in eventTimers)
            {
                if (timer.Value.isRunning)
                    timer.Value.RunningTime += Time.deltaTime;
            }
        if (removeList.Count > 0)
        {
            foreach (string name in removeList)
            {
                if (commonTimers.ContainsKey(name))
                    commonTimers.Remove(name);
                else
                    eventTimers.Remove(name);
            }
        }
    }
    private class InnerTimer
    {
        public string name;
        /// <summary>
        /// ��ʱ���Ƿ���������
        /// </summary>
        public bool isRunning = false;
        /// <summary>
        /// ��ʱ�����е�ʱ�䣨��ͣʱ����ʱ��
        /// </summary>
        public float RunningTime { get; set; }
        public void StartTimer(bool reset = false)
        {
            isRunning = true;
            if (reset)
                RunningTime = 0;
        }
        public void StopTimer()
        {
            isRunning = false;
        }
        public void ResetTimer(bool startImmediately = false)
        {
            RunningTime = 0;
            if (!startImmediately)
                isRunning = false;
        }
    }
    private class EventTimer 
    {
        public string name;
        /// <summary>
        /// ��ʱ���Ƿ���������
        /// </summary>
        public bool isRunning = false;
        private float runningTime;
        /// <summary>
        /// ��ʱ�����е�ʱ�䣨��ͣʱ����ʱ��
        /// </summary>
        public float RunningTime 
        {
            get
            {
                return runningTime;
            }
            set 
            { 
                runningTime = value;
                if (runningTime >= threshold)
                {
                    timerEvent.Invoke(timerArgs);
                    if (disposeSelf)
                    {
                        TimerInstance.RemoveTimer(name);
                        return;
                    }
                    if (isAutoStart)
                        ResetTimer(true);
                    else
                        StopTimer();
                }
            } 
        }
        public float threshold;
        private Action<object[]> timerEvent;

        private object[] timerArgs;
        private bool disposeSelf;
        private bool isAutoStart;
        public EventTimer(float threshold, Action<object[]> timerEvent, object[] eventArgs, bool disposeSelf, bool isAutoStart)
        {
            this.threshold = threshold;
            this.timerEvent = timerEvent;
            this.timerArgs = eventArgs;
            this.isAutoStart = isAutoStart;
        }
        public void StartTimer(bool reset = false)
        {
            isRunning = true;
            if (reset)
                RunningTime = 0;
        }
        public void StopTimer()
        {
            isRunning = false;
        }
        public void ResetTimer(bool startImmediately = false)
        {
            RunningTime = 0;
            if (!startImmediately)
                isRunning = false;
        }
        public void SetArgs(object[] eventArgs)
        {
            timerArgs = eventArgs;
        }
    }
}
