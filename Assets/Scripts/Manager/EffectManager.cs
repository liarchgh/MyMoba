using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour, ICommonManger
{
	public static EffectManager Instance { get; private set; }
	private List<EffectEntity> _effects = new List<EffectEntity>();
	public void Awake ()
	{
		BattleManger.Instance.AddCommonManger(this);
	}
	public void CommonStart()
	{
		Instance = this;
	}

	public void CommonUpdate()
	{
	}

	public void CommonFixedUpdate()
	{
		_effects.Where(x=>x.NeedClear()).ForEach(x=>x.Clear());
		_effects = _effects.Where(x=>!x.NeedClear()).ToList();
	}
	public void AddEffect(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, double startTime, double timeLength)
	{
		var go = GameObject.Instantiate(prefab, position, rotation, parent);
		_effects.Add(new EffectEntity(go, startTime, timeLength));
	}
	private class EffectEntity: EntityBase
	{
		public GameObject GO;
		public double StartTime;
		public double TimeLength;
		public EffectEntity(GameObject prefab, double startTime, double timeLength)
		{
			GO = prefab;
			StartTime = startTime;
			TimeLength = timeLength;
		}
		public bool NeedClear()
		{
			return StartTime + TimeLength > TimeUtil.GetTime();
		}
		public override void Clear()
		{
			Destroy(GO);
		}
	}
}