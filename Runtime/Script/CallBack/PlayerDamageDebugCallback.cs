using UnityEngine;
using Damage;
namespace Enemy.Callback
{
	public class PlayerDamageDebugCallback : MonoBehaviour
	{
		public void DamageCallback(DamageInfo info)
		{
			UnityEngine.Debug.Log($"Attacker {info.Attacker} Damage {info.Damage}");
		}
	}
}

