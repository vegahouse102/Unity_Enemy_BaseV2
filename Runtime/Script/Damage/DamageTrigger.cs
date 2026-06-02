
using System.Security.Cryptography;
using UnityEngine;

namespace Damage
{
	public class DamageTrigger : MonoBehaviour
	{

		[Header("데미지 컴포넌트")]
		[Header("감지하고자 하는 collider의 tag가 Hitbox여야함")]


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

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!_canAttack) return;

			if (_isOnUpdate) return;

			if (!collider.gameObject.CompareTag("Hitbox")) return;

			if (collider.gameObject.layer == _targetLayer)
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

			if (collider.gameObject.layer == _targetLayer)
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
	}
}