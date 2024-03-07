namespace Subsets.Dda
{
    using System;
    using Godot;
    using Godot.Collections;
    using MonoCustomResourceRegistry;
    using Subsets.Dda;

    [RegisteredType(nameof(Vector2sDictionaryVariable), "", nameof(Resource))]
    [Tool]
    public class Vector2sDictionaryVariable : Variable<Dictionary<int, Array<Vector2>>>
    {
        public Vector2sDictionaryVariable()
        {
        }
    
        public Array<Vector2> this[int index]
        {
            get
            {
                if (!Value.ContainsKey(index))
                    Value[index] = new Array<Vector2>();
                return Value[index];    
            }
            set
            {
                Value[index] = value;
            }
        }

        protected override Dictionary<int, Array<Vector2>> InitialValue
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
            
        protected override Dictionary<int, Array<Vector2>> DuplicateValue(Dictionary<int, Array<Vector2>> value)
        {
            return value;
        }

        [Export] public Dictionary<int, Array<Vector2>> initialValue =
            new Dictionary<int, Array<Vector2>>();
    } 
}