using UnityEngine;
using Damage;
using UnityEngine.Events;
namespace Enemy
{
	public class EnemyHealth : MonoBehaviour
	{


		private int _curHealth;
		[SerializeField]
		private EnemyHealthSO _enemyHealthSO;

		public UnityEvent<DamageInfo> OnDeath;
		public UnityEvent<DamageInfo> OnHurt;

		private void OnEnable()
		{
			Initialize();
		}
		private void Initialize()
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
			Debug.Log(damageInfo.Damage);
			_curHealth =  Mathf.Max(0,_curHealth- (int)damageInfo.Damage);
			if(_curHealth <= 0)
			{
				OnDeath?.Invoke(damageInfo);
			}
			else
			{
				OnHurt?.Invoke(damageInfo);
			}
		}

	}
}

