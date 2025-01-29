using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkill1 : MonoBehaviour {
	public float rotate_speed = 600f;
	public uint skill1_attack = 500;

	// Use this for initialization
	void Start () {
        float initAngle = Random.Range(0.0f, 360f);
		transform.RotateAround(this.transform.parent.transform.position, Vector3.up, initAngle);
	}

	// Update is called once per frame
	void Update () {
		//transform.Rotate(0, rotate_speed * Time.deltaTime, 0, this.transform.parent.gameObject.Space);
		transform.RotateAround(this.transform.parent.transform.position, Vector3.up, rotate_speed * Time.deltaTime);
	}

	void FixedUpdate() {

	}

	void OnTriggerEnter(Collider obj) {
		if(obj.gameObject.TryGetComponent<HPComponent>(out var hpc))
			hpc.RemoveHP(skill1_attack);
	}
}
