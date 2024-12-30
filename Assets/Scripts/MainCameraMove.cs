using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCameraMove : MonoBehaviour {
	public bool lock_cam = true;//是否锁定视角
	public float speed = 40f;//视角移动速度
	private float cam_high = 15;//视角高度
	public float cam_high_max = 20;//视角最大高度
	public float cam_high_min = 5;//视角最小高度
	public float set_high_speed = 50;//滚轮设定视角高度的速度
	private Vector3 dis_plus = new Vector3(0, 15, -20);//默认视角比例 锁定视角使用

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//设定视角是否锁定
		if (Input.GetKeyDown(KeyCode.Y)) {
			lock_cam = !lock_cam;
		}

		//如果锁定或者按下空格键设定视角位置到物体 否则判断鼠标位置并移动视角
		if (Input.GetKey(KeyCode.Space) || lock_cam) {
			transform.position = GetTarget().transform.position + dis_plus;
		}
		else {
			if (Input.mousePosition.x >= Screen.width * 0.99f) {
				transform.Translate(Vector3.right * speed * Time.deltaTime * cam_high / cam_high_max, Space.World);
			}
			else if (Input.mousePosition.x <= Screen.width * 0.01f) {
				transform.Translate(Vector3.left * speed * Time.deltaTime * cam_high / cam_high_max, Space.World);
			}
			if (Input.mousePosition.y >= Screen.height * 0.99f) {
				transform.Translate(Vector3.forward * speed * Time.deltaTime * cam_high / cam_high_max, Space.World);
			}
			else if (Input.mousePosition.y <= Screen.height * 0.01f) {
				transform.Translate(Vector3.back * speed * Time.deltaTime * cam_high / cam_high_max, Space.World);
			}
		}

		//用滚轮设定视角高度 判断并处理超出范围
		cam_high -= Input.GetAxis("Mouse ScrollWheel") * set_high_speed;
		if (cam_high > cam_high_max) cam_high = cam_high_max;
		else if (cam_high < cam_high_min) cam_high = cam_high_min;

		//将视角高度设定为设定值
		LayerMask layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
		Ray target_ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
		RaycastHit target_hit;
		if (Physics.Raycast(target_ray, out target_hit, 600f, layer_Terrain.value)) {
			Vector3 cam_set_high = transform.position - target_hit.point;
			cam_set_high *= cam_high / cam_set_high.y ;
			transform.position = cam_set_high + target_hit.point;
		}
	}
	public GameObject GetTarget()
	{
		return BattleManger.Instance.MainPlayer.gameObject;
	}
}
