using System;
using UnityEngine;

namespace Damage
{
	[Serializable]
	public struct DamageInfo
	{

		private float _damage;
		public GameObject Attacker { get; set; }

		public float Damage
		{
			get => _damage;
			set => _damage = Mathf.Max(0, value);
		}
		public DamageInfo(GameObject attacker, float damage)
		{
			Attacker = attacker;
			_damage = Mathf.Max(0, damage);
		}
	}
}

