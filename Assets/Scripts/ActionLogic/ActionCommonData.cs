using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Action
{
	public class ActionCommonData
	{
		private Dictionary<ActionCommonDataType, List<ActionStatusBase>> _dataSources =
			new Dictionary<ActionCommonDataType, List<ActionStatusBase>>();
		public void AddData(ActionCommonDataType dataType, ActionStatusBase actionStatus)
		{
			if(!_dataSources.TryGetValue(dataType, out var sources))
			{
				sources = new List<ActionStatusBase>();
				_dataSources.Add(dataType, sources);
			}
			sources.Add(actionStatus);
		}
		public void RemoveData(ActionStatusBase actionStatus)
		{
			_dataSources.Values.ForEach(x => x.Remove(actionStatus));
		}
		public bool TryGetDataValue(ActionCommonDataType dataType, out float value)
		{
			if(!_dataSources.TryGetValue(dataType, out var sources)
				|| sources.Count <= 0)
			{
				value = 0;
				return false;
			}
			value = sources
				.Where(x => x.HaveData(dataType))
				.Sum(x => x.GetDataValue(dataType));
			return true;
		}
	}
	public enum ActionCommonDataType
	{
		Speed,
	}
}