using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSlider : MonoBehaviour {
	public Camera MainCam;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.LookAt(MainCam.transform.position - transform.position);
	}
}
