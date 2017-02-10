using UnityEngine;
using System.Collections;

public class JianTouMove : MonoBehaviour {
	public GameObject JianTou;
	public Vector3 JianTou_pos;
	public float JianTou_high = 0.1f;
	public const int state_static = 0;
	public const int state_move = 1;
	public float player_high = 0.6f;//高度为2时会乱飞 loading
	//public GameObject clickPont;
	public float Speed = 8;
	private int player_state = 0;
	//private float dis_last = -555;
	private Vector3 target_position;
	public Rigidbody player_rb;

	void Start () {
		player_state = state_static;
	}

	void Update () {
		SetTarget();
	}

	public void FixedUpdate() {
		// Debug.Log(player_state);
		switch (player_state) {
		case state_static:
			break;
		case state_move:
			//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
			//float dis_now = Mathf.Abs(Vector3.Distance(target_position, transform.position));
			// if (dis_now <= dis_last || dis_last< 0) {
			// 	// transform.Translate(target_position - transform.position);
			// 	player_rb.velocity = (target_position - transform.position).normalized * Speed;
			// 	// player_rb.MovePosition(target_position);
			// 	dis_last = dis_now;
			// } else {
			// 	player_state = state_static;
			// 	player_rb.velocity = Vector3.zero;
			// 	dis_last = -555;
			// }
			target_position.y = transform.position.y;
			if (Mathf.Abs(Vector3.Distance(target_position, transform.position)) < 0.2f) {
				player_state = state_static;
				player_rb.velocity = Vector3.zero;

				// JianTou.transform.Translate(JianTou_pos - JianTou.transform.position);
			}
			else {
				transform.Translate(target_position - transform.position);
			}
			break;
		}
		RaycastHit point;
		if (Physics.Raycast(transform.position, Vector3.down, out point, 6000)) {
			Vector3 player_position = point.point;
			player_position.y += player_high;
			transform.Translate(player_position - transform.position);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			player_rb.velocity = Vector3.zero;
			player_state = state_static;
		}
	}

	public void SetTarget() {
		if (Input.GetMouseButtonDown(1)) {
			// Debug.Log(2);
			LayerMask layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
			Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit target_hit;
			if (Physics.Raycast(target_ray, out target_hit, 60000, layer_Terrain.value)) {
				target_position = target_hit.point;
				player_state = state_move;
			}
		}
	}
}