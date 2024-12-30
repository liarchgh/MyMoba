using System;
using UnityEngine;

[Serializable]
public class ActionParamMousePositionParam : ActionParamSingleParamBase<Vector3>
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
