using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class die : MonoBehaviour {
	public Slider hp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(hp.value <= 0){
			GameObject.Destroy(this.gameObject);
		}
	}
}
