namespace MyFighterExperiment
{
    public abstract class AbilityWithCooldown : IAbility
    {
        private readonly int _coolDown;
        private int _countDown;

        protected AbilityWithCooldown(int coolDown)
        {
            _coolDown = coolDown;
        }
        
        public virtual AbilityState UseOn(IActor executor, IActor target, IAbility prevAbility, bool prevAbilityInCombo)
        {
            if (_countDown == 0)
            {
                _countDown = _coolDown;
                return new AbilityState() { IsAvailable = true };
            }
            return new AbilityState() { IsAvailable = false };
        }

        public void Step()
        {
            if (_countDown > 0)
                _countDown--;
        }

        public void Reset()
        {
            _countDown = 0;
        }
    }
}