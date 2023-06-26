using System;
using UnityEngine;

public abstract class  ChannelSO<T> : ScriptableObject
{
    private Action<T> dataEvent;
    public void Subscribe(in Action<T> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T> handler)
    {
        dataEvent += handler;
    }

    public void RaiseEvent(T data)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data);
        }
    }
}
public abstract class ChannelSO<T1,T2> : ScriptableObject
{
    private Action<T1, T2> dataEvent;
    public void Subscribe(in Action<T1, T2> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T1, T2> handler)
    {
        dataEvent += handler;
    }

    public void RaiseEvent(T1 data1,T2 data2)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data1,data2);
        }
    }
}
public abstract class ChannelSO<T1,T2,T3> : ScriptableObject
{
    private Action<T1,T2,T3> dataEvent;
    public void Subscribe(Action<T1, T2, T3> handler)
    {
        dataEvent += handler;
    }
    public void Unsubscribe(in Action<T1, T2, T3> handler)
    {
        dataEvent += handler;
    }

    public void RaiseEvent(T1 data1,T2 data2,T3 data3)
    {
        if (dataEvent == null)
        {
            Debug.LogWarning($"{name}:No subscribers to this event.");
        }
        else
        {
            dataEvent?.Invoke(data1,data2,data3);
        }
    }
}
