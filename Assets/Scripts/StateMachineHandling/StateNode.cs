using System;
using System.Collections.Generic;

namespace Game.StateMachineHandling
{
    public class StateNode
    {
        public IState State { get => state; }
        public HashSet<ITransition> Transitions { get => transitions; }

        public Action OnEnter;
        public Action OnExit;

        private IState state;
        private HashSet<ITransition> transitions;
        

        public StateNode(IState state)
        {
            this.state = state;
            this.transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState to, IPredicate condition)
        {
            transitions.Add(new Transition(to, condition));

            if(condition is IResetable resetable)
            {
                OnExit += resetable.Reset;
            }
        }

        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}
