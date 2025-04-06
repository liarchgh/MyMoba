using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkPanel : MonoBehaviour {
	private PanelShowComponent _panelShowComponent = new PanelShowComponent();
	public Rigidbody panel_rb;
	private int panel_state = state_static;
	private const int state_static = 0;
	private const int state_right = 2;
	private const int state_left = 3;

	public Vector3 position_begin;
	public Vector3 position_end;

    private RectTransform rt;

	// Use this for initialization
	void Start () {
        rt = this.GetComponent<RectTransform>();
		_panelShowComponent.InitStateMachine(KeyCode.Tab, rt,
			position_begin, position_end);
	}

	// Update is called once per frame
	void Update () {
		_panelShowComponent.Tick();
	}
}
