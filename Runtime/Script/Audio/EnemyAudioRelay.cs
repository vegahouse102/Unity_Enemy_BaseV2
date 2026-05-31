
using UnityEngine;

namespace Enemy.Audio
{
	public class EnemyAudioRelay : MonoBehaviour
	{
		[SerializeField]
		EnemyAudioRegistry _registry;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_registry != null);
#endif
		}

		public void TriggerAudioStart(string name)
		{
			_registry.PlaySound(name);
		}
		public void TriggerAudioStop(string name)
		{
			_registry.StopSound(name);
		}
	}
}

