using System;
using System.Collections.Generic;
using System.ComponentModel;
using Godot;
using Godot.Collections;
using Nprosoft.Cards.Client.Models;
using Array = Godot.Collections.Array;
using Object = Godot.Object;

namespace Subsets.Dda
{
    public abstract  class BaseTrigger: Node
    {
        [Export]
        public Godot.Collections.Array<NodePath> Conditions = new Array<NodePath>();

        [Export] 
        public Godot.Collections.Array<NodePath> Responses;
        
        [Export]
        public bool StopTriggerWhenEnter;
        
        [Export]
        public bool ExecuteWhenStarted = false;
        [Export]
        public bool ExecutePeriodic = false;
        [Export]
        public float ExecutePeriodicTime = 1.0f;

        private float time;

        public override void _EnterTree()
        {
            if (!Engine.EditorHint)
            {
                Enable();
            }
        }

        void Enable()
        {
            foreach (NodePath nodePath in Conditions)
            {
                IPropertyComparer comparer = GetNode<IPropertyComparer>(nodePath);
                if (comparer != null)
                {
                    comparer.RegisterValueChangeEvent(OnChanged);
                }
            }
                                
            if (ExecuteWhenStarted)
            {
                Execute();
            }
        }

        public override void _ExitTree()
        {
            Disable();
        }

        public void Disable()
        {
            GD.Print("BaseTrigger::_ExitTree");
            foreach (NodePath nodePath in Conditions)
            {
                IPropertyComparer comparer = GetNode<IPropertyComparer>(nodePath);
                if (comparer != null)
                {
                    comparer.UnregisterValueChangeEvent(OnChanged);
                }
            }
        }

        public override void _Process(float delta)
        {
            if (!Engine.EditorHint)
            {
                if (ExecutePeriodic)
                {
                    time += delta;
                    if (time > ExecutePeriodicTime)
                    {
                        time = time - ExecutePeriodicTime;
                        Execute();
                    }
                }    
            }
            
        }

        private void TriggerEnter()
        {
            foreach (NodePath nodePath in Responses)
            {
                if (!nodePath.IsEmpty())
                {
                    GodotEventResponse response = GetNode<GodotEventResponse>(nodePath);
                    response.Invoke();
                }
            }
            
            if (StopTriggerWhenEnter)
            {
                ExecutePeriodic = false;
                Disable();
                SetProcess(false);
            }
        }
        
        void OnChanged(object sender, PropertyChangedEventArgs args)
        {
            Execute(); 
        }

        void Execute()
        {
            if (IsMatchedConditions())
            {
                TriggerEnter();
            }

            ExecuteWhenStarted = false;
        }

        protected abstract bool IsMatchedConditions();
    }
}