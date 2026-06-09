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
		private float _closeDistanceThreshold = 0.1f;

		private Vector3 _anchorPosition;

		private Vector2 _pos1, _pos2;
		private Vector2 _endPos1, _endPos2;
		private Vector2 _curDir;
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
		}

		protected override Node.Status OnStartProcess()
		{

			_animator.SetBool(_moveBoolAnimationName, true);
			_curDir = Vector2.right * _directionHandler.GetXDirection();
			if (!_isFrontMove)
				_curDir.x *= -1;

			_endPos1 = (Vector2)_anchorPosition + Vector2.right * _radius;
			_endPos2 = (Vector2)_anchorPosition - Vector2.right * _radius;

			_pos1 = transform.position;
			_pos2 = _endPos2;
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
			float directionDot = _rigid.linearVelocityX * _directionHandler.GetXDirection();

			bool shouldTurn = _isFrontMove ? directionDot < 0 : directionDot > 0;

			Accel(_pos1, _pos2, transform.position);


			if (shouldTurn)
			{
				_directionHandler.Turn();
			}

			if(Vector2.Distance(_endPos1, (Vector2)transform.position)< _closeDistanceThreshold)
			{
				_pos1 = _endPos1;
				_pos2 = _endPos2;
			}else if (Vector2.Distance(_endPos2, (Vector2)transform.position)< _closeDistanceThreshold)
			{
				_pos1 = _endPos2;
				_pos2 = _endPos1;
			}
			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			_rigid.linearVelocityX = 0;
			_animator.SetBool(_moveBoolAnimationName, false);
		}


		private void Accel(Vector2 pos1,Vector2 pos2,Vector2 cur)
		{
			_rigid.linearVelocity = EnemyMath.GetAdvancedTrapezoidalVelocity(
				Vector2.Distance(pos1, pos2),
				Vector2.Distance(pos1, cur),
				_maxVelocity,
				_acceleration,
				_startVelocity,
				_endVelocity
				)*(pos2-cur);
		}


	}

}




