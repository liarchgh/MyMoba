using UnityEngine;

[RequireComponent(typeof(HPComponent))]
public class PlayerControl : MonoBehaviour, ICommonManger {
	[SerializeField]
	public SkillComponent SkillComponent = new SkillComponent();
	[SerializeField]
	private GameObject PlayerPositionPrefab;
	private GameObject player_position;
	[SerializeField]
	private float player_high = 0.6f;//高度为2时会乱飞 loading
	[SerializeField]
	public float Speed = 8;
	[SerializeField]
	private LayerMask layer_Terrain;

	public void CommonStart()
	{
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
		SkillComponent.CommonFixedUpdate();
	}
}
