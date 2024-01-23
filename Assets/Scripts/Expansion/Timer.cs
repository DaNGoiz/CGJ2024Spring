using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ȫ��Manager�Ϲ����ļ�ʱ��
/// </summary>
public class Timer : MonoBehaviour
{
    private Dictionary<string, InnerTimer> timers;
    /// <summary>
    /// ����һ����ʱ��
    /// </summary>
    /// <param name="name">��ʱ��������</param>
    /// <returns>�Ƿ񴴽��ɹ�</returns>
    public bool CreateTimer(string name)
    {
        if (ContainTimer(name))
            return false;
        timers.Add(name, new InnerTimer());
        return true;
    }
    /// <summary>
    /// ���ټ�ʱ��
    /// </summary>
    /// <param name="name">��ʱ��������</param>
    public void DestroyTimer(string name)
    {
        if (timers.ContainsKey(name))
            timers.Remove(name);
    }
    /// <summary>
    /// ��ʼ��ʱ������ѡ���ڼ�ʱǰ���ü�ʱ��
    /// </summary>
    /// <param name="name">��ʱ��������</param>
    /// <param name="reset">�Ƿ������ü�ʱ��</param>
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
}
