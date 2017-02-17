using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour {
	public GameObject click_rig;
	public GameObject skill1;
	public GameObject skill2;
	public float skill2_last_time = 4.0f;
	public float skill3 = 5;
	public GameObject player_position;
	public Vector3 default_position = new Vector3(0, 2000f, 0);
	public float player_position_high = 0.1f;
	public const int state_static = 0;
	public const int state_move = 1;
	public float player_high = 0.6f;//高度为2时会乱飞 loading
	//public GameObject clickPont;
	public float Speed = 8;
	public float skill1_speed = 50;
	private int skill1_state = state_static;
	private int player_state = state_static;
	private float dis_last = -555;
	private Vector3 target_position;
	private Vector3 skill1_target_position;
	public Rigidbody player_rb;
	public Rigidbody skill1_rb;
	public LayerMask layer_Terrain;
	private float skill1_dis_last = -55;
	private float skill2_time = -55;


	void Start () {
		// layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
		player_state = state_static;
	}

	void Update () {
		//将player设置为指定的高度 将光标设置到player的脚下
		Ray set_high_ray = new Ray(transform.position, Vector3.down);
		RaycastHit player_point;
		if (Physics.Raycast(set_high_ray, out player_point, 600f, layer_Terrain.value)) {
			Vector3 player_now_position = player_point.point;
			player_position.transform.position = player_now_position;
			player_now_position.y += player_high;
			transform.position = player_now_position;
		}
	}

	void FixedUpdate() {
		//读取鼠标右键设置的目标位置
		SetTarget();

		//player不同状态下的不同处理方法
		switch (player_state) {
		case state_static:
			break;
		case state_move:
			//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
			float dis_now = Mathf.Abs(Vector3.Distance(target_position, transform.position));
			if ((dis_now <= dis_last || dis_last < 0) && dis_now > 0.2f) {
				// transform.Translate(target_position - transform.position);
				target_position.y = transform.position.y;
				// player_rb.velocity = (target_position - transform.position).normalized * Speed;
				// player_rb.MovePosition(target_position);
				dis_last = dis_now;
			} else {
				set_player_state(state_static);
			}
			// target_position.y = transform.position.y;
			// if (Mathf.Abs(Vector3.Distance(target_position, transform.position)) < 0.2f) {
			// 	player_state = state_static;
			// 	player_rb.velocity = Vector3.zero;

			// 	// player_position.transform.Translate(default_position - player_position.transform.position);
			// }
			// else {
			// 	player_rb.velocity = (target_position - transform.position).normalized * Speed;
			// }
			break;
		}

		//摁下S键player停止活动
		if (Input.GetKeyDown(KeyCode.S)) {
			set_player_state(state_static);
		}

		//qwer技能
		if (Input.GetKeyDown(KeyCode.Q)) {
			set_skill1_state(state_move);
		}
		switch (skill1_state) {
		case state_move:
			float dis_now = Mathf.Abs(Vector3.Distance(skill1_target_position, skill1.transform.position));
			if (dis_now <= skill1_dis_last || skill1_dis_last < 0) {
				skill1_target_position.y = skill1.transform.position.y;
				// skill1_rb.velocity = (skill1_target_position - skill1.transform.position).normalized * skill1_speed;
				skill1_dis_last = dis_now;
			} else {
				set_skill1_state(state_static);
			}
			break;
		case state_static:
			break;
		}

		if(Input.GetKeyDown(KeyCode.W)){
			Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit target_hit;
			if (Physics.Raycast(target_ray, out target_hit, 600f, layer_Terrain.value)) {
				Vector3 pos = target_hit.point;
				skill2.transform.position = pos;
				skill2_time = Time.time;
			}
		}
		if(skill2_time >= 0){
			if(Time.time > skill2_time + skill2_last_time){
				skill2.transform.position = default_position;
				skill2_time = -55;
			}
		}
		if(Input.GetKeyDown(KeyCode.E)){
			Speed += skill3;
		}
		if(Input.GetKeyDown(KeyCode.X)){
			Speed = 8;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit target_hit;
			if (Physics.Raycast(target_ray, out target_hit, 600f, layer_Terrain.value)) {
				Vector3 pos = target_hit.point;
				pos.y += player_high;
				transform.position = pos;
				set_player_state(state_static);
				// player_rb.velocity = (target_position - transform.position).normalized * Speed;
			}
		}
	}

	public void set_skill1_state(int state) {
		skill1_state = state_move;
		switch (state) {
		case state_move:
			skill1_dis_last = -55;
			Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit target_hit;
			Vector3 pos;
			if (Physics.Raycast(target_ray, out target_hit, 600f, layer_Terrain.value)) {
				pos = target_hit.point;
				skill1_target_position = pos;
				skill1.transform.position = transform.position;
			}
			skill1_target_position.y = skill1.transform.position.y;
			skill1_rb.velocity = (skill1_target_position - skill1.transform.position).normalized * skill1_speed;
			break;
		case state_static:
			skill1_dis_last = -55;
			skill1.transform.position = default_position;
			break;
		}
	}

	public void set_player_state(int state) {
		player_state = state;
		switch (state) {
		case state_static:
			player_rb.velocity = Vector3.zero;
			dis_last = -555;
			click_rig.transform.position = default_position;
			break;
		case state_move:
			dis_last = -55;
			Ray target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit target_hit;
			if (Physics.Raycast(target_ray, out target_hit, 600f, layer_Terrain.value)) {
				if (Mathf.Abs(Vector3.Distance(transform.position, target_hit.point)) > 1.5f) {
					target_position = target_hit.point;
					click_rig.transform.position = target_position;
					player_state = state_move;
				}
			}
			target_position.y = transform.position.y;
			player_rb.velocity = (target_position - transform.position).normalized * Speed;
			break;
		}
	}

	public void SetTarget() {
		if (Input.GetMouseButton(1)) {
			set_player_state(state_move);
			// Debug.Log(2);
		}
	}
}