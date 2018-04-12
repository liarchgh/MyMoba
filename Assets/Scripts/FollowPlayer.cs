using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Transform thisObj;
    public Transform player;

	// Use this for initialization
	void Start () {
        thisObj = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        thisObj.transform.SetPositionAndRotation(
            new Vector3(player.position.x, thisObj.position.y, player.position.z),
            Quaternion.identity);
	}
}
