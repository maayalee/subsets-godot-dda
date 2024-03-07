using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(IntegersVariable), "", nameof(Resource))]
    [Tool]
    public class IntegersVariable : Variable<Array<int>>
    {
        public IntegersVariable()
        {
            GD.Print("IntegersVariable::IntegersVariable()");
        }

        protected override Array<int> InitialValue
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
        
        protected override Array<int> DuplicateValue(Array<int> value)
        {
            return value;
        }


        [Export] public Array<int> initialValue = new Array<int>();
    }   
}