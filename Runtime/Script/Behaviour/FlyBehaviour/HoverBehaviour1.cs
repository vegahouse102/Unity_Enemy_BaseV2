using Sensor;
using Unity.Behavior;
using UnityEngine;
using Enemy.Utils;
namespace Enemy
{
	public class HoverBehaviour : EnemyBehaviour
	{
		[Header("공중 hover 움직임")]


		[SerializeField]
		private Rigidbody2D _rigid;
		[SerializeField]
		private EnemyCurDirectionHandler _directionHandler;
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private SensorContext _sensorContext;

		[Space(30)]
		[Header("앵커로부터 radius안을 배회함, 밖으로 나가면 안으로 돌아온다")]
		[SerializeField]
		private Transform _anchorPointTransform;
		[SerializeField]
		private float _radius;

		[Space(30)]



		[SerializeField]
		private string _moveBoolAnimationName;

		[SerializeField]
		private bool _isFrontMove = true;

		[SerializeField, Min(0.01f)]
		private float _startVelocity;
		[SerializeField, Min(0)]
		private float _endVelocity;

		[SerializeField, Min(0)]
		private float _maxVelocity;

		[SerializeField,Min(0.001f)]
		private float _acceleration;
		[SerializeField, Min(0.001f)]
		private float _closeDistanceThreshold = 0.01f;

		private Vector3 _anchorPosition;

		private Vector2 _pos1, _pos2;
		private Vector2 _endPos1, _endPos2;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_directionHandler != null);
			Debug.Assert(_rigid != null);
			Debug.Assert(_animator != null);
			Debug.Assert(_sensorContext != null);
			Debug.Assert(_anchorPointTransform != null);
#endif
			_anchorPosition = _anchorPointTransform.position;
			_endPos1 = (Vector2)_anchorPosition + Vector2.right * _radius;
			_endPos2 = (Vector2)_anchorPosition - Vector2.right * _radius;
		}

		protected override Node.Status OnStartProcess()
		{

			_animator.SetBool(_moveBoolAnimationName, true);


			

			_pos1 = transform.position;
			_pos2 = _endPos2;
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
#if UNITY_EDITOR
			Debug.DrawLine(_pos1,_pos2,Color.red);
			Debug.DrawLine(_endPos1, _endPos2, Color.blue);
#endif
			if (Vector2.Distance(transform.position, _pos2) < _closeDistanceThreshold)
			{
				_pos1 = _pos2;
				_pos2 = (_pos2 == _endPos1) ? _endPos2 : _endPos1;
			}

			

			Accel(_pos1, _pos2, transform.position);


			float targetDirX = _pos2.x - transform.position.x;
			int currentFacingX = _directionHandler.GetXDirection(); // 오른쪽이면 1, 왼쪽이면 -1 가정

			// 목적지가 오른쪽에 있는가? 현재 오른쪽을 보고 있는가?
			bool targetIsRight = targetDirX > 0;
			bool facingIsRight = currentFacingX > 0;
		
			bool shouldTurn = _isFrontMove ? (targetIsRight != facingIsRight) : (targetIsRight == facingIsRight);
			
			if (shouldTurn)
			{
				_directionHandler.Turn();
			}
			//Debug.Log(shouldTurn);
			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			_rigid.linearVelocityX = 0;
			_animator.SetBool(_moveBoolAnimationName, false);
		}


		private void Accel(Vector2 pos1, Vector2 pos2, Vector2 cur)
		{
			// 1. 총 가야 할 벡터와 거리 구하기
			Vector2 totalVector = pos2 - pos1;
			float totalDistance = totalVector.magnitude;

			// 만약 목적지와 출발지가 완전히 똑같다면 연산 패스
			if (totalDistance < 0.001f) return;

			// 2. 💥 [기적의 수정] 벡터 투영을 이용한 '진짜 전진 거리' 계산!
			// 몬스터가 옆으로 빗나가거나 뒤로 밀려도, '고속도로 진행 방향' 기준의 정밀한 위치를 찾아냅니다.
			Vector2 currentVector = cur - pos1;
			float currentMoved = Vector2.Dot(currentVector, totalVector.normalized);

			// 💥 [안전장치] 전진 거리가 0보다 작아지거나(뒤로 밀림), 총 거리를 넘어서면(오버슈트) 딱 잘라줍니다.
			currentMoved = Mathf.Clamp(currentMoved, 0f, totalDistance);

			// 3. 이제 안전해진 데이터로 사다리꼴 속도 계산
			float calculatedSpeed = EnemyMath.GetAdvancedTrapezoidalVelocity(
				totalDistance,
				currentMoved,
				_maxVelocity,
				_acceleration,
				_startVelocity,
				_endVelocity
			);

			//float finalSpeed = Mathf.Max(0.1f, calculatedSpeed);

			_rigid.linearVelocity = calculatedSpeed * totalVector.normalized;
		}

	}

}




