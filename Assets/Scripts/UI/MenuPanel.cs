using UnityEngine;
using StateMachine;
using System;

public partial class MenuPanel : MonoBehaviour {
	private PanelShowComponent _panelShowComponent = new PanelShowComponent();
	public Vector2 position_begin;
	public Vector2 position_end;
	private RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = this.GetComponent<RectTransform>();
		_panelShowComponent.InitStateMachine(KeyCode.Escape, rt,
			position_begin, position_end);
	}

	void Update () { _panelShowComponent.Tick(); }
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
