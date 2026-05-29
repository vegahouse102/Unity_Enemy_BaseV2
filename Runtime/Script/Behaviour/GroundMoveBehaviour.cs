using Sensor;
using Unity.Behavior;
using UnityEngine;

namespace Enemy
{

	public class GroundMoveBehaviour : EnemyBehaviour
	{
		[SerializeField]
		private Rigidbody2D _rigid;
		[SerializeField]
		private EnemyCurDirectionHandler _directionHandler;
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private SensorContext _sensorContext;

		[Space(30)]


		[SerializeField]
		private string _moveBoolAnimationName;

		[SerializeField]
		private bool _isFrontMove = true;
		[SerializeField]
		private bool _shouldTurnOnWall = true;
		[SerializeField]
		private bool _shouldTurnOnCliff = true;
		[SerializeField,Min(0)]
		private float _turnCooldown = 0.5f;

		[Space(30)]


		[SerializeField, Min(0)]
		private float _maxRandomMoveTime;
		[SerializeField, Min(0)]
		private float _minRandomMoveTime;
		[SerializeField,Min(0)]
		private float _velocity;



		private float _curTime;
		private float _finishTime;


		private float _lastTurnTime;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert( _directionHandler != null );
			Debug.Assert( _rigid != null );
			Debug.Assert(_animator != null );
			Debug.Assert(_sensorContext!=null);
#endif
			if (_minRandomMoveTime > _maxRandomMoveTime)
			{
				float tmp = _minRandomMoveTime;
				_minRandomMoveTime = _maxRandomMoveTime;
				_maxRandomMoveTime = tmp;
			}
		}

		protected override Node.Status OnStartProcess()
		{

			_finishTime = Random.Range(_minRandomMoveTime,_maxRandomMoveTime);
			_curTime = 0f;
			_lastTurnTime = -999;
			_animator.SetBool(_moveBoolAnimationName,true);
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
			if(_curTime >= _finishTime)
			{
				return Node.Status.Success;
			}

			_curTime += Time.deltaTime;

			int dir = _directionHandler.GetXDirection();
			if (!_isFrontMove)
			{
				dir = -dir;
			}


			
			bool hitWall = 
				(_shouldTurnOnWall && dir < 0 && _sensorContext.WallSensor.IsTouchLeftWall)
				|| (_shouldTurnOnWall && dir > 0 && _sensorContext.WallSensor.IsTouchRightWall);

			bool isCliff = 
				(_shouldTurnOnCliff && dir < 0 && !_sensorContext.GroundSensor.IsGroundedLeft)
					|| (_shouldTurnOnCliff && dir > 0 && !_sensorContext.GroundSensor.IsGroundedRight);

			if (hitWall || isCliff)
			{
				if(Time.time - _lastTurnTime > _turnCooldown)
				{
					_directionHandler.Turn();
					dir = -dir;
					_lastTurnTime = Time.time;
				}
				else
				{
					_rigid.linearVelocityX = 0;
					return Node.Status.Running;
				}
					
			}
			
			_rigid.linearVelocityX = dir * _velocity;

			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			_rigid.linearVelocityX = 0;
			_animator.SetBool(_moveBoolAnimationName, false);
		}
	}
}

