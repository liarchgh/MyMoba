using UnityEngine;

public class ActionLogicGameObjectEntity : EntityBase
{
	public GameObject GO;
	public override void Clear()
	{
		GameObject.Destroy(GO);
	}
}