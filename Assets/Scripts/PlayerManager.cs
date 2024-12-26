using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, ICommonManger {
	public GameObject Player;
	private List<PlayerControl> Players = new List<PlayerControl>();
	public void Start () {
        BattleManger.Instance.AddCommonManger(this);
		var mainPlayer = CreatePlayer();
		Camera.main.GetComponent<MainCameraMove>().player = mainPlayer.gameObject;
		GetComponent<EnemyManager>().player = mainPlayer.gameObject;
	}

	public PlayerControl CreatePlayer()
	{
		var player = GameObject.Instantiate(Player).GetComponent<PlayerControl>();
		Players.Add(player);
		return player;
	}

	public void CommonFixedUpdate ()
	{
		Players.ForEach(p => p.CommonFixedUpdate());
	}
    public void CommonUpdate()
    {
		Players.ForEach(p => p.CommonUpdate());
    }
}
