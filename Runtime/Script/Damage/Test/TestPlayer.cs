using Damage;
using UnityEngine;
namespace Damage
{
	public class TestPlayer : MonoBehaviour
	{
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		public void GetDamage(DamageInfo info)
		{
			Debug.Log($"{info.Attacker.name} attack {info.Damage} Damage");
		}
	}


}
