using System;
using UnityEngine;

[Serializable]
public class ActionParamPlayerPositionParam : ActionParamSingleParamBase<Vector3>
{
    public override bool TryGenValue(out Vector3 pos)
    {
        pos = PlayerControl.Instance.transform.position;
		return true;
    }
}
