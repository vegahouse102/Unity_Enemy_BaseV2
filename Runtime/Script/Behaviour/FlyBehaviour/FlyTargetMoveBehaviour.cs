using UnityEngine;
using Unity.Behavior;
using Sensor;
namespace Enemy
{
	public abstract class FlyTargetMoveBehaviour : EnemyBehaviour
	{
		[Header("공중움직임")]


		[SerializeField]
		private Rigidbody2D _rigid;
		[SerializeField]
		private EnemyCurDirectionHandler _directionHandler;
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private SensorContext _sensorContext;


		[SerializeField]
		private string _moveBoolAnimationName;

		[SerializeField]
		private bool _isFrontMove = true;

		[SerializeField, Min(0.01f)]
		private float _startVelocity;


		[SerializeField, Min(0)]
		private float _maxVelocity;

		[SerializeField, Min(0.001f)]
		private float _acceleration;

		[SerializeField, Range(0f, 1f)]
		private float _steeringSensitivity;

		[SerializeField, Min(0.001f)]
		private float _closeDistanceThreshold = 0.01f;


		private bool _isActiveNode;


		private Transform _targetTransform;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_directionHandler != null);
			Debug.Assert(_rigid != null);
			Debug.Assert(_animator != null);
			Debug.Assert(_sensorContext != null);

#endif

		}

		protected override Node.Status OnStartProcess()
		{
			_targetTransform = GetTargetTransform();
			if (_targetTransform == null)
			{
				Debug.Log("target object is null");
				return Node.Status.Failure;
			}

			_animator.SetBool(_moveBoolAnimationName, true);
			_isActiveNode = true;
			_rigid.linearVelocity = (_targetTransform.position-transform.position).normalized*_startVelocity;
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{

			if (Vector2.Distance(transform.position, _targetTransform.position) < _closeDistanceThreshold)
			{
				return Node.Status.Success;
			}


			//float targetDirX = _rigid.linearVelocityX;
			float targetDirX = (_targetTransform.position.x - transform.position.x);
			int currentFacingX = _directionHandler.GetXDirection(); // 오른쪽이면 1, 왼쪽이면 -1 가정
			float distanceX = Mathf.Abs(_targetTransform.position.x - transform.position.x);
			// 목적지가 오른쪽에 있는가? 현재 오른쪽을 보고 있는가?
			bool targetIsRight = targetDirX > 0;
			bool facingIsRight = currentFacingX > 0;

			bool shouldTurn = _isFrontMove ? (targetIsRight != facingIsRight) : (targetIsRight == facingIsRight);
			shouldTurn = shouldTurn && distanceX > _closeDistanceThreshold;
			if (shouldTurn)
			{
				_directionHandler.Turn();
			}

			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			_rigid.linearVelocity = Vector2.zero;
			_animator.SetBool(_moveBoolAnimationName, false);
			_isActiveNode = false;
		}

		protected abstract Transform GetTargetTransform();

		private void FixedUpdate()
		{
			
			if (!_isActiveNode)
				return;

			
			Vector2 targetDirection = ((Vector2)_targetTransform.position - (Vector2)transform.position).normalized;

		
			_rigid.linearVelocity += targetDirection * _acceleration * Time.fixedDeltaTime;

			_rigid.linearVelocity = Vector2.Lerp(_rigid.linearVelocity, targetDirection * _rigid.linearVelocity.magnitude, _steeringSensitivity);

			if (_rigid.linearVelocity.sqrMagnitude > _maxVelocity * _maxVelocity)
			{

				_rigid.linearVelocity = _rigid.linearVelocity.normalized * _maxVelocity;
			}
		}



	}
}
