using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using Array = Godot.Collections.Array;
using Object = Godot.Object;

namespace Subsets.Dda
{
    public enum ResponseTarget
    {
        Resource,
        Node,
    }

    public enum ResponseParameter
    {
        StaticParameter,
        Dynamic,
    }

    public class DynamicInvokeValue
    {
        public Variant.Type Type;
        public object Value;
    }

    [RegisteredType(nameof(GodotEventResponse), "", nameof(Godot.Node))]
    [Tool]
    public class GodotEventResponse : Node
    {
        private ResponseTarget Target
        {
            get { return target; }
            set
            {
                target = value;
                PropertyListChangedNotify();
            }
        }

        private ResponseTarget target;

        private Resource Resource
        {
            get { return resource; }
            set
            {
                resource = value;
                PropertyListChangedNotify();
            }
        }

        private Resource resource;

        private NodePath Node
        {
            get { return node; }
            set
            {
                node = value;
                PropertyListChangedNotify();
            }
        }

        private NodePath node;


        private string Method
        {
            get { return method; }
            set
            {
                method = value;
                Parameters = new Array<object>();
                if (parameterTypes.ContainsKey(value))
                {
                    Array<Variant.Type> parameters = parameterTypes[value];
                    for (int i = 0; i < parameters.Count; ++i)
                    {
                        if (parameters[i] == Variant.Type.Dictionary)
                        {
                            Parameters.Add(new Godot.Collections.Dictionary());
                        }
                        else
                        {
                            Parameters.Add("");
                        }
                    }
                }

                PropertyListChangedNotify();
            }
        }

        private string method = "";

        private Godot.Collections.Array<object> Parameters
        {
            get { return parameters; }
            set
            {
                parameters = value;
                PropertyListChangedNotify();
            }
        }

        private Godot.Collections.Array<object> parameters = new Array<object>();

        public Godot.Collections.Dictionary<string, Array<Variant.Type>> ParameterTypes
        {
            get
            {
                return parameterTypes;
            }
            set
            {
                parameterTypes = value;
            }
        }

        private Godot.Collections.Dictionary<string, Array<Variant.Type>> parameterTypes =
            new Godot.Collections.Dictionary<string, Array<Variant.Type>>();


        public bool UseDynamicInvoke = true;

        public override Array _GetPropertyList()
        {

            Godot.Collections.Array properties = new Array();
            /*
            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "Response" },
                { "type", Godot.Variant.Type.Nil },
                { "usage", PropertyUsageFlags.Category | PropertyUsageFlags.Editor}
            });
            */
            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "Target" },
                { "type", Godot.Variant.Type.Int },
                { "hint", PropertyHint.Enum },
                { "hint_string", ResponseTarget.Resource.ToString() + "," + ResponseTarget.Node.ToString() },
                { "usage", PropertyUsageFlags.Default }
            });

            Godot.Object target = null;
            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "Resource" },
                { "type", Godot.Variant.Type.Object },
                { "usage", PropertyUsageFlags.Default }
            });
            if (Target == ResponseTarget.Resource)
                target = Resource;
            
            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "Node" },
                { "type", Godot.Variant.Type.NodePath },
                { "usage", PropertyUsageFlags.Default }
            });
            if (Target == ResponseTarget.Node)
            {
                if (Node != null && !Node.IsEmpty())
                {
                    target = GetNode(Node);
                }
            }
            

            string hintString = "";
            if (target != null)
            {
                parameterTypes.Clear();
                foreach (Godot.Collections.Dictionary property in target.GetMethodList())
                {
                    Array<Variant.Type> parameters = new Array<Variant.Type>();

                    Array args = property["args"] as Array;
                    string registerMethod = "";
                    if (null == args || args.Count == 0)
                    {
                        registerMethod = String.Format("{0}()", property["name"]);
                        GD.Print(registerMethod + " is non parameter");
                    }
                    else
                    {
                        for (int i = 0; i < args.Count; ++i)
                        {
                            Dictionary dictionary = args[i] as Dictionary;
                            Godot.Variant.Type variant = (Variant.Type)dictionary["type"];
                            if (variant == Variant.Type.String || variant == Variant.Type.Int ||
                                variant == Variant.Type.Bool || variant == Variant.Type.Real ||
                                variant == Variant.Type.Object || variant == Variant.Type.Dictionary ||
                                variant == Variant.Type.Array)
                            {
                                if (i == 0)
                                {
                                    registerMethod = String.Format("{0}(", property["name"]);
                                }

                                registerMethod += String.Format("{0} {1} ", variant, dictionary["name"]);
                                if (i == args.Count - 1)
                                {
                                    registerMethod += ")";
                                }

                                parameters.Add(variant);
                            }
                            else
                            {
                                registerMethod = "";
                                break;
                            }
                        }
                    }

                    if (registerMethod.Length > 0)
                    {
                        GD.Print(registerMethod);
                        if (!parameterTypes.ContainsKey(registerMethod))
                        {
                            hintString += registerMethod;
                            hintString += ',';
                            parameterTypes.Add(registerMethod, parameters);
                        }
                    }

                }
            }

            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "Method" },
                { "type", Godot.Variant.Type.String },
                { "hint", PropertyHint.Enum },
                { "hint_string", hintString },
                { "usage", PropertyUsageFlags.Default }
            });
            
            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "UseDynamicInvoke" },
                { "type", Godot.Variant.Type.Bool },
                { "usage", PropertyUsageFlags.Default }
            });

            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "Parameters" },
                { "type", Godot.Variant.Type.Array },
                { "hint", PropertyHint.Enum },
                //{ "hint_string", hintString },
                { "usage", PropertyUsageFlags.Default }
            });
            
            properties.Add(new Godot.Collections.Dictionary()
            {
                { "name", "ParameterTypes" },
                { "type", Godot.Variant.Type.Array },
                { "usage", PropertyUsageFlags.Noeditor }
            });

            GD.Print("NodeEvent::properties:" + properties);
            return properties;
        }

        public void Invoke()
        {
            Object target = GetTarget();
            if (null != target)
            {
                string methodName = Method.Remove(Method.Find("("));
                if (methodName.Length == 0)
                    return;
                target.Callv(methodName, CreateCallParameters());
            }
        }

        public void Invoke(List<DynamicInvokeValue> dynamicValues)
        {
            GD.Print("GodotEventResponse::Invoke");
            
            Object target = GetTarget();
            if (null == target)
            {
                GD.PrintErr("GodotEventResponse::Invoke::target is null");
                return;
            }

            if (Method == null)
            {
                GD.PrintErr("GodotEventResponse::Invoke::method is null");
                return;
            }

            int startIndex = Method.Find("(");
            if (startIndex == -1)
            {
                GD.PrintErr("GodotEventResponse::Invoke::Method name is invalid");
                return;
            }

            string methodName = Method.Remove(startIndex);
            Array<Variant.Type> parameters = parameterTypes[Method];
                
            // 리스너가 전달해주는 형과 동일하면 동적 값 전달을 사용한다.
            if (IsDynamicValue(parameters, dynamicValues))
            {
                Array args = new Array();
                foreach (DynamicInvokeValue value in dynamicValues)
                {
                    args.Add(value.Value);
                    GD.Print("GodotEventResponse::Invoke:: DynamicValue: " + value.Value);
                }
                GD.Print( String.Format("GodotEventResponse::Invoke:: Dynamic Call:: {0}({1})", methodName, args));
                target.Callv(methodName, args);    
            }
            else
            {
                Array args = CreateCallParameters();
                GD.Print( String.Format("GodotEventResponse::Invoke:: Call {0}({1})", methodName, args));
                target.Callv(methodName, args);
            }
        }

        bool IsDynamicValue(Array<Variant.Type> parameterTypes, List<DynamicInvokeValue> dynamicValues)
        {
            if (!UseDynamicInvoke)
                return false;
            if (dynamicValues.Count == 0)
                return false;
            int count = 0;
            if (parameterTypes.Count == dynamicValues.Count)
            {
                for (int i = 0; i < dynamicValues.Count; ++i)
                {
                    if (parameterTypes[i] == dynamicValues[i].Type)
                        count++;
                }
            }

            return count == dynamicValues.Count;
        }

        private Array CreateCallParameters()
        {
            Array args = new Array();
            foreach (object value in Parameters)
            {
                args.Add(value);
            }
            return args;
        }

        private Object GetTarget()
        {
            Object target = null;
            if (Target == ResponseTarget.Resource)
            {
                target = Resource;
            }

            if (Target == ResponseTarget.Node)
            {
                if (Node != null && !Node.IsEmpty())
                {
                    target = GetNode(Node);
                }
            }

            return target;
        }
    }
}