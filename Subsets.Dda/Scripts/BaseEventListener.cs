using System;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    public abstract class BaseEventListener<T> : Node 
        where T: BaseProperties, new()
    {
        [Export]
        public BaseEvent<T> Event;
       
        [Export]
        public ResponseConditionOperator ConditionOperator;
        [Export]
        public Array<NodePath> Conditions = new Array<NodePath>();

        [Export] public Array<NodePath> Responses = new Array<NodePath>();
        
        public override void _Ready()
        {
            base._Ready();
            
        }

        public override void _EnterTree()
        {
            if (Engine.EditorHint)
                return;
            if (Event != null)
            {
                Event?.RegisterListener(OnEventRaised);
            }
        }

        public override void _ExitTree()
        {
            if (Engine.EditorHint)
                return;
            Disable();
        }

        public void Disable()
        {
            if (Event != null)
            {
                Event.UnregisterListener(OnEventRaised);
            }
        }
        
        
        public void OnEventRaised(object sender, T value)
        {
            if (IsMatchedConditions())
            {
                foreach (NodePath nodePath in Responses)
                {
                    GodotEventResponse response = GetNode<GodotEventResponse>(nodePath);
                    DynamicInvoke(response, value);
                }
            }
        }

        private bool IsMatchedConditions()
        {
            ConditionCompareResult result = new ConditionCompareResult();
            foreach (NodePath nodePath in Conditions)
            {
                IPropertyComparer comparer = GetNode<IPropertyComparer>(nodePath);
                result.Add(comparer.IsMatch());
            }
            
            return result.CheckConditionOperator(ConditionOperator);
        }
        protected abstract void DynamicInvoke(GodotEventResponse response, T dynamicValue);
    }
}