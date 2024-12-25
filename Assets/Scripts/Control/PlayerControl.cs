using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour, ICommonManger {
	public static PlayerControl Instance { get; private set; }
	public SkillComponent SkillComponent = new SkillComponent();
	public GameObject click_rig;
	public GameObject player_position;
	private static Vector3 default_position = new Vector3(0, 2000f, 0);
	public float player_position_high = 0.1f;
	private const int state_static = 0;
	private const int state_move = 1;
	public float player_high = 0.6f;//高度为2时会乱飞 loading
	//public GameObject clickPont;
	public float Speed = 8;
	private int player_state = state_static;
	private float dis_last = -555;
	private Vector3 target_position;
	public Rigidbody player_rb => GetComponent<Rigidbody>();
	public LayerMask layer_Terrain;

	void Start () {
		// layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
		BattleManger.Instance.AddCommonManger(this);
		player_state = state_static;
		Instance = this;
	}

	public void CommonUpdate () {
		//将player设置为指定的高度 将光标设置到player的脚下
		Ray set_high_ray = new Ray(new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Vector3.down);
		RaycastHit player_point;
		if (Physics.Raycast(set_high_ray, out player_point, 60f, layer_Terrain.value)) {
			Vector3 player_now_position = player_point.point;
			player_position.transform.position = player_now_position;
			player_now_position.y += player_high;
			transform.position = player_now_position;
		}
		SkillComponent.CommonUpdate();
	}

	public void CommonFixedUpdate() {
		//读取鼠标右键设置的目标位置
		SetTarget();

		//player不同状态下的不同处理方法
		switch (player_state) {
		case state_static:
			break;
		case state_move:
			//用和目的地距离变远作为终止条件 但是在碰撞后被弹回会停止
			target_position.y = transform.position.y;
			float dis_now = Mathf.Abs(Vector3.Distance(target_position, transform.position));
			if ((dis_now <= dis_last || dis_last < 0) && dis_now > 0.2f) {
				// transform.Translate(target_position - transform.position);
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

		if (Input.GetKeyDown(KeyCode.X)) {
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
		SkillComponent.CommonFixedUpdate();
	}

	public void set_player_state(int state) {
		player_state = state;
		switch (state) {
		case state_static:
			player_rb.linearVelocity = Vector3.zero;
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
			player_rb.linearVelocity = (target_position - transform.position).normalized * Speed;
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
