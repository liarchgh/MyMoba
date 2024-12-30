using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICommonManger {
	public static PlayerManager Instance { get; private set; }
	public GameObject Player;
	private IDGenerateComponent _iDGenerateComponent = new IDGenerateComponent();
	private Dictionary<uint, PlayerControl> _players = new Dictionary<uint, PlayerControl>();
	public void Awake()
	{
		BattleManger.Instance.AddCommonManger(this);
	}

	public uint CreatePlayer()
	{
		var player = GameObject.Instantiate(Player).GetComponent<PlayerControl>();
		player.ID = _iDGenerateComponent.GenID();
		player.CommonStart();
		_players.Add(player.ID, player);
		return player.ID;
	}
	public bool TryGetPlayerControl(uint pid, out PlayerControl player)
	{
		return _players.TryGetValue(pid, out player);
	}
	public PlayerControl GetPlayerControl(uint pid)
	{
		if(!TryGetPlayerControl(pid, out var player))
			return default;
		return player;
	}

	public void CommonStart()
	{
		Instance = this;
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
