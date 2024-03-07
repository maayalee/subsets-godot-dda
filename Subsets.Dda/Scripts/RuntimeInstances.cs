using System.Collections.Generic;
using Godot;

namespace Subsets.Dda
{
    public static class RuntimeInstances
    {
        private static HashSet<IVariable> instances = new HashSet<IVariable>();
        private static HashSet<IVariable> initializeInstances = new HashSet<IVariable>();

        public static void Register(IVariable instance)
        {
            instances.Add(instance);
            initializeInstances.Add(instance);
            //GD.Print("RuntimeInstances::Registered:: count: "+ instances.Count);
        }

        public static void Enable(IVariable instance)
        {
            instance.OnEnable();
            initializeInstances.Remove(instance);
        }

        public static void EnableAll()
        {
            foreach (IVariable variable in initializeInstances)
            {
                variable.OnEnable();
            }
            initializeInstances.Clear();
        }

        public static void Unregister(IVariable instance)
        {
            instances.Remove(instance);
            //GD.Print("RuntimeInstances::Unregistered:: count: "+ instances.Count);
        }

        public static HashSet<IVariable> GetInstances()
        {
            return instances;
        }

        public static HashSet<IVariable> GetInitializeInstances()
        {
            return initializeInstances;
        }
    }
}