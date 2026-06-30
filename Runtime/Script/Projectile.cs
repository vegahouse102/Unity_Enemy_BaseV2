
using UnityEngine;
namespace Enemy
{


	public class Projectile : MonoBehaviour
	{

		[SerializeField,Min(0)]
		private float _removeTime = 7f;
		[SerializeField]
		private bool _poolManagerEnable = true;
		private float _startTime;
		private void OnEnable()
		{
			_startTime = Time.time;
		}

		// Update is called once per frame
		private void Update()
		{

			if(Time.time >= _startTime + _removeTime)
			{
				if (_poolManagerEnable&& PoolManager.Instance != null)
					PoolManager.Instance.ReleaseObject(gameObject);
				else
					Destroy(gameObject);
			}
		}
	}


}


