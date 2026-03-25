using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class CharacterWalkManager : MonoBehaviour
{
	public CharacterModelManager _characterModelManager;

	public CharacterAnimancerManager _characterAnimancerManager;

	public ActionManager _actionManager;

	public NavMeshAgent _navMeshAgent;

	public Animator bodyAnimator;

	[Header("Status")]
	public bool isMove;

	public bool isLink;

	public bool isFit;

	public bool isRotate;

	public bool isEvent;

	[Header("Start Warp")]
	public GameObject startPoint;

	[Header("End")]
	public GameObject targetPoint;

	public NavMeshPoint _navMeshPoint;

	[Header("Navmesh")]
	public NavMeshPath navMeshPath;

	public int pathIndex;

	public float nearRange = 0.05f;

	[Header("Calc Position")]
	public Vector3 currentPosition;

	public Vector3 targetPosition;

	public Vector3 nextPosition;

	public Vector3 calcPosition;

	public Vector3 calcDistance;

	public Vector3 linkPosition;

	public float linkDistance;

	public float fitLerp;

	[Header("Calc Rotation")]
	public float rotateLerp;

	public quaternion currentRotation;

	public quaternion targetRotation;

	public quaternion calcRotation;

	[Header("Speed")]
	public float currentSpeed;

	public float defaultSpeed = 1.5f;

	public float linkSpeed = 0.5f;

	public float slowSpeed = 0.3f;

	public float acceleration = 2f;

	public float deceleration = 3f;

	[Header("NavMesh Areas")]
	public int currentArea;

	[Header("Wait Time")]
	public float waitTime;

	public float waitTimeMax = 10f;

	private void Start()
	{
		if (_navMeshAgent == null)
		{
			_navMeshAgent = GetComponent<NavMeshAgent>();
		}
		_navMeshAgent.Warp(startPoint.transform.position);
		base.transform.rotation = startPoint.transform.rotation;
		currentPosition = base.transform.position;
		waitTime = 0f;
		if (!isMove && targetPoint != null)
		{
			SetMovePoint(targetPoint);
		}
	}

	private void FixedUpdate()
	{
		if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
			if (waitTime < 0f)
			{
				waitTime = 0f;
				SetMoveAnim();
				SetMovePoint(targetPoint);
			}
		}
		if (isMove)
		{
			isLink = _navMeshAgent.isOnOffMeshLink;
			if (!isLink)
			{
				_navMeshAgent.isStopped = false;
				calcDistance = base.transform.position;
				calcDistance.y = targetPosition.y;
				if (Vector3.Distance(calcDistance, targetPosition) > 1f)
				{
					if (currentSpeed < defaultSpeed)
					{
						currentSpeed = Mathf.MoveTowards(currentSpeed, defaultSpeed, acceleration * Time.deltaTime);
					}
					else
					{
						currentSpeed = defaultSpeed;
					}
				}
				else if (currentSpeed > slowSpeed)
				{
					currentSpeed = Mathf.MoveTowards(currentSpeed, slowSpeed, deceleration * Time.deltaTime);
				}
				else
				{
					currentSpeed = slowSpeed;
				}
				if (Vector3.Distance(calcDistance, targetPosition) < nearRange)
				{
					Debug.Log(base.gameObject.name + " <Stop> ", base.gameObject);
					isMove = false;
					isFit = true;
					isRotate = true;
					isEvent = true;
					currentPosition = base.transform.position;
					currentRotation = base.transform.rotation;
					fitLerp = 0f;
					rotateLerp = 0f;
				}
				else if (_navMeshAgent.path != null)
				{
					if (navMeshPath == null)
					{
						navMeshPath = _navMeshAgent.path;
					}
					if (navMeshPath != null && pathIndex < navMeshPath.corners.Length)
					{
						nextPosition = navMeshPath.corners[pathIndex];
						Vector3 vector = (nextPosition - base.transform.position).normalized * currentSpeed * Time.deltaTime;
						base.transform.position += vector;
						LookPoint(vector);
					}
					if (pathIndex < navMeshPath.corners.Length)
					{
						nextPosition = navMeshPath.corners[pathIndex];
					}
					if (Vector3.Distance(base.transform.position, nextPosition) < nearRange)
					{
						pathIndex++;
						if (pathIndex >= navMeshPath.corners.Length)
						{
							navMeshPath = null;
							pathIndex = 0;
						}
					}
				}
			}
			else if (isLink)
			{
				if (!_navMeshAgent.isStopped)
				{
					linkPosition = _navMeshAgent.currentOffMeshLinkData.endPos;
				}
				_navMeshAgent.isStopped = true;
				if (currentSpeed < linkSpeed)
				{
					currentSpeed = Mathf.MoveTowards(currentSpeed, linkSpeed, acceleration * Time.deltaTime);
				}
				else
				{
					currentSpeed = Mathf.MoveTowards(currentSpeed, linkSpeed, deceleration * Time.deltaTime);
				}
				Vector3 vector2 = (linkPosition - base.transform.position).normalized * currentSpeed * Time.deltaTime;
				base.transform.position += vector2;
				LookPoint(vector2);
				linkDistance = Vector3.Distance(base.transform.position, linkPosition);
				if (linkDistance <= 0.01f)
				{
					isLink = false;
					_navMeshAgent.CompleteOffMeshLink();
					pathIndex++;
				}
			}
			_characterAnimancerManager.currentSpeed = currentSpeed;
		}
		else
		{
			if (currentSpeed > 0f)
			{
				currentSpeed -= Time.deltaTime;
				if (currentSpeed < 0f)
				{
					currentSpeed = 0f;
				}
				_characterAnimancerManager.currentSpeed = currentSpeed;
			}
			if (isFit)
			{
				if (fitLerp < 1f)
				{
					fitLerp += Time.deltaTime * acceleration;
					if (fitLerp > 1f)
					{
						isFit = false;
						fitLerp = 1f;
					}
				}
				calcPosition = Vector3.Lerp(currentPosition, targetPosition, fitLerp);
				calcPosition.y = base.transform.position.y;
				base.transform.position = calcPosition;
			}
			if (!isFit && isEvent)
			{
				isEvent = false;
				GoalEvent();
			}
			if (isRotate)
			{
				LookTarget();
			}
		}
		if (!NavMesh.SamplePosition(base.transform.position, out var hit, 1f, -1))
		{
			return;
		}
		int mask = hit.mask;
		if (currentArea != mask)
		{
			if (currentArea == 16 && mask != 16)
			{
				_characterModelManager.MagicaChange("Ground");
			}
			else if (currentArea != 16 && mask == 16)
			{
				_characterModelManager.MagicaChange("Bed");
			}
			currentArea = mask;
		}
	}

	public void SetMoveAnim()
	{
		_characterAnimancerManager.StateSet("isMove", 0.25f, playWait: false);
	}

	public void SkipMoveAnim()
	{
		_characterAnimancerManager.IdleMotion();
	}

	public void SetMovePoint(GameObject point)
	{
		isMove = true;
		_navMeshAgent.SetDestination(point.transform.position);
		navMeshPath = null;
		targetPoint = point;
		targetPosition = targetPoint.transform.position;
		targetRotation = targetPoint.transform.rotation;
	}

	public void SkipMovePoint(GameObject point, bool value)
	{
		isMove = false;
		isFit = false;
		isRotate = false;
		isEvent = value;
		fitLerp = 0f;
		rotateLerp = 0f;
		if (point != null)
		{
			targetPoint = point;
		}
		_navMeshAgent.Warp(point.transform.position);
		base.transform.rotation = point.transform.rotation;
		currentPosition = base.transform.position;
		currentRotation = base.transform.rotation;
		waitTime = 0f;
	}

	private void LookPoint(Vector3 value)
	{
		Quaternion b = Quaternion.LookRotation(value.normalized);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime);
	}

	private void LookTarget()
	{
		if (rotateLerp < 1f)
		{
			rotateLerp += Time.deltaTime * acceleration;
			if (rotateLerp > 1f)
			{
				isRotate = false;
				rotateLerp = 1f;
			}
		}
		calcRotation = Quaternion.Lerp(currentRotation, targetRotation, rotateLerp);
		base.transform.rotation = calcRotation;
	}

	public void GoalEvent()
	{
		if ((bool)targetPoint.GetComponent<NavMeshPoint>())
		{
			_navMeshPoint = targetPoint.GetComponent<NavMeshPoint>();
			_navMeshPoint.Goal(this);
		}
	}

	public void MoveWait(GameObject obj, float time)
	{
		waitTimeMax = time;
		targetPoint = obj;
		waitTime = waitTimeMax;
	}
}
