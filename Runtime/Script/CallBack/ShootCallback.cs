using Damage;
using Sensor;
using UnityEngine;

namespace Enemy
{
	public class ShootCallback : MonoBehaviour
	{
		[SerializeField]
		GameObject _projectile;
		[SerializeField]
		PlayerFinder _playerFinder;
		[SerializeField]
		EnemyEntity _enemyEntity;
		[SerializeField]
		Transform _shootPos;




		[SerializeField]
		float _speed;
		[SerializeField]
		float _damage;

		private void Awake()
		{

		}

		public void Fire()
		{
			GameObject projectile;
			if (PoolManager.Instance == null)
				projectile = Instantiate(_projectile);
			else
				projectile = PoolManager.Instance.GetObject(_projectile);
			projectile.transform.position = _shootPos.position;

			Rigidbody2D rigid = projectile.GetComponent <Rigidbody2D>();
			DamageTrigger damageTrigger = projectile.GetComponent<DamageTrigger>();
#if UNITY_EDITOR

			Debug.Assert(rigid != null);
			Debug.Assert(damageTrigger != null);
#endif
			GameObject player = _playerFinder.GetPlayerOrNull();
			if (player == null)
				return;
			Vector2 velocity = player.transform.position - _shootPos.position;
			velocity = velocity.normalized;
			velocity *= _speed;

			rigid.linearVelocity = (velocity);

			damageTrigger.SetAttacker(_enemyEntity.GetEnemyObject());
			damageTrigger.SetDamage(_damage);



		}
	}
}

