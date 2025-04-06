using UnityEngine;
using StateMachine;
using System;

public class PanelShowComponent
{
	private StateMachine<PanelStateValue, PanelState> _stateMachine =
		new StateMachine<PanelStateValue, PanelState>();
	public void InitStateMachine(KeyCode key, RectTransform rt,
		Vector2 hidePosition, Vector2 showPosition)
	{
		_stateMachine.AddState(PanelStateValue.Show, new PanelState(
			() => rt.anchoredPosition = showPosition, null));
		_stateMachine.AddState(PanelStateValue.Hide, new PanelState(
			() => rt.anchoredPosition = hidePosition, null));
		_stateMachine.ConnectState(PanelStateValue.Show, PanelStateValue.Hide,
			new LogicTriggerDownKey(){Key=key});
		_stateMachine.ConnectState(PanelStateValue.Hide, PanelStateValue.Show,
			new LogicTriggerDownKey(){Key=key});
		_stateMachine.InitState(PanelStateValue.Hide);
	}
	public void Tick()
	{
		_stateMachine.Tick();
	}
	public enum PanelStateValue
	{
		Show,
		Hide,
	}
	public struct PanelState: IState
	{
		private Action _onEnter;
		private Action _onExist;
		public PanelState(Action onEnter, Action onExist)
		{
			_onEnter = onEnter;
			_onExist = onExist;
		}
		public void OnEnter()
		{
			_onEnter?.Invoke();
		}
		public void OnExist()
		{
			_onExist?.Invoke();
		}
	}
}