using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour, ICommonManger {
	public uint ID { get; set; }
	public SkillComponent SkillComponent = new SkillComponent();
	public GameObject PlayerPositionPrefab;
	private GameObject player_position;
	public float player_high = 0.6f;//高度为2时会乱飞 loading
	public float Speed = 8;
	public Rigidbody player_rb => GetComponent<Rigidbody>();
	public LayerMask layer_Terrain;
	public Slider HPSlider;
	public HPComponent HPComponent = new HPComponent();

	public void CommonStart()
	{
		// layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
		player_position = GameObject.Instantiate(PlayerPositionPrefab);
	}
	public void CommonUpdate () {
		//将player设置为指定的高度 将光标设置到player的脚下
		Ray set_high_ray = new Ray(new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Vector3.down);
		RaycastHit player_point;
		if (Physics.Raycast(set_high_ray, out player_point, 60f, layer_Terrain.value))
		{
			Vector3 player_now_position = player_point.point;
			player_position.transform.position = player_now_position;
			player_now_position.y += player_high;
			transform.position = player_now_position;
		}
		SkillComponent.CommonUpdate();
	}

	public void CommonFixedUpdate() {
		//摁下S键player停止活动
		if (Input.GetKeyDown(KeyCode.S)) {
			// set_player_state(state_static);
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
				// set_player_state(state_static);
				// player_rb.velocity = (target_position - transform.position).normalized * Speed;
			}
		}
		SkillComponent.CommonFixedUpdate();
	}
}
