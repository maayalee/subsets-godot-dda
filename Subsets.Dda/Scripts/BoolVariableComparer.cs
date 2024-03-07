using System;
using System.ComponentModel;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;

namespace Subsets.Dda
{
    public enum BoolCompare
    {
        IsTrue,
        IsFalse,
        Updated,
    }
    
    [RegisteredType(nameof(BoolVariableComparer), "", nameof(Node))]
    [Tool]
    public class BoolVariableComparer : BaseComparer<BoolVariable>, IPropertyComparer
    {
        [Export]
        private BoolCompare Compare { get; set; }

        BoolVariableComparer()
        {
        }

        public bool IsMatch()
        {
            if (null == Target)
                return false;
            
            if (Compare == BoolCompare.IsTrue)
            {
                return Target.Value == true;
            }
            else if (Compare == BoolCompare.IsFalse)
            {
                return Target.Value == false;
            }
            else if (Compare == BoolCompare.Updated)
            {
                return true;
            }
            return false;
        }

        public void RegisterValueChangeEvent(PropertyChangedEventHandler handler)
        {
            Target.PropertyChanged += handler;
        }

        public void UnregisterValueChangeEvent(PropertyChangedEventHandler handler)
        {
            Target.PropertyChanged -= handler;
        }

        public override void _Ready()
        {
            base._Ready();
            GD.Print("BoolVariableComparer::_Ready: BoolCompare: " + Compare);
        }
    }
}