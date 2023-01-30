using System;
using System.Collections.Generic;
using Godot;

namespace Subsets.Dda
{
    public class BaseEvent<T> : Resource
    {
        public event EventHandler<T> EventListener;
        
        public void Raise(T value)
        {
            //Debug.Log("BaseEvent::Raise: " + name);
           
            EventListener?.Invoke(this, value);
        }
    }
}