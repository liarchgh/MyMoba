using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkPanel : MonoBehaviour {
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
	}

	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			set_panel_state(state_right);
		}
		if (Input.GetKeyUp(KeyCode.Tab)) {
			set_panel_state(state_left);
		}

        //根据状态板状态 设置状态板的速度
		switch (panel_state) {
		case state_right:
			panel_rb.velocity = Vector3.right * 20 / 1000 * Screen.width * (position_end.x - rt.localPosition.x) ;
			if (position_end.x - rt.localPosition.x <= 0) {
                rt.localPosition = position_end;
				set_panel_state(state_static);
			}
			break;
		case state_left:
			panel_rb.velocity = Vector3.left * 20 * (rt.localPosition.x - position_begin.x) ;
			// if (position_end.x - rt.localPosition.x >= 0) {
			// 	set_panel_state(state_static);
			// }
			break;
		case state_static:
			break;
		}
	}

    //设置状态板的状态并开始运动
	void set_panel_state(int state) {
		panel_state = state;
		switch (state) {
		case state_right:
            goToBeginPosition();
			panel_rb.velocity = Vector3.zero;
			break;
		case state_left:
			break;
		case state_static:
			panel_rb.velocity = Vector3.zero;
			// panel_rb.AddForce(Vector3.left * 50000);
			break;
		}
	}

    void goToBeginPosition() {
        Vector3 nowPos = this.rt.localPosition;
		nowPos.x = position_begin.x;
    }
}
