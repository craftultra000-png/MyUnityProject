using UnityEngine;

public class CharacterHead : MonoBehaviour
{
	public Transform mouthPosition;

	public Transform headEffectPosition;

	public Transform headTopEffectPosition;

	public Transform mouthHangPosition;

	[Header("Status")]
	public bool isTimeline;

	[Header("Effect")]
	public GameObject breathEffect;

	[Space]
	public GameObject headDizzyEffect;

	public GameObject headHeartEffect;

	public GameObject headOverHeatEffect;

	[Space]
	public GameObject vomitEffect;

	public GameObject anglyEffect;

	[Header("Tangue Hang")]
	[Range(-100f, 100f)]
	public float tongueOut;

	public Vector3 calcTonguePosition;

	public Vector3 defaultTonguePosition;

	public float tongueOutAdjust = 0.0175f;

	[Header("Tear Status")]
	public float tearCurrent;

	public float tearTarget;

	[Header("Drip Tear")]
	public GameObject tearDripL;

	public GameObject tearDripR;

	private ParticleSystem tearDripParticleL;

	private ParticleSystem tearDripParticleR;

	private ParticleSystem.EmissionModule tearDripParticleEmissionL;

	private ParticleSystem.EmissionModule tearDripParticleEmissionR;

	public float dripCount;

	public float dripCountUp = 2f;

	public float dripCountMax = 4f;

	public float dripSpeed = 0.05f;

	public Transform tearLPosition;

	public Transform tearRPosition;

	[Header("Head Mesh")]
	public SkinnedMeshRenderer headMesh;

	private Mesh headSkinnedMesh;

	[Header("Head Effect")]
	public GameObject headSweat;

	public GameObject headSteam;

	public float hotCurrent;

	public float hotMax;

	public float hotSweat;

	public float sweatCurrent;

	public float sweatMax = 4f;

	public float steamCurrent;

	public float steamMax = 2f;

	public ParticleSystem _sweatParticle;

	public ParticleSystem.ShapeModule _sweatParticleShape;

	private ParticleSystem.EmissionModule _sweatParticleEmission;

	public ParticleSystem _steamParticle;

	public ParticleSystem.ShapeModule _steamParticleShape;

	private ParticleSystem.EmissionModule _steamParticleEmission;

	private void Start()
	{
		if (!isTimeline)
		{
			headSkinnedMesh = headMesh.sharedMesh;
		}
	}

	private void OnEnable()
	{
	}

	private void LateUpdate()
	{
		tongueOut = headMesh.GetBlendShapeWeight(12);
		calcTonguePosition.z = tongueOutAdjust * (tongueOut / 100f);
	}

	public void Breath(bool hardBreath)
	{
		Object.Instantiate(breathEffect, mouthPosition.transform, worldPositionStays: false).transform.localPosition = Vector3.zero;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			if (!hardBreath)
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "Breath", Camera.main.transform);
			}
			else
			{
				OnomatopoeiaManager.instance.SpawnOnomatopoeia(base.transform.position, null, "BreathHard", Camera.main.transform);
			}
		}
	}

	public void HeadDizzy()
	{
		Object.Instantiate(headDizzyEffect, headEffectPosition.transform, worldPositionStays: false).transform.localPosition = Vector3.zero;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(headTopEffectPosition.position, null, "Dizzy", Camera.main.transform);
		}
	}

	public void HeadHeart()
	{
		Object.Instantiate(headHeartEffect, headEffectPosition.transform, worldPositionStays: false).transform.localPosition = Vector3.zero;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(headTopEffectPosition.position, null, "Heart", Camera.main.transform);
		}
	}

	public void HeadOverHeat()
	{
		Object.Instantiate(headOverHeatEffect, headEffectPosition.transform, worldPositionStays: false).transform.localPosition = Vector3.zero;
		if (OnomatopoeiaManager.instance.useOtomanopoeia)
		{
			OnomatopoeiaManager.instance.SpawnOnomatopoeia(headTopEffectPosition.position, null, "OverHeat", Camera.main.transform);
		}
	}

	public void Vomit()
	{
		Object.Instantiate(vomitEffect, mouthPosition.transform, worldPositionStays: false).transform.localPosition = Vector3.zero;
	}

	public void Angly()
	{
		Object.Instantiate(anglyEffect, base.transform, worldPositionStays: false).transform.localPosition = Vector3.zero;
	}

	public void Tear()
	{
		dripCount += dripCountUp;
		if (dripCount > dripCountMax)
		{
			dripCount = dripCountMax;
		}
		tearDripL.SetActive(value: true);
		tearDripR.SetActive(value: true);
	}

	public void TearShort()
	{
		if (dripCount < 0.1f)
		{
			dripCount = 0.1f;
		}
		tearDripL.SetActive(value: true);
		tearDripR.SetActive(value: true);
	}

	public void HotData(float max, float sweat)
	{
		hotMax = max;
		hotSweat = sweat;
	}

	public void HotSync(float value)
	{
		hotCurrent = value;
	}
}
