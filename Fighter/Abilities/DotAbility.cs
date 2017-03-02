namespace MyFighterExperiment
{
    public class DotAbility : DamageBasedAbility
    {
        public override AbilityState UseOn(IActor executor, IActor target, IAbility prevAbility, bool prevAbilityInCombo)
        {
//            target.StatusesHolder.ApplyAstatus(new SimpleDotStatus(10, 37));
            return new AbilityState() { IsAvailable = true };
        }

        public DotAbility() : base(1)
        {
        }

        protected override int BaseDamage => 0;
    }
}