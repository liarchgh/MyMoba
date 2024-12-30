using System;
using UnityEngine;

[Serializable]
// TODO: 可以删了吧，直接用父类的泛型
public abstract class ActionParamSinglePositionParamBase : ActionParamSingleParamBase<Vector3> { }
[Serializable]
public class ActionParamPlayerPositionParam : ActionParamSinglePositionParamBase
{
    public override bool TryGenValue(out Vector3 pos)
    {
        pos = PlayerControl.Instance.transform.position;
		return true;
    }
}
[Serializable]
public class ActionParamMousePositionParam : ActionParamSinglePositionParamBase
{
	public LayerMask layer_Terrain;
    public override bool TryGenValue(out Vector3 Value)
    {
		var target_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(target_ray, out var target_hit, 600f, layer_Terrain.value))
		{
			Value = target_hit.point;
			return true;
		}
		else
		{
			Value = default;
			return false;
		}
    }
}
