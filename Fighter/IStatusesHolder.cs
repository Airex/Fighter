namespace MyFighterExperiment
{
    public interface IStatusesHolder
    {
        void ApplyAstatus(IActor source, IStatus status);
        void Step();
    }
}