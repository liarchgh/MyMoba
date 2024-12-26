using System;
using UnityEngine;

[Serializable]
public abstract class ActionLogicBase
{
	public abstract bool PreCheckLogic();
	public abstract bool FixedUpdate();
	public abstract void DoLogic();
	public abstract void Clear();
}
[Serializable]
public abstract class ActionLogicWithParamBase<T>: ActionLogicBase where T : ActionParam
{
	public T ActionParam;
	public override bool PreCheckLogic()
	{
		return ActionParam.TryGenParam();
	}
}

[Serializable]
public abstract class ActionParam
{
	public abstract bool TryGenParam();
}

public abstract class ActionParamSingleParamBase<T>
{
	[NonSerialized]
	public T Value;
	public abstract bool TryGenValue();
}
public abstract class ActionParamSinglePositionParamBase : ActionParamSingleParamBase<Vector3> { }
public class ActionParamPlayerPositionParam : ActionParamSinglePositionParamBase
{
    public override bool TryGenValue()
    {
        Value = PlayerControl.Instance.transform.position;
		return true;
    }
}
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