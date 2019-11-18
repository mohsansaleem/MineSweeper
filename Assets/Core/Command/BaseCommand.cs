using System;
using Zenject;

namespace PM.Core.Command
{
    public class BaseCommand
    {
        [Inject]
        protected SignalBus SignalBus;

        public BaseCommand()
        {
        }

        public virtual void Execute()
        {
            throw new Exception("BaseCommand - Execute() is not implemented.");
        }
    }
}