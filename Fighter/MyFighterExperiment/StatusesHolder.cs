using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFighterExperiment
{
    public class StatusesHolder : IStatusesHolder
    {
        private IActor _actor;

        readonly IList<StatusContainer> _holders = new List<StatusContainer>();

        public StatusesHolder(IActor actor)
        {
            _actor = actor;
        }

        public void ApplyAstatus(IActor source, IStatus status)
        {
            var singleOrDefault = _holders.SingleOrDefault(holder => holder.Source == source && holder.Status.GetType() == status.GetType());
            if (singleOrDefault == null)
                _holders.Add(new StatusContainer(source, status));
        }

        public void Step()
        {
            foreach (var statusContainer in _holders)
            {
              //  statusContainer.Status.Act(_actor);
            }
        }
    }

    public interface IModifier
    {
        void ToDamage();
        void ToDefense();
    }


    public class StatusContainer
    {
        public StatusContainer(IActor source, IStatus status)
        {
            Source = source;
            Status = status;
        }

        public IActor Source { get; }
        public IStatus Status { get; }
    }
}