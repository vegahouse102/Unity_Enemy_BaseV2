
using UnityEngine;
namespace Enemy
{


	public class Projectile : MonoBehaviour
	{

		[SerializeField,Min(0)]
		private float _removeTime = 7f;

		private float _startTime;
		private void Awake()
		{
			_startTime = Time.time;
		}

		// Update is called once per frame
		private void Update()
		{

			if(Time.time >= _startTime + _removeTime)
			{
				Destroy(this.gameObject);
			}
		}
	}


}


