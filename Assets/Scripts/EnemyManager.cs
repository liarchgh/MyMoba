using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour, ICommonManger {
	public GameObject enemy;
	public Vector2 rangeToCreateEnemies;
	public int numberOfEnemies = 0;
	public Text scoreText;
	public float EverySecondAddEnemies = 1f;

	public int numberOfKilledEnemies = 0;
	private List<uint> _effectIDs = new List<uint>();

	// Use this for initialization
	public void Awake ()
	{
		BattleManger.Instance.AddCommonManger(this);
	}

	public void CommonStart()
	{
		numberOfKilledEnemies = 0;
		scoreText.text = numberOfKilledEnemies.ToString();
	}
	// Update is called once per frame
	public void CommonFixedUpdate () {
		//增大敌人总数量
		numberOfEnemies = (int)(Time.time * EverySecondAddEnemies);

		//删除空敌人 并且加分
		for(int i = 0; i < _effectIDs.Count; ++i) {
			if (!ActorManager.Instance.HaveActor(_effectIDs[i])) {
				_effectIDs.Remove(_effectIDs[i]);
				numberOfKilledEnemies = (int)(Time.time / 10) + 1;
				scoreText.text = numberOfKilledEnemies.ToString();
				--i;
			}
		}
		if (_effectIDs.Count < numberOfEnemies) {
			float len = Random.Range(rangeToCreateEnemies.x, rangeToCreateEnemies.y),
				angle = Random.Range(0, 360);
			Vector3 pos = new Vector3(len * Mathf.Cos(angle) + GetTarget().transform.position.x,
				300,
				len * Mathf.Sin(angle) + GetTarget().transform.position.z);
			_effectIDs.Add(ActorEnemy.Create(pos, Quaternion.identity, GetTarget()));
		}
	}
	public void CommonUpdate()
	{
	}
	public GameObject GetTarget()
	{
		return BattleManger.Instance.MainPlayer.gameObject;
	}
}
