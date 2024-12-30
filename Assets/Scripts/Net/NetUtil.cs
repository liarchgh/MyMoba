
using System.Collections.Generic;
using System.Linq;

public class NetUtil
{
	public void HandleSkillInput(List<string> inputs)
	{
		var datas = inputs.Select(x=>x.Deserialize<SkillRunData>());
		BattleManger.Instance.MainPlayer.SkillComponent.AddSkillRunDatas(datas);
	}
}