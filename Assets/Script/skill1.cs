using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill1 : MonoBehaviour {
	public float rotate_speed = 4000f;
	public float skill1_attack = 500f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0, rotate_speed * Time.deltaTime, 0, Space.Self);
	}

	void FixedUpdate() {

	}

	void OnTriggerEnter(Collider obj) {
		Debug.Log("ENter");
		obj.gameObject.GetComponentInChildren<Slider>().value -= skill1_attack;
	}
}
