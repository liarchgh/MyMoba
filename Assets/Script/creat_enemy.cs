using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creat_enemy : MonoBehaviour {
	public GameObject enemy;
	public Vector3 pos;
	private GameObject now;

	// Use this for initialization
	void Start () {
		now = Instantiate(enemy, pos, Quaternion.identity);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!now) {
			now = Instantiate(enemy, pos, Quaternion.identity);
		}
	}
}
