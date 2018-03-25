using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creat_enemy : MonoBehaviour {
	public GameObject enemy;
	public Vector2 rangeToCreateEnemies;
    public GameObject player;
    public int numberOfEnemies;

	private List<GameObject> now = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {
        for(int i = 0; i < now.Count; ++i) {
            if (!now[i]) {
                now.Remove(now[i]);
            }
        }
		if (now.Count < numberOfEnemies) {
            float len = Random.Range(rangeToCreateEnemies.x, rangeToCreateEnemies.y),
                angle = Random.Range(0, 360);
            Vector3 pos = new Vector3(len * Mathf.Cos(angle)+player.transform.position.x,
                len * Mathf.Sin(angle)+player.transform.position.y,
                300);
			now.Add(Instantiate(enemy, pos, Quaternion.identity) as GameObject);
            EnemyAI ea = now[now.Count-1].GetComponent<EnemyAI>();
            ea.player = player;
		}
	}
}
