
using UnityEngine;
using UnityEngine.Events;
namespace Damage
{
	public class DamageTrigger : MonoBehaviour
	{


		[SerializeField] private float _damage;
		[SerializeField] private LayerMask _targetLayer;

		[SerializeField] GameObject _attacker;
		[SerializeField] private bool _isOneHitDamage = true;
		public UnityEvent OnDamaged;
		private bool _canAttack = true;
		private bool _isEntered;
		private int _isEnterColliderCount;

		private void OnEnable()
		{
			_canAttack = true;
			_isEnterColliderCount = 0;
			_isEntered = false;
		}
		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!TryGetDamageReceiver(collider, out DamageReceiver receiver))
				return;

			_isEnterColliderCount++;

			if (!_canAttack) return;

			if (_isEntered) return;

			Damage(receiver);
			_isEntered = true;
		}


		private void OnTriggerExit2D(Collider2D collision)
		{

			if (TryGetDamageReceiver(collision,out DamageReceiver receiver)) { 
				_isEnterColliderCount--;
				//Debug.Log(_isEnterColliderCount);
				if (_isEnterColliderCount == 0)
					_isEntered = false;
			}
		}

		private void Damage(DamageReceiver receiver)
		{
			receiver.ReceiveDamage(new DamageInfo(_attacker, _damage));
			OnDamaged?.Invoke();
			if (_isOneHitDamage)
			{
				_canAttack = false;
			}



		}
		private bool TryGetDamageReceiver(Collider2D collider,out DamageReceiver receiver)
		{
			if (IsContainLayer(collider.gameObject.layer, _targetLayer))
			{
				DamageReceiver targetReceiver = collider.GetComponentInParent<DamageReceiver>();
				if (targetReceiver != null)
				{
					receiver = targetReceiver;
					return true;
				}
			}
			receiver = null;
			return false;
		}
		public void SetDamage(float damage)
		{
			_damage = damage;
		}

		public void SetAttacker(GameObject attacker)
		{
			_attacker = attacker;
			_canAttack = true; 
		}


		public void ResetTrigger()
		{
			_canAttack = true;
		}

		private bool IsContainLayer(int layer,LayerMask layerMask)
		{
			return ((1 << layer) & layerMask) != 0;
		}
	}
}