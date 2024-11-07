using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager<E> where E : Enum
{
    public delegate void Callback();
    public delegate void Callback<T>(T t);
    public delegate void Callback<T,T1>(T t,T1 t1);
    public delegate void Callback<T,T1,T2>(T t,T1 t1,T2 t2);
    public delegate void Callback<T,T1,T2,T3>(T t,T1 t1,T2 t2,T3 t3);
    
    private class EventReceiver
    {
        public Delegate listener;
    }

    private Dictionary<E, List<EventReceiver>> _dicEvent = new Dictionary<E, List<EventReceiver>>();
    
    
    
    
    private void AddEvent(E eventId, Delegate listener)
    {
        EventReceiver receiver = new EventReceiver();
        receiver.listener = listener;
        
        
        _dicEvent.TryGetValue(eventId, out List<EventReceiver> list);
        

        if (list != null)
        {
            list.Add(receiver);
        }
        else
        {
            _dicEvent[eventId] = new List<EventReceiver>() { receiver };
        }
    }
    
    //______________________________________________________________
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="eventId">事件类型（枚举）</param>
    /// <param name="listener">监听事件</param>
    public void AddListener(E eventId, Callback listener)
    {
        AddEvent(eventId, listener);
    }
    
    public void AddListener<T>(E eventId, Callback<T> listener)
    {
        AddEvent(eventId, listener);
    }
    
    public void AddListener<T,T1>(E eventId, Callback<T,T1> listener)
    {
        AddEvent(eventId, listener);
    }
    
    public void AddListener<T,T1,T2>(E eventId, Callback<T,T1,T2> listener)
    {
        AddEvent(eventId, listener);
    }
    
    public void AddListener<T,T1,T2,T3>(E eventId, Callback<T,T1,T2,T3> listener)
    {
        AddEvent(eventId, listener);
    }
    
    //______________________________------------------------------------------------
    public void Dispatch(E eventId)
    {
        ForEach(eventId, listener => { if (listener is Callback callback) callback.Invoke(); });
    }
    
    public void Dispatch<T>(E eventId,T t)
    {
        ForEach(eventId, listener => { if (listener is Callback<T> callback) callback.Invoke(t);});
    }
    
    private void ForEach(E eventId, Action<Delegate> action)
    {
        _dicEvent.TryGetValue(eventId, out List<EventReceiver> list);
        if (list != null)
        {
            int listCount = list.Count;
            for (int i = listCount - 1; i >= 0; i--)
            {
                EventReceiver receiver = list[i];
                if (receiver != null)
                {
                    action?.Invoke(receiver.listener);
                }
                else
                {
                    list.RemoveAt(i);

                    if (list.Count <= 0)
                    {
                        _dicEvent.Remove(eventId);
                    }
                }
            }
        }
    }

}
