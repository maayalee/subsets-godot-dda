using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;

namespace Subsets.Dda
{
    
    [RegisteredType(nameof(RuntimeVariableSystem), "", nameof(Node))]
    public class RuntimeVariableSystem: Node
    {
        [Export] public List<Resource> PreLoadVariables = new List<Resource>();

        public override void _EnterTree()
        {
            GD.Print("RuntimeInitializer::_EnterTree");
            foreach (Resource resource in PreLoadVariables)
            {
                GD.Print("RuntimeInitializer::_Ready: Pre load resource :" + resource.ResourcePath);
                IVariable initializeResource = resource as IVariable;
                if (initializeResource != null)
                {
                    RuntimeInstances.Enable(initializeResource);
                }
            }    
        }

        public override void _Process(float delta)
        { 
            // 동적 생성하는 객체들에 대한 초기화 수행. 현재는 객체 접근이 발생하면 초기화하므로 제거해도 될지도 모른다.
            RuntimeInstances.EnableAll();
        }
    }
}