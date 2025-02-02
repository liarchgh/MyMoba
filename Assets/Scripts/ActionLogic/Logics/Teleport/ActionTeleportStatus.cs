using System.Collections.Generic;

public class ActionTeleportStatus: ActionStatusBase
{
	public List<ActionLogicGameObjectEntity> _entities = new List<ActionLogicGameObjectEntity>();
	public bool _moving = false;
	public float dis_last;
}
