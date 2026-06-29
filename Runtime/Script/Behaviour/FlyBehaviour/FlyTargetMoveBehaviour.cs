using UnityEngine;
using Unity.Behavior;
using Sensor;

namespace Enemy
{
	public abstract class FlyTargetMoveBehaviour : EnemyBehaviour
	{
		[Header("공중움직임")]
		[SerializeField] private Rigidbody2D _rigid;
		[SerializeField] private EnemyCurDirectionHandler _directionHandler;
		[SerializeField] private Animator _animator;

		[SerializeField] private string _moveBoolAnimationName;
		[SerializeField] private bool _isFrontMove = true;
		[SerializeField, Min(0.01f)] private float _startVelocity;
		[SerializeField, Min(0)] private float _maxVelocity;
		[SerializeField, Min(0.001f)] private float _acceleration;
		[SerializeField, Range(0f, 1f)] private float _steeringSensitivity;
		[SerializeField, Min(0.001f)] private float _closeDistanceThreshold = 0.01f;

		private bool _isActiveNode;

		// 💥 FixedUpdate와 OnUpdateProcess가 동일한 프레임의 최신 좌표를 공유하도록 필드로 관리합니다.
		private Vector3 _cachedTargetPosition;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_directionHandler != null);
			Debug.Assert(_rigid != null);
			Debug.Assert(_animator != null);
#endif
		}

		protected override Node.Status OnStartProcess()
		{
			InitializeTarget();
			if (!TryGetTargetPosition(out _cachedTargetPosition))
				return Node.Status.Failure;

			_animator.SetBool(_moveBoolAnimationName, true);
			_isActiveNode = true;

			// 캐싱된 안전한 좌표로 초기 속도 주입
			_rigid.linearVelocity = (_cachedTargetPosition - transform.position).normalized * _startVelocity;
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{

			if (!TryGetTargetPosition(out _cachedTargetPosition))
				return Node.Status.Failure;


			if (_cachedTargetPosition == Vector3.positiveInfinity)
			{
				return Node.Status.Failure;
			}

			// 이제 안전해진 좌표로 거리 계산
			if (Vector2.Distance(transform.position, _cachedTargetPosition) < _closeDistanceThreshold)
			{
				return Node.Status.Success;
			}

			// 회전(Turn) 로직
			float targetDirX = (_cachedTargetPosition.x - transform.position.x);
			int currentFacingX = _directionHandler.GetXDirection();
			float distanceX = Mathf.Abs(targetDirX);

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

		/// <summary>
		/// 추적할 대상의 실시간 위치 가져올 수 있으면 true 없으면 false 리턴할 것.
		/// </summary>
		protected abstract bool TryGetTargetPosition(out Vector3 targetPos);

		/// <summary>
		/// 행동이 시작될시 target을 initialize하는 메서드
		/// </summary>
		protected virtual void InitializeTarget()
		{

		}
		private void FixedUpdate()
		{
			if (!_isActiveNode)
				return;


			Vector2 targetDirection = ((Vector2)_cachedTargetPosition - (Vector2)transform.position).normalized;


			_rigid.linearVelocity += targetDirection * _acceleration * Time.fixedDeltaTime;
			_rigid.linearVelocity = Vector2.Lerp(_rigid.linearVelocity, targetDirection * _rigid.linearVelocity.magnitude, _steeringSensitivity);


			if (_rigid.linearVelocity.sqrMagnitude > _maxVelocity * _maxVelocity)
			{
				_rigid.linearVelocity = _rigid.linearVelocity.normalized * _maxVelocity;
			}
		}
	}
}