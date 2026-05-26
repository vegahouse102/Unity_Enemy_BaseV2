using UnityEngine;
using Damage;
using UnityEngine.Events;
namespace Enemy
{
	public class EnemyHealth : MonoBehaviour
	{
		[SerializeField]
		private EnemyHealthSO _enemyHealthSO;

		private int _curHealth;


		public UnityEvent<DamageInfo> OnDead;
		public UnityEvent<DamageInfo> OnHurt;
		private void Awake()
		{
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

