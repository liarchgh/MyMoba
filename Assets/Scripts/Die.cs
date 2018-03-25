using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour {
	public Slider hp;
    public GameObject dieAnimation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(hp.value <= 0){
            Instantiate(dieAnimation, this.transform.position, Quaternion.identity);
			GameObject.Destroy(this.gameObject);
		}
	}
}
