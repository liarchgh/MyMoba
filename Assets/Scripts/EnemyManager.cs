using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour, ICommonManger {
	public GameObject enemy;
	public Vector2 rangeToCreateEnemies;
	public int numberOfEnemies = 0;
	public GameObject dieAnimation;
	public Text scoreText;
	public float EverySecondAddEnemies = 1f;

	public int numberOfKilledEnemies = 0;

	private List<GameObject> now = new List<GameObject>();

	// Use this for initialization
	public void Start () {
		BattleManger.Instance.AddCommonManger(this);
		numberOfKilledEnemies = 0;
		scoreText.text = numberOfKilledEnemies.ToString();
	}

	// Update is called once per frame
	public void CommonFixedUpdate () {
		//增大敌人总数量
		numberOfEnemies = (int)(Time.time * EverySecondAddEnemies);

		//删除空敌人 并且加分
		for(int i = 0; i < now.Count; ++i) {
			if (!now[i]) {
				now.Remove(now[i]);
				numberOfKilledEnemies = (int)(Time.time / 10) + 1;
				scoreText.text = numberOfKilledEnemies.ToString();
				--i;
			}
		}
		if (now.Count < numberOfEnemies) {
			float len = Random.Range(rangeToCreateEnemies.x, rangeToCreateEnemies.y),
				angle = Random.Range(0, 360);
			Vector3 pos = new Vector3(len * Mathf.Cos(angle) + GetTarget().transform.position.x,
				300,
				len * Mathf.Sin(angle) + GetTarget().transform.position.z);
			now.Add(Instantiate(enemy, pos, Quaternion.identity) as GameObject);

			//设置脚本参数
			EnemyAI ea = now[now.Count-1].GetComponent<EnemyAI>();
			ea.player = GetTarget();

			Die die = now[now.Count - 1].GetComponent<Die>();
			die.dieAnimation = this.dieAnimation;
		}
	}
	public void CommonUpdate()
	{
	}
	public GameObject GetTarget()
	{
		return PlayerManager.Instance.MainPlayer.gameObject;
	}
}
