namespace MyFighterExperiment
{
    public class Ability3 : ComboAbility
    {
        protected override int BaseDamage => 100;
        protected override int AdditionalDamageIfInCombo => 200;
        protected override bool CanComboAfter(IAbility ability, bool prevAbilityInCombo)
        {
            return prevAbilityInCombo && ability is Ability2;
        }
    }
}