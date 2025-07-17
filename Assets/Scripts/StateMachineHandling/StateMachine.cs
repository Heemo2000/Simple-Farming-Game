using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachineHandling
{
    public class StateMachine
    {
        private StateNode previous;
        private StateNode current;
        private Dictionary<Type, StateNode> nodes = new Dictionary<Type, StateNode>();
        private HashSet<ITransition> anyTransitions = new HashSet<ITransition>();

        public override string ToString()
        {
            return $"Current: {current?.State.GetType().Name}, Previous: {previous?.State.GetType().Name}";
        }

        public void OnUpdate()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            current.State?.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            current.State?.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            current.State?.OnLateUpdate();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.OnEnter();
        }

        void ChangeState(IState state)
        {
            if (state == current.State) return;

            previous = current;

            var previousState = previous.State;
            var nextStateNode = nodes[state.GetType()];
            var nextState = nextStateNode.State;

            previous?.Exit();
            previousState?.OnExit();
            nextStateNode?.Enter();
            nextState?.OnEnter();
            current = nodes[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (var transition in anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (var transition in current.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        StateNode GetOrAddNode(IState state)
        {
            StateNode node = null;

            if (!nodes.TryGetValue(state.GetType(), out node))
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }

            return node;
        }
    }
}
