namespace MyFighterExperiment
{
    public class Ability2 : ComboAbility
    {
        protected override int BaseDamage => 100;
        protected override int AdditionalDamageIfInCombo => 120;
        protected override bool CanComboAfter(IAbility ability, bool prevAbilityInCombo)
        {
            return prevAbilityInCombo && ability is Ability1;
        }
    }
}