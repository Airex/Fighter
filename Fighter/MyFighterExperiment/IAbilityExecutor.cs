namespace MyFighterExperiment
{
    public interface IAbilityExecutor
    {
        AbilityExecutionResult Execute(IActor source, int abilityIndex, IActor target);
        int PrevAbilityIndex { get; }
        bool IsPrevAbilityInCombo { get; }
    }

    public class AbilityExecutionResult
    {
        public bool IsValid { get; set; }
    }

    public class AbilityExecutor : IAbilityExecutor
    {
        public int PrevAbilityIndex { get; private set; } = -1;
        public bool IsPrevAbilityInCombo { get; private set; }
      

        public AbilityExecutionResult Execute(IActor source, int abilityIndex, IActor target)
        {
            source.Step();
            target.Step();
            var ability = source.GetAbility(abilityIndex);
            if (ability != null)
            {
                var prevAbility = PrevAbilityIndex >= 0 && PrevAbilityIndex < source.AbbilitiesCount ? source.GetAbility(PrevAbilityIndex) : null;
                var abilityState = ability.UseOn(source,target, prevAbility, IsPrevAbilityInCombo);

                if (abilityState.IsAvailable)
                {
                    IsPrevAbilityInCombo = abilityState.IsInCombo;
                    PrevAbilityIndex = abilityIndex;
                    return new AbilityExecutionResult() {IsValid = true};
                }
            }

            return new AbilityExecutionResult() {IsValid = false};
        }
    }
}