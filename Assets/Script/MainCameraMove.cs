using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCameraMove : MonoBehaviour {
	public GameObject player;
	public bool lock_cam = true;
	public float speed = 40f;
	private float cam_high = 15;
	public float cam_high_max = 20;
	public float cam_high_min = 5;
	public float set_high_speed = 50;
	private Vector3 dis_plus = new Vector3(0, 15, -20);

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Y)) {
			lock_cam = !lock_cam;
		}
		if (Input.GetKey(KeyCode.Space) || lock_cam) {
			transform.position = player.transform.position + dis_plus;
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

		cam_high -= Input.GetAxis("Mouse ScrollWheel") * set_high_speed;
		if (cam_high > cam_high_max) cam_high = cam_high_max;
		else if (cam_high < cam_high_min) cam_high = cam_high_min;

		LayerMask layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
		Ray target_ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
		RaycastHit target_hit;
		if (Physics.Raycast(target_ray, out target_hit, 60000f, layer_Terrain.value)) {
			Debug.Log(11);
			Vector3 cam_set_high = transform.position - target_hit.point;
			cam_set_high *= cam_high / cam_set_high.y ;
			transform.position = cam_set_high + target_hit.point;
		}
	}
}
