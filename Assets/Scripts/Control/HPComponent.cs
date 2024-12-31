using System;
using UnityEngine;

[Serializable]
public class HPComponent
{
	[SerializeField]
	private uint m_maxHP;
	public uint HPValue { get; private set; }

	public void AddHP(uint hpChange)
	{
		HPValue += MathUtil.Min(hpChange, m_maxHP-HPValue);
	}
	public void RemoveHP(uint hpChange)
	{
		HPValue -= MathUtil.Min(HPValue, hpChange);
	}
}