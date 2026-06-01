
using UnityEngine;

namespace Enemy.Audio
{
	/// <summary>
	/// animator에 붙이는 audio 이벤트
	/// </summary>
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

