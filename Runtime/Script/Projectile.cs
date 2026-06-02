
using UnityEngine;
namespace Enemy
{


	public class Projectile : MonoBehaviour
	{
		[SerializeField]
		Rigidbody2D _rigid;

		[SerializeField]
		Vector2 _velocity;

		[SerializeField,Min(0)]
		private float _removeTime = 7f;

		private float _startTime;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert( _rigid != null );
			Debug.Assert( _velocity != null );
#endif
			_startTime = Time.time;
		}

		// Update is called once per frame
		private void Update()
		{
			_rigid.linearVelocity = _velocity;

			if(Time.time >= _startTime + _removeTime)
			{
				Destroy(this.gameObject);
			}
		}
		public void SetVelocity(Vector2 velocity)
		{
			_velocity= velocity;
		}
	}


}


