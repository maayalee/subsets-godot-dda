using System;
using System.ComponentModel;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;

namespace Subsets.Dda
{
    [RegisteredType(nameof(StringVariableComparer), "", nameof(Node))]
    [Tool]
    public class StringVariableComparer : BaseComparer<StringVariable>, IPropertyComparer
    {
        [Export]
        private StringCompare Compare { get; set; }
        [Export] protected string Value;

        StringVariableComparer()
        {
        }

        public bool IsMatch()
        {
            if (null == Target)
                return false;
            
            if (Compare == StringCompare.Equal)
            {
                return Target.Value.Equals(Value);
            }
            else if (Compare == StringCompare.Contains)
            {
                return Target.Value.Contains(Value);
            }
            else if (Compare == StringCompare.IsNot)
            {
                return !Target.Value.Equals(Value);
            }
            else if (Compare == StringCompare.Updated)
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
            GD.Print("StringVariableComparer::_Ready: StringCompare: " + Compare);
        }
    }
}