
using UnityEngine;

namespace Damage
{
	public class DamageTrigger : MonoBehaviour
	{
		[SerializeField] private float _damage;
		[SerializeField] private bool _isOneHitDamage = true;
		[SerializeField] private string _targetLayerName = "Player";
		[SerializeField] private bool _isOnUpdate = false;

		private bool _canAttack = true;
		private int _targetLayer;
		private GameObject _attacker;

		private void OnEnable()
		{
			_canAttack = true;
			
			_targetLayer = LayerMask.NameToLayer(_targetLayerName);
			_attacker = transform.root.gameObject;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!_canAttack) return;

			if (_isOnUpdate) return;

			if (collision.gameObject.layer == _targetLayer)
			{
				if (collision.attachedRigidbody.TryGetComponent<DamageReceiver>(out var playerDamageReceiver))
				{
					playerDamageReceiver.ReceiveDamage(new DamageInfo(_attacker, _damage));

			
					if (_isOneHitDamage)
					{
						_canAttack = false;
					}
				}
			}
		}


		private void OnTriggerStay2D(Collider2D collision)
		{
			if (!_canAttack) return;

			if (!_isOnUpdate) return;

			if (collision.gameObject.layer == _targetLayer)
			{
				if (collision.attachedRigidbody.TryGetComponent<DamageReceiver>(out var playerDamageReceiver))
				{
					playerDamageReceiver.ReceiveDamage(new DamageInfo(_attacker, _damage));


					if (_isOneHitDamage)
					{
						_canAttack = false;
					}
				}
			}
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
	}
}