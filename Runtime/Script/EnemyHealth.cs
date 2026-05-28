using UnityEngine;
using Damage;
using UnityEngine.Events;
namespace Enemy
{
	public class EnemyHealth : MonoBehaviour
	{


		private int _curHealth;
		private EnemyHealthSO _enemyHealthSO;

		public UnityEvent<DamageInfo> OnDead;
		public UnityEvent<DamageInfo> OnHurt;

		public void Initialize(EnemyHealthSO enemyHealthSO)
		{
			_enemyHealthSO = enemyHealthSO;
			_curHealth = _enemyHealthSO.MaxHealth;
		}
		public int GetCurHealth()
		{
			return _curHealth;
		}
		public int GetMaxHealth()
		{
			return _enemyHealthSO.MaxHealth;
		}

		public void GetDamage(DamageInfo damageInfo)
		{
			if (_curHealth <= 0)
			{
				return;
			}
			_curHealth =  Mathf.Max(0,_curHealth- (int)damageInfo.Damage);
			if(_curHealth <= 0)
			{
				OnDead?.Invoke(damageInfo);
			}
			else
			{
				OnHurt?.Invoke(damageInfo);
			}
		}

	}
}

