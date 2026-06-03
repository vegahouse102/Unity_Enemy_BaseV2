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
			GameObject projectile = Instantiate(_projectile);
			projectile.transform.position = _shootPos.position;
			Projectile projectileComponent = projectile.GetComponent<Projectile>();
			DamageTrigger damageTrigger = projectile.GetComponent<DamageTrigger>();
#if UNITY_EDITOR
			Debug.Assert(projectileComponent != null);
			Debug.Assert(damageTrigger != null);
#endif
			GameObject player = _playerFinder.GetPlayerOrNull();
			if (player == null)
				return;
			Vector2 velocity = player.transform.position - _shootPos.position;
			velocity = velocity.normalized;
			velocity *= _speed;

			projectileComponent.SetVelocity(velocity);

			damageTrigger.SetAttacker(_enemyEntity.GetEnemyObject());
			damageTrigger.SetDamage(_damage);



		}
	}
}

