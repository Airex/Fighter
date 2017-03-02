namespace MyFighterExperiment
{
    public class HighDamageWith7OnCooldown : DamageBasedAbility
    {
        public HighDamageWith7OnCooldown() : base(7)
        {
        }

        protected override int BaseDamage => 380;
    }
}