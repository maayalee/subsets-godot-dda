using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Cards2.addons.Subsets.Dda.Scripts;
using Godot;
using Godot.Collections;
using Nprosoft.Cards.Client.Models;
using Nprosoft.Cards.Shared.Core;
using Object = Godot.Object;

namespace Subsets.Dda
{
    public abstract class BaseEvent<TProperties> : Resource, IEvent
        where TProperties : BaseProperties, new()
    {
        [Export] private Godot.Collections.Dictionary<string, object> properties =
            new Godot.Collections.Dictionary<string, object>();

        // @todo BaseVariable 처럼 기본 형태의 데이터로 받는다? BaseProperteis 형태를 가지지 않아도 RRTI 조회로 대부분 될듯하다.
        // Atom 구현물 참고
        public TProperties Value = new TProperties();

        private event EventRaisable<TProperties> RaiseAbleEvent;
        private event EventHandler<TProperties> ListenerEvent;
        
        private List<EventRaisable<TProperties>> raiseAbleDelegates = new List<EventRaisable<TProperties>>();
        private List<EventHandler<TProperties>> listenerDelegates = new List<EventHandler<TProperties>>();

        protected BaseEvent()
        {
            DictionaryKeyValueBuilder builder = new DictionaryKeyValueBuilder();
            builder.Initialize<TProperties>(properties);
            Value.FromDictionary<TProperties>(properties);
            
            RegisterRaisable((sender, baseProperties) => true);
        }
        
        public void RaiseWithProperty(Godot.Collections.Dictionary<string, object> propertyValues)
        {
            GD.Print("BaseEvent::Raise(Dictionary): " + ResourcePath);
            Value.FromDictionary<TProperties>(propertyValues);

            if (IsRaiseable(this))
            {
                foreach (EventHandler<TProperties> handler in listenerDelegates)
                {
                    handler?.Invoke(this, Value);
                }
            }

        }

        public void RegisterRaisable(EventRaisable<TProperties> raisable)
        {
            RaiseAbleEvent += raisable;
            raiseAbleDelegates.Add(raisable);
        }
        
        public void UnregisterRaisable(EventRaisable<TProperties> raisable)
        {
            RaiseAbleEvent -= raisable;
            raiseAbleDelegates.Remove(raisable);
        }

        public bool IsRaiseable(object sender)
        {
            /*
            foreach (EventRaiseable<TProperties> raiseable in raiseableDelegates)
            {
                if (!raiseable.Invoke(this, Value))
                    return false;
            }
            return true;
            */
            return RaiseAbleEvent.Invoke(sender, Value);
        }

        public void ClearRaisables()
        {
            foreach (EventRaisable<TProperties> raiseable in raiseAbleDelegates)
            {
                RaiseAbleEvent -= raiseable;
            }
            raiseAbleDelegates.Clear();
        }

        public void Raise()
        {
            if (IsRaiseable(this))
            {
                ListenerEvent?.Invoke(this, Value);
            }
        }
        
        public void Raise(object sender)
        {
            if (IsRaiseable(sender))
            {
                ListenerEvent?.Invoke(sender, Value);
            }
        }
        
        public void RegisterListener(EventHandler<TProperties> action)
        {
            ListenerEvent += action;
            listenerDelegates.Add(action);
        }
        
        public void RegisterListener(EventRaisable<TProperties> raisable, EventHandler<TProperties> action)
        {
            RegisterRaisable(raisable);
            ListenerEvent += action;
            listenerDelegates.Add(action);
        }

        public bool HasListener(EventHandler<TProperties> action)
        {
            return listenerDelegates.Contains(action);
        }
        
        public void UnregisterListener(EventHandler<TProperties> action)
        {
            ListenerEvent -= action;
            listenerDelegates.Remove(action);
        }
        
        public void UnregisterListener(EventRaisable<TProperties> raisable, EventHandler<TProperties> action)
        {
            UnregisterRaisable(raisable);
            ListenerEvent -= action;
            listenerDelegates.Remove(action);
        }
        
        public void UnregisterAllListeners()
        {
            foreach (EventHandler<TProperties> listener in listenerDelegates)
            {
                ListenerEvent -= listener;
            }
            listenerDelegates.Clear();
        }

        public virtual object[] ToArgs()
        {
            throw new Exception("Must implementation ToArgs");
        }

        public virtual void FromArgs(object[] args)
        {
            throw new Exception("Must implementation FromArgs");
        }
    }
}