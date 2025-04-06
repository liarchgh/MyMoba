using UnityEngine;
using StateMachine;
using System;

public partial class MenuPanel : MonoBehaviour {
	public Vector2 position_begin;
	public Vector2 position_end;
	private RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = this.GetComponent<RectTransform>();
		InitStateMachine();
	}

	void Update () { _stateMachine.Tick(); }
	public void OnStartClick()
	{
		LogUtil.Debug("OnStartClick");
	}
	public void OnEndClick()
	{
		LogUtil.Debug("OnEndClick");
	}

	void HidePanel() {
		rt.anchoredPosition = position_begin;
	}
	void ShowPanel() {
		rt.anchoredPosition = position_end;
	}
}
// state machine
public partial class MenuPanel
{
	private StateMachine<PanelStateValue, PanelState> _stateMachine =
		new StateMachine<PanelStateValue, PanelState>();
	private void InitStateMachine()
	{
		_stateMachine.AddState(PanelStateValue.Show, new PanelState(ShowPanel, null));
		_stateMachine.AddState(PanelStateValue.Hide, new PanelState(HidePanel, null));
		_stateMachine.ConnectState(PanelStateValue.Show, PanelStateValue.Hide,
			new LogicTriggerDownKey(){Key=KeyCode.Escape});
		_stateMachine.ConnectState(PanelStateValue.Hide, PanelStateValue.Show,
			new LogicTriggerDownKey(){Key=KeyCode.Escape});
		_stateMachine.InitState(PanelStateValue.Hide);
	}
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
