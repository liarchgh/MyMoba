
using System.Collections.Generic;
using System.Linq;

public class NetUtil
{
	public void HandleSkillInput(List<string> inputs)
	{
		var datas = inputs.Select(x=>x.Deserialize<SkillRunData>());
		PlayerControl.Instance.SkillComponent.AddSkillRunDatas(datas);
	}
}