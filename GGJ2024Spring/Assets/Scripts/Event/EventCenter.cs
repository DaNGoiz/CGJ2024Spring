using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YSFramework
{
    /// <summary>
    /// 事件中心类，用于添加或移除监听逻辑实现
    /// </summary>
    public class EventCenter
    {
        #region 属性、字段

        private static Dictionary<EventCode, Delegate> _eventTable = new Dictionary<EventCode, Delegate>();

        #endregion

        #region 自定义方法（OnListenerAdding OnListenerRemoving Broadcast）

        private static void OnListenerAdding(EventCode eventType, Delegate callBack)
        {
            if (!_eventTable.ContainsKey(eventType))
            {
                _eventTable.Add(eventType, null);
            }
            Delegate d = _eventTable[eventType];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        private static void OnListenerRemoving(EventCode eventType, Delegate callBack)
        {
            if (_eventTable.ContainsKey(eventType))
            {
                Delegate d = _eventTable[eventType];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
            }
        }
        private static void OnListenerRemoved(EventCode eventType)
        {
            if (_eventTable[eventType] == null)
            {
                _eventTable.Remove(eventType);
            }
        }
        //no parameters
        public static void AddListener(EventCode eventType, CallBack callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack)_eventTable[eventType] + callBack;
        }
        //no parameters and have return value
        public static void AddListener_Return(EventCode eventType, CallBack_Return callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack_Return)_eventTable[eventType] + callBack;
        }
        //Single parameters
        public static void AddListener<T>(EventCode eventType, CallBack<T> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack<T>)_eventTable[eventType] + callBack;
        }

        //Single parameters and return value
        public static void AddListener_Return<T>(EventCode eventType, CallBack_Return<T> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack_Return<T>)_eventTable[eventType] + callBack;
        }

        //two parameters
        public static void AddListener<T, X>(EventCode eventType, CallBack<T, X> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X>)_eventTable[eventType] + callBack;
        }
        //two parameters and have return value
        public static void AddListener_Return<T, X>(EventCode eventType, CallBack_Return<T, X> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack_Return<T, X>)_eventTable[eventType] + callBack;
        }
        //three parameters
        public static void AddListener<T, X, Y>(EventCode eventType, CallBack<T, X, Y> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y>)_eventTable[eventType] + callBack;
        }
        //four parameters
        public static void AddListener<T, X, Y, Z>(EventCode eventType, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y, Z>)_eventTable[eventType] + callBack;
        }
        //five parameters
        public static void AddListener<T, X, Y, Z, W>(EventCode eventType, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y, Z, W>)_eventTable[eventType] + callBack;
        }
        //six parameters
        public static void AddListener<T, X, Y, Z, W, A>(EventCode eventType, CallBack<T, X, Y, Z, W, A> callBack)
        {
            OnListenerAdding(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y, Z, W, A>)_eventTable[eventType] + callBack;
        }

        //no parameters
        public static void RemoveListener(EventCode eventType, CallBack callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //no parameters and have return value
        public static void RemoveListener_Return(EventCode eventType, CallBack_Return callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack_Return)_eventTable[eventType] + callBack;
            OnListenerRemoved(eventType);
        }
        //single parameters
        public static void RemoveListener<T>(EventCode eventType, CallBack<T> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack<T>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }

        //single parameters and return value
        public static void RemoveListener_Return<T>(EventCode eventType, CallBack_Return<T> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack_Return<T>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //two parameters
        public static void RemoveListener<T, X>(EventCode eventType, CallBack<T, X> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //two parameters and have return value
        public static void RemoveListener_Return<T, X>(EventCode eventType, CallBack_Return<T, X> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack_Return<T, X>)_eventTable[eventType] + callBack;
            OnListenerRemoved(eventType);
        }
        //three parameters
        public static void RemoveListener<T, X, Y>(EventCode eventType, CallBack<T, X, Y> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //four parameters
        public static void RemoveListener<T, X, Y, Z>(EventCode eventType, CallBack<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y, Z>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //five parameters
        public static void RemoveListener<T, X, Y, Z, W>(EventCode eventType, CallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y, Z, W>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //six parameters
        public static void RemoveListener<T, X, Y, Z, W, A>(EventCode eventType, CallBack<T, X, Y, Z, W, A> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            _eventTable[eventType] = (CallBack<T, X, Y, Z, W, A>)_eventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }


        //no parameters
        public static void Broadcast(EventCode eventType)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack callBack = d as CallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //no parameters and have return value
        public static object Broadcast_Return(EventCode eventType)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack_Return callBack = d as CallBack_Return;
                if (callBack != null)
                {
                    return callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }

            }
            return null;
        }
        //single parameters
        public static void Broadcast<T>(EventCode eventType, T arg)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T> callBack = d as CallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //single parameters and have return value
        public static object Broadcast_Return<T>(EventCode eventType, T arg1)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack_Return<T> callBack = d as CallBack_Return<T>;
                if (callBack != null)
                {
                    return callBack(arg1);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
            return null;
        }
        //two parameters
        public static void Broadcast<T, X>(EventCode eventType, T arg1, X arg2)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T, X> callBack = d as CallBack<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //two parameters and have return value
        public static object Broadcast_Return<T, X>(EventCode eventType, T arg1, X arg2)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack_Return<T, X> callBack = d as CallBack_Return<T, X>;
                if (callBack != null)
                {
                    return callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }

            }
            return null;
        }
        //three parameters
        public static void Broadcast<T, X, Y>(EventCode eventType, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }

        //four parameters
        public static void Broadcast<T, X, Y, Z>(EventCode eventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //five parameters
        public static void Broadcast<T, X, Y, Z, W>(EventCode eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y, Z, W> callBack = d as CallBack<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //six parameters
        public static void Broadcast<T, X, Y, Z, W, A>(EventCode eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5, A arg6)
        {
            Delegate d;
            if (_eventTable.TryGetValue(eventType, out d))
            {
                CallBack<T, X, Y, Z, W, A> callBack = d as CallBack<T, X, Y, Z, W, A>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5, arg6);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }

        #endregion

    }
}