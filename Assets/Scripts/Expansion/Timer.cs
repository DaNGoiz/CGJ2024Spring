using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂载在全局Manager上工作的计时器
/// </summary>
public class Timer : MonoBehaviour
{
    private Dictionary<string, InnerTimer> timers;
    /// <summary>
    /// 创建一个计时器
    /// </summary>
    /// <param name="name">计时器的名字</param>
    /// <returns>是否创建成功</returns>
    public bool CreateTimer(string name)
    {
        if (ContainTimer(name))
            return false;
        timers.Add(name, new InnerTimer());
        return true;
    }
    /// <summary>
    /// 销毁计时器
    /// </summary>
    /// <param name="name">计时器的名字</param>
    public void DestroyTimer(string name)
    {
        if (timers.ContainsKey(name))
            timers.Remove(name);
    }
    /// <summary>
    /// 开始计时，可以选择在计时前重置计时器
    /// </summary>
    /// <param name="name">计时器的名字</param>
    /// <param name="reset">是否先重置计时器</param>
    public void StartTimer(string name, bool reset = false)
    {
        if (timers.ContainsKey(name))
            timers[name].StartTimer(reset);
    }
    public void StopTimer(string name)
    {
        if (timers.ContainsKey(name))
            timers[name].StopTimer();
    }
    public void ResetTimer(string name, bool startImmediately = false)
    {
        if (timers.ContainsKey(name))
            timers[name].ResetTimer(startImmediately);
    }
    public float GetTime(string name)
    {
        if (timers.ContainsKey(name))
            return timers[name].RunningTime;
        return -1f;
    }
    public bool ContainTimer(string name) => timers.ContainsKey(name);
    private void Awake()
    {
        timers = new Dictionary<string, InnerTimer>();
    }
    private void Update()
    {
        if (timers != null)
            foreach (KeyValuePair<string, InnerTimer> timer in timers)
            {
                if (timer.Value.isRunning)
                    timer.Value.RunningTime += Time.deltaTime;
            }
    }

    private class InnerTimer
    {
        public string name;
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
}
