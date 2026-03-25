using System.Collections.Generic;
using UnityEngine;

public class FilamentVaginaOpenObject : MonoBehaviour
{
	public List<FeelerFilamentObject> _feeler;

	public List<FeelerFilamentObject> _filament;

	[Header("Status")]
	public bool isAim;

	public bool isAimEndCheck;

	public bool isAimEnd;

	[Space]
	public bool isFeeler;

	public bool isOpenReady;

	public bool isOpen;

	public bool isClose;

	[Header("Target")]
	public SkinnedMeshRenderer _body;

	public List<SkinnedMeshRenderer> _openMesh;

	public List<Transform> openTargetL;

	public List<Transform> openTargetR;

	public List<Vector3> targetDefaultPositionL;

	public List<Vector3> targetDefaultPositionR;

	[Header("Open")]
	[Range(0f, 1f)]
	public float currentOpen;

	[Range(0f, 1f)]
	public float targetOpen;

	[Range(0f, 1f)]
	public float calcOpen;

	public float openSpeed = 2f;

	public float openRange = 0.01f;

	public AnimationCurve openCurve;

	public AnimationCurve closeCurve;

	public Vector3 calcRange;

	[Header("Mosaic")]
	public Transform vaginaMosaic;

	public float currentMosaic = 0.006f;

	public Vector3 currentMosaicScale;

	public AnimationCurve vaginaCurve;

	[Header("Open SE Wait")]
	public float openWait;

	public float openWaitMin = 1f;

	[Header("Se")]
	public AudioClip aimSe;

	public List<AudioClip> openSe;

	private void Start()
	{
		isAim = false;
		for (int i = 0; i < _feeler.Count; i++)
		{
			_feeler[i].isAim = false;
		}
		for (int j = 0; j < _filament.Count; j++)
		{
			_filament[j].isAim = false;
		}
		isAimEndCheck = true;
		targetDefaultPositionL.Clear();
		for (int k = 0; k < openTargetL.Count; k++)
		{
			targetDefaultPositionL.Add(openTargetL[k].localPosition);
		}
		targetDefaultPositionR.Clear();
		for (int l = 0; l < openTargetR.Count; l++)
		{
			targetDefaultPositionR.Add(openTargetR[l].localPosition);
		}
		currentMosaicScale = vaginaMosaic.localScale;
		currentMosaicScale.x = vaginaCurve.Evaluate(calcOpen);
		vaginaMosaic.localScale = currentMosaicScale;
		openWait = 0f;
	}

	private void LateUpdate()
	{
		if (isAim && !isAimEndCheck)
		{
			isAimEnd = false;
			for (int i = 0; i < _feeler.Count; i++)
			{
				if (!_feeler[i].isAimEnd)
				{
					isAimEnd = true;
				}
			}
			if (!isAimEnd)
			{
				isAimEndCheck = true;
				EffectSeManager.instance.PlaySe(aimSe);
				for (int j = 0; j < _filament.Count; j++)
				{
					_filament[j].isAim = true;
				}
			}
		}
		else if (!isAim && !isAimEndCheck)
		{
			isAimEnd = false;
			for (int k = 0; k < _filament.Count; k++)
			{
				if (_filament[k].isAimEnd)
				{
					isAimEnd = true;
				}
			}
			if (!isAimEnd)
			{
				isAimEndCheck = true;
				EffectSeManager.instance.PlaySe(aimSe);
				for (int l = 0; l < _feeler.Count; l++)
				{
					_feeler[l].isAim = false;
				}
			}
		}
		if (!isOpenReady && isOpen)
		{
			isOpenReady = true;
			for (int m = 0; m < _filament.Count; m++)
			{
				if (!_filament[m].isAimEnd)
				{
					isOpenReady = false;
				}
			}
			if (isOpenReady)
			{
				if (OnomatopoeiaManager.instance.useOtomanopoeia)
				{
					OnomatopoeiaManager.instance.SpawnOnomatopoeia(vaginaMosaic.position, null, "VaginaOpen", Camera.main.transform);
				}
				EffectSeManager.instance.PlaySe(openSe[Random.Range(0, openSe.Count)]);
			}
		}
		else if (isOpenReady && isOpen)
		{
			if (isOpen && isFeeler)
			{
				if (currentOpen < 1f)
				{
					currentOpen += Time.deltaTime * (openSpeed / 2f);
					if (currentOpen > 1f)
					{
						currentOpen = 1f;
					}
				}
			}
			else if (isOpen)
			{
				if (openWait > 0f)
				{
					openWait -= Time.deltaTime;
				}
				if (currentOpen < targetOpen)
				{
					currentOpen += Time.deltaTime * openSpeed;
					if (currentOpen > targetOpen)
					{
						currentOpen = targetOpen;
						if (openWait <= 0f)
						{
							openWait = openWaitMin;
							if (OnomatopoeiaManager.instance.useOtomanopoeia)
							{
								OnomatopoeiaManager.instance.SpawnOnomatopoeia(vaginaMosaic.position, null, "VaginaOpen", Camera.main.transform);
							}
							EffectSeManager.instance.PlaySe(openSe[Random.Range(0, openSe.Count)]);
						}
					}
				}
				else if (currentOpen > targetOpen)
				{
					currentOpen -= Time.deltaTime * openSpeed;
					if (currentOpen < targetOpen)
					{
						currentOpen = targetOpen;
						if (openWait <= 0f)
						{
							openWait = openWaitMin;
							EffectSeManager.instance.PlaySe(openSe[Random.Range(0, openSe.Count)]);
						}
					}
				}
			}
			if (calcOpen != currentOpen)
			{
				calcOpen = currentOpen;
				currentMosaicScale.x = vaginaCurve.Evaluate(calcOpen);
				vaginaMosaic.localScale = currentMosaicScale;
			}
		}
		else if (currentOpen > 0f)
		{
			currentOpen -= Time.deltaTime * (openSpeed * 2f);
			if (currentOpen < 0f)
			{
				currentOpen = 0f;
			}
			calcOpen = currentOpen;
			currentMosaicScale.x = vaginaCurve.Evaluate(calcOpen);
			vaginaMosaic.localScale = currentMosaicScale;
		}
		for (int n = 0; n < openTargetL.Count; n++)
		{
			calcRange = targetDefaultPositionL[n];
			calcRange.x -= openRange * calcOpen;
			openTargetL[n].localPosition = calcRange;
		}
		for (int num = 0; num < openTargetR.Count; num++)
		{
			calcRange = targetDefaultPositionR[num];
			calcRange.x += openRange * calcOpen;
			openTargetR[num].localPosition = calcRange;
		}
		_body.SetBlendShapeWeight(2, calcOpen * 100f);
		_body.SetBlendShapeWeight(3, calcOpen * 100f);
		if (_body.GetBlendShapeWeight(2) < 0f)
		{
			_body.SetBlendShapeWeight(2, 0f);
		}
		else if (_body.GetBlendShapeWeight(2) > 100f)
		{
			_body.SetBlendShapeWeight(2, 100f);
		}
		if (_body.GetBlendShapeWeight(3) < 0f)
		{
			_body.SetBlendShapeWeight(3, 0f);
		}
		else if (_body.GetBlendShapeWeight(3) > 100f)
		{
			_body.SetBlendShapeWeight(3, 100f);
		}
	}

	public void AimSet(bool value)
	{
		isAim = value;
		isAimEndCheck = false;
		if (isAim)
		{
			isOpen = value;
			isOpenReady = false;
			for (int i = 0; i < _feeler.Count; i++)
			{
				_feeler[i].isAim = true;
			}
		}
		else if (!isAim)
		{
			isOpen = value;
			isOpenReady = false;
			for (int j = 0; j < _filament.Count; j++)
			{
				_filament[j].isAim = false;
			}
		}
		EffectSeManager.instance.PlaySe(aimSe);
	}
}
