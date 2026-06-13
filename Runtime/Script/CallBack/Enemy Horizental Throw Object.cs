
using UnityEngine;

namespace Enemy
{
	public class EnemyHorizentalThrowObject : MonoBehaviour
	{
		[SerializeField]
		private GameObject _throwObject;

		[SerializeField]
		private Transform _throwPosTransform;
		[SerializeField]
		private float _range;
		[SerializeField,Min(1)] 
		private int _throwCount;
		[SerializeField]
		private Vector2 _offset;
		[SerializeField]
		[Tooltip("´øÁú½Ã velocity")]
		private Vector2 _throwVelocity;

		private Vector2 _throwPos;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_throwObject != null);
			Debug.Assert(_throwPos != null);
#endif

			_throwPos = _throwPosTransform.position;
		}
		private void Update()
		{
#if UNITY_EDITOR
			Vector2 left = (Vector2)_throwPos - Vector2.right * _range + _offset;
			Vector2 right = (Vector2)_throwPos + Vector2.right * _range + _offset;
			Debug.DrawLine(left,right,Color.purple);
#endif
		}
		private void OnDestroy()
		{

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
			if(_throwCount == 1)
			{
				Throw((Vector2)_throwPos + _offset);
				return;
			}
			Vector2 left = (Vector2)_throwPos-Vector2.right*_range+_offset;
			Vector2 right = (Vector2)_throwPos +Vector2.right * _range + _offset;
			float gap = (left - right).magnitude / (_throwCount - 1);
			Vector2 cur = left;
			for(int i = 0; i <  _throwCount; i++)
			{
				Throw(cur);
				cur.x += gap;
			}
		}
		private  void Throw(Vector2 pos)
		{
			GameObject throwObject = null;
			if (PoolManager.Instance == null)
				throwObject = Instantiate(_throwObject,pos,Quaternion.identity);
			else
			{
				throwObject = PoolManager.Instance.GetObject(_throwObject,pos);
				//Debug.Log(throwObject);
			}

			Rigidbody2D rigid = throwObject.GetComponent<Rigidbody2D>();
			if (rigid != null)
			{
				float xVelocity = _throwVelocity.x;
				rigid.linearVelocity = new Vector2(xVelocity, _throwVelocity.y);
			}
		}
	}
}

