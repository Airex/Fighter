namespace MyFighterExperiment
{

    public interface IActor
    {
        int AbbilitiesCount { get; set; }
        IAbility GetAbility(int index);
        void Step();
        bool IsAvlive { get; }
        void Hit(double damage);
        void Reset();
        double Health { get; }
        IStatusesHolder StatusesHolder { get; }
    }


    //    public class Target : IActor
//    {
//       
//
//        readonly IList<IDot> _dots = new List<IDot>();
//        private readonly double _initialHealth;
//       
//
//        public void Hit(double damage)
//        {
//
//        }
//
//        public void ApplyDot(IDot dot)
//        {
//
//            if (_dots.Count == 0)
//                _dots.Add(dot);
//            for (int index = 0; index < _dots.Count; index++)
//            {
//                var d = _dots[index];
//                var canStackWith = dot.CanStackWith(d);
//                switch (canStackWith)
//                {
//                    case DotCombineEnum.AddNew:
//                        _dots.Add(dot);
//                        break;
//                    case DotCombineEnum.Replace:
//                        _dots.RemoveAt(index);
//                        _dots.Add(dot);
//                        break;
//                    case DotCombineEnum.Discard:
//                        break;
//                    case DotCombineEnum.Stack:
//                        _dots[index].Stack();
//                        break;
//                    default:
//                        throw new ArgumentOutOfRangeException();
//                }
//            }
//        }
//
//        public void Step()
//        {
//            for (int index = 0; index < _dots.Count; index++)
//            {
//                var dot = _dots[index];
//                if (dot.Act(this))
//                    _dots.RemoveAt(index);
//            }
//        }
//
//       
//    }
}