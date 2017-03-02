namespace MyFighterExperiment
{
    public class Ability1 : ComboAbility
    {
        protected override int BaseDamage => 150;
        protected override int AdditionalDamageIfInCombo => 0;
        protected override bool CanComboAfter(IAbility ability, bool prevAbilityInCombo)
        {
            return true;
        }
    }
}