using System;
using UnityEngine;

[Serializable]
public abstract class ActionParamSinglePositionParamBase : ActionParamSingleParamBase<Vector3> { }
[Serializable]
public class ActionParamPlayerPositionParam : ActionParamSinglePositionParamBase
{
    public override bool TryGenValue()
    {
        Value = PlayerControl.Instance.transform.position;
		return true;
    }
}
[Serializable]
public class ActionParamMousePositionParam : ActionParamSinglePositionParamBase
{
	public LayerMask layer_Terrain;
    public override bool TryGenValue()
    {
		var target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(target_ray, out var target_hit, 600f, layer_Terrain.value))
		{
			Value = target_hit.point;
			return true;
		}
		else
		{
			return false;
		}
    }
}
