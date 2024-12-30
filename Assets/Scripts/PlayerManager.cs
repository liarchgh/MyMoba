using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICommonManger {
	public static PlayerManager Instance { get; private set; }
	public uint MainPlayerID { get; private set; }
	public PlayerControl MainPlayer => _players[MainPlayerID];
	public GameObject Player;
	private IDGenerateComponent _iDGenerateComponent = new IDGenerateComponent();
	private Dictionary<uint, PlayerControl> _players = new Dictionary<uint, PlayerControl>();
	public void Start () {
		Instance = this;
        BattleManger.Instance.AddCommonManger(this);
		var mainPlayer = CreatePlayer();
		MainPlayerID = mainPlayer.ID;
		Camera.main.GetComponent<MainCameraMove>().player = mainPlayer.gameObject;
		GetComponent<EnemyManager>().player = mainPlayer.gameObject;
	}

	public PlayerControl CreatePlayer()
	{
		var player = GameObject.Instantiate(Player).GetComponent<PlayerControl>();
		player.ID = _iDGenerateComponent.GenID();
		_players.Add(player.ID, player);
		return player;
	}

	public void CommonFixedUpdate ()
	{
		// TODO: 不同角色时序不能有影响
		_players.ForEach(p => p.Value.CommonFixedUpdate());
	}
    public void CommonUpdate()
    {
		_players.ForEach(p => p.Value.CommonUpdate());
    }
}
