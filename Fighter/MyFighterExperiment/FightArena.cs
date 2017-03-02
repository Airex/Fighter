using System;
using System.Collections.Generic;
using SharpNeat.Phenomes;

namespace MyFighterExperiment
{
    public class FightArena
    {
        private readonly int _maxHits;
        private readonly bool _saveHits;

        public FightArena(int maxHits, bool saveHits = false)
        {
            _maxHits = maxHits;
            _saveHits = saveHits;
        }

        public IList<int> Hits { get; } = new List<int>();

        public double Start(IBlackBox box)
        {
            Hits.Clear();

            IActor target = new DefaultActor(30000);
            IActor fighter = new DefaultActor(0);

            IAbilityExecutor abilityExecutor = new AbilityExecutor();

            for (var step = 0; step <= _maxHits; step++)
            {

                box.InputSignalArray[0] = (double)abilityExecutor.PrevAbilityIndex / fighter.AbbilitiesCount;
                box.InputSignalArray[1] = abilityExecutor.IsPrevAbilityInCombo ? 1 : 0;

                box.Activate();
             

                var abilityIndexSignal = (int)(box.OutputSignalArray[0] * fighter.AbbilitiesCount);
                if (_saveHits)
                    Hits.Add(abilityIndexSignal);

                try
                {
                    var abilityExecutionResult = abilityExecutor.Execute(fighter, abilityIndexSignal, target);

                    if (!abilityExecutionResult.IsValid) return 0;
                    if (!target.IsAvlive) return _maxHits - step;
                }
                catch (Exception)
                {
                    return 0;
                }
            }


            return 0 ;
        }
    }
}