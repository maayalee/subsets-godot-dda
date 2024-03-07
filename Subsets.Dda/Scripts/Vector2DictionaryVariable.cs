﻿namespace Subsets.Dda
{
    using System;
    using Godot;
    using Godot.Collections;
    using MonoCustomResourceRegistry;
    using Subsets.Dda;

    [RegisteredType(nameof(Vector2DictionaryVariable), "", nameof(Resource))]
    [Tool]
    public class Vector2DictionaryVariable : Variable<Dictionary<int, Vector2>>
    {
        public Vector2DictionaryVariable()
        {
        }
    
        public Vector2 this[int index]
        {
            get
            {
                if (!Value.ContainsKey(index))
                    Value[index] = new Vector2();
                return Value[index];    
            }
            set
            {
                Value[index] = value;
            }
        }

        protected override Dictionary<int, Vector2> InitialValue
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
            
        protected override Dictionary<int, Vector2> DuplicateValue(Dictionary<int, Vector2> value)
        {
            return value;
        }

        [Export] public Dictionary<int, Vector2> initialValue =
            new Dictionary<int, Vector2>();
    } 
}