using System;
using SharpNeat.Core;
using SharpNeat.Phenomes;

namespace MyFighterExperiment
{
    public class FighterBlackBoxEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        private readonly int _maxHits;
        public ulong EvaluationCount { get; private set; }

        public bool StopConditionSatisfied => false;

        

        public FighterBlackBoxEvaluator(int maxHits)
        {
            _maxHits = maxHits;
        }   


        public FitnessInfo Evaluate(IBlackBox box)
        {
            EvaluationCount++;

            var arena = new FightArena(_maxHits);
            var fitness = arena.Start(box);

            return new FitnessInfo(fitness, fitness);

          
        }

        public void Reset()
        {
            
        }
    }
}