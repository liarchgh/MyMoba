using System;
using UnityEngine;

[Serializable]
public class ActionParamPlayerPositionParam : ActionParamSingleParamBase<Vector3>
{
    public override bool TryGenValue(out Vector3 pos)
    {
        pos = BattleManger.Instance.MainPlayer.transform.position;
		return true;
    }
}
