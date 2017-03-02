using System;

namespace MyFighterExperiment
{
    public interface IStatus
    {
        void AttachModifiers(IModifier[] modifiers);
    }


    public class SimpleDotStatus : IStatus
    {
        private readonly int _initialSteps;
        private int _steps;
        private readonly int _damage;
        private readonly int _maxStacks;
        private int _stacks = 1;

        public SimpleDotStatus(int steps, int damage, int maxStacks = 1)
        {
            _initialSteps = steps;
            _steps = steps;
            _damage = damage;
            _maxStacks = maxStacks;
        }

        public bool Act(IActor target)
        {
            target.Hit(_damage);
            _steps--;
            return _steps == 0;
        }

        public void Stack()
        {
            _steps = _initialSteps;
            _stacks = Math.Min(_maxStacks, _stacks + 1);
        }

        public void AttachModifiers(IModifier[] modifiers)
        {

        }
    }
}