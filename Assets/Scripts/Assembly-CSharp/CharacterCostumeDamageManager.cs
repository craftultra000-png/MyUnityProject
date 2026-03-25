using System.Collections.Generic;
using PaintCore;
using UnityEngine;

public class CharacterCostumeDamageManager : MonoBehaviour
{
	public CharacterCostumeManager _characterCostumeManager;

	public TorsoManager _torsoManager;

	[Header("Clothe Data")]
	public List<float> clotheLife;

	public List<float> breakRatio;

	[Header("Reset Wait")]
	public float resetWait;

	public float resetWaitMax = 1f;

	[Header("Channel Data")]
	public List<CharacterCostumeObject> _characterCostumeObject;

	public List<CwChannelCounter> counters;

	public List<long> clotheCurrent;

	public List<long> clotheCalc;

	public List<long> clotheMax;

	private void Start()
	{
		clotheLife.Clear();
		clotheCurrent.Clear();
		clotheCalc.Clear();
		clotheMax.Clear();
		for (int i = 0; i < _characterCostumeManager.clotheModel.Count; i++)
		{
			clotheLife.Add(1f);
			clotheCurrent.Add(0L);
			clotheCalc.Add(0L);
			clotheMax.Add(0L);
		}
	}

	private void LateUpdate()
	{
		if (resetWait > 0f)
		{
			resetWait -= Time.deltaTime;
		}
		for (int i = 0; i < counters.Count; i++)
		{
			if (clotheLife[i] > 0f)
			{
				List<CwChannelCounter> list = new List<CwChannelCounter> { counters[i] };
				clotheMax[i] = CwChannelCounter.GetTotal(list);
				clotheCalc[i] = CwChannelCounter.GetCountA(list);
				if (resetWait <= 0f && clotheCurrent[i] != clotheCalc[i])
				{
					clotheCurrent[i] = clotheCalc[i];
					float num = (float)clotheMax[i] * breakRatio[i];
					float num2 = (float)clotheMax[i] - num;
					float num3 = (float)clotheCurrent[i] - num;
					clotheLife[i] = num3 / num2;
					SetClotheDamage(i, clotheLife[i]);
				}
			}
		}
	}

	public void GetCounters()
	{
		counters.Clear();
		breakRatio.Clear();
		for (int i = 0; i < _characterCostumeObject.Count; i++)
		{
			counters.Add(_characterCostumeObject[i].counter);
			breakRatio.Add(_characterCostumeObject[i].breakRatio);
		}
	}

	public void ResetClothe(int value)
	{
		if (clotheLife.Count > 0)
		{
			resetWait = resetWaitMax;
			clotheLife[value] = 1f;
			_torsoManager.SetClotheDamage(value, clotheLife[value]);
			_characterCostumeObject[value].painter.Clear();
		}
	}

	public void AllResetClothe()
	{
		resetWait = resetWaitMax;
		for (int i = 0; i < clotheLife.Count; i++)
		{
			clotheLife[i] = 1f;
			_torsoManager.SetClotheDamage(i, clotheLife[i]);
			_characterCostumeObject[i].painter.Clear();
		}
	}

	public void BreakClothe(int value)
	{
		clotheLife[value] = 0f;
		_torsoManager.SetClotheDamage(value, clotheLife[value]);
		CwCommandFill.Instance.SetMaterial(CwBlendMode.AlphaBlend(Vector4.one), _characterCostumeObject[value].painter.Texture, Color.black, 1f, 0f);
	}

	public void SetClotheDamage(int value, float life)
	{
		if (life > 1f)
		{
			clotheLife[value] = 1f;
		}
		else if (life < 0f)
		{
			clotheLife[value] = 0f;
			_characterCostumeManager.SetCostumeMelt(value);
		}
		if (_torsoManager != null)
		{
			_torsoManager.SetClotheDamage(value, life);
		}
	}
}
