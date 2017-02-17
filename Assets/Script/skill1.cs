using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill1 : MonoBehaviour {
	public float rotate_speed = 4000f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0, rotate_speed * Time.deltaTime, 0, Space.Self);
	}

	void FixedUpdate() {

	}

	void OnCollisionExit(Collision obj){
		obj
	}
}
