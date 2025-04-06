using System;
using System.Collections.Generic;
using System.Linq;
namespace StateMachine
{
	public interface IState
	{
		void OnEnter();
		void OnExist();
	}

	public class StateMachine<StateEnum, StateType>
		where StateType: IState
		where StateEnum: Enum
	{
		private class StateConnection
		{
			public LogicTriggerBase Trigger;
			public StateEnum Target;
		}
		private Dictionary<StateEnum, List<StateConnection>> _connections =
			new Dictionary<StateEnum, List<StateConnection>>();
		private Dictionary<StateEnum, StateType> _states =
			new Dictionary<StateEnum, StateType>();
		private StateEnum _nowState;
		public void AddState(StateEnum state, StateType config)
		{
			_states.Add(state, config);
		}
		public void RemoveState(StateEnum state)
		{
			_states.Remove(state);
			_connections.Remove(state);
		}
		public void ConnectState(StateEnum state1, StateEnum state2, LogicTriggerBase trigger)
		{
			if (!_connections.ContainsKey(state1))
				_connections.Add(state1, new List<StateConnection>());
			_connections[state1].Add(new StateConnection()
			{
				Target = state2,
				Trigger = trigger,
			});
		}
		public void InitState(StateEnum state)
		{
			_nowState = state;
			_states[_nowState].OnEnter();
		}
		private void EnterState(StateEnum state)
		{
			_states[_nowState].OnExist();
			_nowState = state;
			_states[_nowState].OnEnter();
		}

		public void Tick()
		{
			var triggeredStates = _connections[_nowState]
				.Where(x=>x.Trigger.CheckSkillTrigger());
			if(!triggeredStates.Any()) return;
			EnterState(triggeredStates.First().Target);
		}
	}
}