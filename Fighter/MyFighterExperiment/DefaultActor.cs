using System;

namespace MyFighterExperiment
{
    public class DefaultActor : IActor
    {

        private readonly double _initalHealth;


        private readonly IAbility[] _abilities = {
            new DotAbility(),
            new Ability1(),
            new Ability2(),
            new Ability3(),
            new HighDamageWith7OnCooldown()

        };

        public DefaultActor(double initalHealth)
        {
            _initalHealth = initalHealth;
            AbbilitiesCount = _abilities.Length;
            Reset();
        }

        public int AbbilitiesCount { get; set; }

        public IAbility GetAbility(int index)
        {
            return _abilities[index];
        }

        public void Step()
        {
            foreach (var ability in _abilities)
            {
                ability.Step();
            }
            StatusesHolder.Step();
        }

        public bool IsAvlive => Health > 0;
        public void Hit(double damage)
        {
            Health = Math.Max(0, Health - damage);
        }

        public void Reset()
        {
            Health = _initalHealth;
        }

        public double Health { get; private set; }

        public IStatusesHolder StatusesHolder { get; } 
    }
}