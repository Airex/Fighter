namespace MyFighterExperiment
{
    public abstract class ComboAbility : DamageBasedAbility
    {

        protected abstract int AdditionalDamageIfInCombo { get; }

        public override AbilityState UseOn(IActor executor, IActor target, IAbility prevAbility, bool prevAbilityInCombo)
        {
            var abilityState = base.UseOn(executor, target, prevAbility, prevAbilityInCombo);
            if (!abilityState.IsAvailable) return new AbilityState();

            var isInCombo = CanComboAfter(prevAbility, prevAbilityInCombo);
            if (isInCombo)
            {
                target.Hit(AdditionalDamageIfInCombo);
            }

            return new AbilityState() { IsInCombo = isInCombo, IsAvailable = true };
        }

        protected abstract bool CanComboAfter(IAbility ability, bool prevAbilityInCombo);

        protected ComboAbility() : base(1)
        {
        }
    }
}