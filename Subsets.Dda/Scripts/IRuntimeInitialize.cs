namespace Subsets.Dda
{
    public interface IRuntimeInitialize
    {
        void RuntimeInitialize();
        void RaiseRuntimeInitializeEvent();
        void RuntimeFinalize();
    }
}