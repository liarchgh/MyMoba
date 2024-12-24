using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public GameObject player;
    public float moveSpeed = 1;
    public float limitLength = 40;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player) {
            Vector3 vPos = player.transform.position - this.transform.position;
            if(vPos.sqrMagnitude > limitLength) {
                rb.linearVelocity = moveSpeed * Vector3.Normalize(vPos);
            }
            else {
                rb.linearVelocity = Vector3.zero;
            }
        }
	}
}
