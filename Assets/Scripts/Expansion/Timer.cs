using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

/// <summary>
/// 挂载在全局Manager上工作的计时器
/// </summary>
public class Timer : MonoBehaviour
{
    private Dictionary<string, InnerTimer> commonTimers;
    private Dictionary<string, EventTimer> eventTimers;
    private List<string> removeList;
    /// <summary>
    /// 创建一个常规计时器
    /// </summary>
    /// <param name="name">计时器的名字</param>
    /// <returns>是否创建成功</returns>
    public bool CreateCommonTimer(string name)
    {
        if (ContainCommonTimer(name))
            return false;
        commonTimers.Add(name, new InnerTimer());
        return true;
    }
    /// <summary>
    /// 创建一个带事件的计时器，计时超过阈值会自动触发事件。默认情况下触发后计时器自动重新开始计时
    /// </summary>
    /// <param name="name">计时器名字</param>
    /// <param name="threshold">触发事件的阈值</param>
    /// <param name="timerEvent">要触发的事件</param>
    /// <param name="eventArgs">传入触发事件的参数</param>
    /// <param name="isAutoReset">计时结束后是否自动重新开始计时</param>
    /// <returns></returns>
    public bool CreateEventTimer(string name, float threshold, Action<object[]> timerEvent, object[] eventArgs, bool isAutoReset = true)
    {
        if (ContainEventTimer(name))
            return false;
        eventTimers.Add(name, new EventTimer(threshold, timerEvent, eventArgs, isAutoReset));
        return true;
    }
    /// <summary>
    /// 销毁计时器
    /// </summary>
    /// <param name="name">计时器的名字</param>
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
    /// 开始计时，可以选择在计时前重置计时器
    /// </summary>
    /// <param name="name">计时器的名字</param>
    /// <param name="reset">是否先重置计时器</param>
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
        /// <summary>
        /// 计时器是否正在运行
        /// </summary>
        public bool isRunning = false;
        /// <summary>
        /// 计时器运行的时间（暂停时不计时）
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
        /// <summary>
        /// 计时器是否正在运行
        /// </summary>
        public bool isRunning = false;
        private float runningTime;
        /// <summary>
        /// 计时器运行的时间（暂停时不计时）
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
        private bool isAutoStart;
        public EventTimer(float threshold, Action<object[]> timerEvent, object[] eventArgs, bool isAutoStart)
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
