using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mark_panel : MonoBehaviour {
	public Rigidbody panel_rb;
	public int panel_state = state_static;
	public const int state_static = 0;
	public const int state_right = 2;
	public const int state_left = 3;

	public Vector3 position_begin;
	public Vector3 position_end;

	// Use this for initialization
	void Start () {

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

		switch (panel_state) {
		case state_right:
			panel_rb.velocity = Vector3.right * 20 * (position_end.x - transform.position.x) ;
			if (position_end.x - transform.position.x <= 0) {
				set_panel_state(state_static);
			}
			break;
		case state_left:
			panel_rb.velocity = Vector3.left * 20 * (transform.position.x - position_begin.x) ;
			// if (position_end.x - transform.position.x >= 0) {
			// 	set_panel_state(state_static);
			// }
			break;
		case state_static:
			break;
		}
	}

	void set_panel_state(int state) {
		panel_state = state;
		switch (state) {
		case state_right:
			transform.position = position_begin;
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
}
