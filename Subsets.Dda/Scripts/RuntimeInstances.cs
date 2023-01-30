using System.Collections.Generic;
using Godot;

namespace Subsets.Dda
{
    public static class RuntimeInstances
    {
        private static HashSet<IRuntimeInitialize> instances = new HashSet<IRuntimeInitialize>();

        public static void Register(IRuntimeInitialize instance)
        {
            instances.Add(instance);
            GD.Print("RuntimeInstances::Registered:: count: "+ instances.Count);
        }

        public static void Unregister(IRuntimeInitialize instance)
        {
            instances.Remove(instance);
            GD.Print("RuntimeInstances::Unregistered:: count: "+ instances.Count);
        }

        public static HashSet<IRuntimeInitialize> GetInstances()
        {
            return instances;
        }
    }
}