using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;

namespace Subsets.Dda
{
    
    [RegisteredType(nameof(FloatVariable), "", nameof(Resource))]
    [Tool]
    public class FloatVariable : Variable<float>
    {
        public FloatVariable() : base()
        {
            GD.Print("FloatVariable::FloatVariable()");
        }

        protected override float DuplicateValue(float value)
        {
            return value;
        }
        
        
        protected override float InitialValue
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
        
        
        [Export] private float initialValue;
    }
}