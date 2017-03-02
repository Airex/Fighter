namespace MyFighterExperiment
{
    public interface IAbility
    {
        AbilityState UseOn(IActor executor, IActor target, IAbility prevAbility, bool prevAbilityInCombo);
        void Step();
        void Reset();
    }
}