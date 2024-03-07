using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(LongVariable), "", nameof(Resource))]
    [Tool]
    public class LongVariable : Variable<long>
    {
        public LongVariable()
        {
            //GD.Print("LongVariable::LongVariable()");
        }
     
        protected override long DuplicateValue(long value)
        {
            return value;
        }

        protected override long InitialValue
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


        [Export] public long initialValue;
    }   
}