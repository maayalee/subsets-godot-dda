using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(StringsVariable), "", nameof(Resource))]
    [Tool]
    public class StringsVariable : Variable<Dictionary<int, string>>
    {
        public StringsVariable()
        {
            GD.Print("StringsVariable::StringsVariable()");
        }

        public string this[int index]
        {
            get
            {
                if (!Value.ContainsKey(index))
                    throw new Exception("invalid key value");
                return Value[index];    
            }
            set
            {
                Value[index] = value;
            }
        }

        protected override Dictionary<int, string> InitialValue
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
        
        protected override Dictionary<int, string> DuplicateValue(Dictionary<int, string> value)
        {
            return value;
        }


        [Export] public Dictionary<int, string> initialValue = new Dictionary<int, string>();
    }   
}