using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_height : MonoBehaviour {
	public LayerMask layer_Terrain;
	public float height = 1.5f;

	// Use this for initialization
	void Start () {
        //设置为地形层
		layer_Terrain = 1 << LayerMask.NameToLayer("Terrain");
	}
	
	// Update is called once per frame
	void Update () {
        //向上发射射线 找地形高度
		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit the_point;
		if(Physics.Raycast(ray, out the_point, 600f, layer_Terrain.value)){
			Vector3 now_position = the_point.point;
			now_position.y += height;
			transform.position = now_position;
		}
        //向下发射射线 找地形高度
        else {
		ray = new Ray(transform.position, Vector3.up);
    		if(Physics.Raycast(ray, out the_point, 600f, layer_Terrain.value)){
    			Vector3 now_position = the_point.point;
    			now_position.y += height;
    			transform.position = now_position;
    		}
        }
    }
}
