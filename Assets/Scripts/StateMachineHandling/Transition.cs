
namespace Game.StateMachineHandling
{
    public class Transition : ITransition
    {
        public IState To { get => to; }
        public IPredicate Condition { get => condition; }

        private IState to;
        private IPredicate condition;

        public Transition(IState to, IPredicate condition)
        {
            this.to = To;
            this.condition = condition;
        }
    }
}
