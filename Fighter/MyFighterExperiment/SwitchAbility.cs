using System;

namespace MyFighterExperiment
{
    public class SwitchAbility : IAbility
    {
        public AbilityState UseOn(IActor executor, IActor target, IAbility prevAbility, bool prevAbilityInCombo)
        {
            throw new NotImplementedException();
        }

        public void Step()
        {

        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}