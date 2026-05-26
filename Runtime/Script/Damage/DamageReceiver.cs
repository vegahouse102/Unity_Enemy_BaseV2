using UnityEngine;
using UnityEngine.Events;

namespace Damage
{
	public class DamageReceiver : MonoBehaviour
	{
		public UnityEvent<DamageInfo> OnReceiveDamage;

		public void ReceiveDamage(DamageInfo damageInfo)
		{
			OnReceiveDamage?.Invoke(damageInfo);
		}
	}
}

