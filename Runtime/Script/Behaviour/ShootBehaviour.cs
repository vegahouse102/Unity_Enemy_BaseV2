using Damage;
using Unity.Behavior;
using UnityEngine;

namespace Enemy.Cleaner
{
	public class ShootBehaviour : EnemyBehaviour
	{
		[SerializeField]
		Animator _animator;

		[SerializeField]
		private GameObject _bullet;

		[SerializeField]
		private Transform _bulletPos;

		[SerializeField]
		private PlayerFinder _enemyPlayerFinder;

		[SerializeField] 
		private EnemyStateController _stateController;


		[SerializeField]
		private int _shootCount;
		[SerializeField]
		private float _shootWaitTime;

		[SerializeField]
		private float _shootSpeed;

	
		private int _remainShootCount;
		private float _lastShootTime;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_bulletPos != null);
			Debug.Assert(_bullet != null);
			Debug.Assert(_enemyPlayerFinder != null);
			Debug.Assert(_stateController != null);
#endif
		}
		public override Node.Status OnStart()
		{
			_remainShootCount = _shootCount;
			_lastShootTime = -999;
			return Node.Status.Running;
		}

		public override Node.Status OnUpdate()
		{
			if (_remainShootCount > 0)
			{
				GameObject player = _enemyPlayerFinder.GetPlayerOrNull();
				if ((player.transform.position.x < transform.position.x && _stateController.GetXDirection() > 0)
					|| (player.transform.position.x > transform.position.x && _stateController.GetXDirection() < 0))
					_stateController.Turn();
				if (Time.time >= _lastShootTime + _shootWaitTime)
				{
					_lastShootTime = Time.time;
					//Debug.Log("Shoot");

					GameObject bullet = Instantiate(_bullet);
					bullet.transform.position = _bulletPos.position;

					Projectile projectile = bullet.GetComponent<Projectile>();
					DamageTrigger trigger = bullet.GetComponent<DamageTrigger>();
					trigger.SetAttacker(_stateController.GetEnemyObject());

					if (player != null)
					{
						projectile.SetVelocity(_shootSpeed * (player.transform.position - transform.position).normalized);
					}
					else
					{
#if UNITY_EDITOR

					//	Debug.Log("Player Null");
#endif
					}

					--_remainShootCount;
				}
				return Node.Status.Running;
			}

			return Node.Status.Success;

		}
		public override void OnEnd()
		{
			
		}

	}

}
