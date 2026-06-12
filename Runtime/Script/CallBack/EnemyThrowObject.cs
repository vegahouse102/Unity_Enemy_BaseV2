
using UnityEngine;

namespace Enemy 
{
	public class EnemyThrowObject : MonoBehaviour
	{
		[SerializeField]
		private GameObject _throwObject;



		[SerializeField]
		private EnemyCurDirectionHandler _curDirectionHandler;

		[SerializeField]
		private Transform _throwPos;

		[SerializeField]
		[Tooltip("던질시 velocity")]
		private Vector2 _throwVelocity;

		[SerializeField]
		[Tooltip("true시 Turn할때 velocityX가 반대가된다.")]
		private bool _isAutoTurnX;

		private bool _shouldTurnVelocityX;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_throwObject != null);
			Debug.Assert(_throwPos != null);
#endif

			if(_isAutoTurnX&& _curDirectionHandler != null)
				_curDirectionHandler.OnTurn += TurnX;
		}
		private void OnDestroy()
		{
			if (_isAutoTurnX&&_curDirectionHandler != null)
				_curDirectionHandler.OnTurn -= TurnX;
		}
		private void TurnX()
		{
			_shouldTurnVelocityX = !_shouldTurnVelocityX;
		}

		public void SetVelocity(Vector2 velocity)
		{
			_throwVelocity = velocity;
		}
		public Vector2 GetOriginThrowVelocity()
		{
			return _throwVelocity;
		}
		public void Throw()
		{
			GameObject throwObject = null;
			if (PoolManager.Instance==null)
				throwObject = Instantiate(_throwObject, _throwPos.position,Quaternion.identity);
			else
			{
				throwObject = PoolManager.Instance.GetObject(_throwObject, _throwPos.position);
				//Debug.Log(throwObject);
			}
				
			Rigidbody2D rigid = throwObject.GetComponent<Rigidbody2D>();
			if (rigid!=null)
			{
				float xVelocity = _throwVelocity.x;
				if (_shouldTurnVelocityX)
				{
					xVelocity = -xVelocity;
				}
				rigid.linearVelocity = new Vector2(xVelocity, _throwVelocity.y);
			}
		}
	}
	
}


