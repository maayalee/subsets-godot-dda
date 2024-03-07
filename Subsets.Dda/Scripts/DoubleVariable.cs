using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;

namespace Subsets.Dda
{
    
    [RegisteredType(nameof(DoubleVariable), "", nameof(Resource))]
    [Tool]
    public class DoubleVariable : Variable<double>
    {
        public DoubleVariable() : base()
        {
            GD.Print("DoubleVariable::DoubleVariable()");
        }

        protected override double DuplicateValue(double value)
        {
            return value;
        }
        
        
        protected override double InitialValue
        {
            get
            {
                return initialValue;
            }
            set
            {
                initialValue = value;
            }
        }
        
        
        [Export] private double initialValue;
    }
}