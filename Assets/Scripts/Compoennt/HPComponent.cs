using System;
using UnityEngine;
using UnityEngine.UI;

public class HPComponent: MonoBehaviour
{
	[SerializeField]
	private Slider m_hpSlider;
	[SerializeField]
	private uint m_maxHP;
	[SerializeField]
	private GameObject m_dieAnimationPrefab;
	[SerializeField]
	private float m_dieAnimationTimeLength;
	public uint HPValue { get; private set; }
	public Action<HPComponent> OnDie;
	void Awake() {
		HPValue = m_maxHP;
	}

	public void AddHP(uint hpChange)
	{
		HPValue += MathUtil.Min(hpChange, m_maxHP-HPValue);
		SyncValueToSlider();
	}
	public void RemoveHP(uint hpChange)
	{
		HPValue -= MathUtil.Min(HPValue, hpChange);
		SyncValueToSlider();
		if(HPValue <= 0){
			EffectManager.Instance.AddEffect(m_dieAnimationPrefab, null, transform.position,
				Quaternion.identity, TimeUtil.GetTime(), m_dieAnimationTimeLength);
			OnDie(this);
		}
	}
	private void SyncValueToSlider()
	{
		m_hpSlider.value = HPValue;
	}
}
