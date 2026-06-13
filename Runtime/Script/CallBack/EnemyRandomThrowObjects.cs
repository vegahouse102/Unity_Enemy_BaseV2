using UnityEngine;
using System.Collections.Generic;
namespace Enemy
{
	public class EnemyRandomThrowObjects : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> _throwObject = new();



		[SerializeField]
		private EnemyCurDirectionHandler _curDirectionHandler;

		[SerializeField]
		private Transform _throwPos;

		[SerializeField]
		private float _maxVelocity;
		[SerializeField]
		private float _minVelocity;
		[SerializeField]
		private float _count;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_throwObject != null);
			Debug.Assert(_throwPos != null);
#endif

		}
		private void OnDestroy()
		{

		}

		public void Throw()
		{
			if (_throwObject == null)
				return;
			if (_throwObject.Count <= 0)
				return;
			for(int i = 0; i < _count; i++)
			{
				GameObject prefab = _throwObject[Random.Range(0,_throwObject.Count)];
				GameObject throwObject = null;
				if (PoolManager.Instance == null)
					throwObject = Instantiate(prefab, _throwPos.position,Quaternion.identity);
				else
				{
					throwObject = PoolManager.Instance.GetObject(prefab, _throwPos.position);
					//Debug.Log(throwObject);
				}
	
				if(throwObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid))
				{
					float velocity = Random.Range(_minVelocity, _maxVelocity);
					rigid.linearVelocity = Random.onUnitCircle* velocity; 
				}
			}

			

		}
	}
}

