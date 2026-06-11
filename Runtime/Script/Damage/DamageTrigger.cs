
using UnityEngine;

namespace Damage
{
	public class DamageTrigger : MonoBehaviour
	{

		[Header("데미지 컴포넌트")]
		[Header("감지하고자 하는 collider의 tag가 Hitbox여야함")]


		[SerializeField] private float _damage;
		[SerializeField] private bool _isOneHitDamage = true;
		[SerializeField] private LayerMask _targetLayer;
		[SerializeField] private bool _isOnUpdate = false;
		[SerializeField] GameObject _attacker;
		private bool _canAttack = true;

		private void OnEnable()
		{
			_canAttack = true;
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!_canAttack) return;

			if (_isOnUpdate) return;

			if (!collider.gameObject.CompareTag("Hitbox")) return;

			if (IsContainLayer(collider.gameObject.layer, _targetLayer))
			{
				if (collider.attachedRigidbody.TryGetComponent<DamageReceiver>(out var playerDamageReceiver))
				{
					playerDamageReceiver.ReceiveDamage(new DamageInfo(_attacker, _damage));

			
					if (_isOneHitDamage)
					{
						_canAttack = false;
					}
				}
			}
		}


		private void OnTriggerStay2D(Collider2D collider)
		{
			if (!_canAttack) return;

			if (!_isOnUpdate) return;

			if (!collider.gameObject.CompareTag("Hitbox")) return;



			if (IsContainLayer(collider.gameObject.layer, _targetLayer))
			{
				if (collider.attachedRigidbody.TryGetComponent<DamageReceiver>(out var playerDamageReceiver))
				{
					playerDamageReceiver.ReceiveDamage(new DamageInfo(_attacker, _damage));


					if (_isOneHitDamage)
					{
						_canAttack = false;
					}
				}
			}
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