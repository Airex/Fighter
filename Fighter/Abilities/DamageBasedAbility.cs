namespace MyFighterExperiment
{
    public abstract class DamageBasedAbility : AbilityWithCooldown
    {

        protected abstract int BaseDamage { get; }

        public override AbilityState UseOn(IActor executor, IActor target, IAbility prevAbility, bool prevAbilityInCombo)
        {
            var abilityState = base.UseOn(executor, target, prevAbility, prevAbilityInCombo);
            if (!abilityState.IsAvailable) return abilityState;

            target.Hit(BaseDamage);

            return new AbilityState() { IsInCombo = false, IsAvailable = true };
        }

        protected DamageBasedAbility(int coolDown) : base(coolDown)
        {
        }
    }
}