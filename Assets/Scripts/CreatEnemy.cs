using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatEnemy : MonoBehaviour {
	public GameObject enemy;
	public Vector2 rangeToCreateEnemies;
    public GameObject player;
    public int numberOfEnemies;
    public GameObject dieAnimation;

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
            Vector3 pos = new Vector3(len * Mathf.Cos(angle) + player.transform.position.x,
                300,
                len * Mathf.Sin(angle) + player.transform.position.z);
			now.Add(Instantiate(enemy, pos, Quaternion.identity) as GameObject);

            //设置脚本参数
            EnemyAI ea = now[now.Count-1].GetComponent<EnemyAI>();
            ea.player = player;

            Die die = now[now.Count - 1].GetComponent<Die>();
            die.dieAnimation = this.dieAnimation;
		}
	}
}
