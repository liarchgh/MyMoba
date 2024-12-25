using System.Collections.Generic;
using UnityEngine;

public interface ICommonManger
{
	public void CommonUpdate();
	public void CommonFixedUpdate();
}
public class BattleManger : MonoBehaviour {
	public static BattleManger Instance {get; private set;}
	private List<ICommonManger> _commonMangers = new List<ICommonManger>();
	void Awake()
	{
		Instance = this;
	}
	void Start ()
	{
	}
	void Update ()
	{
		_commonMangers.ForEach(m => m.CommonUpdate());
	}
	void FixedUpdate ()
	{
		_commonMangers.ForEach(m => m.CommonFixedUpdate());
	}
	public void AddCommonManger(ICommonManger commonManger)
	{
		_commonMangers.Add(commonManger);
	}
}