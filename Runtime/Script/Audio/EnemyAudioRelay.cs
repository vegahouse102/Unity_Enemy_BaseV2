
using System;
using UnityEngine;

namespace Enemy.Audio
{
	/// <summary>
	/// animator event에 붙이는 audio 이벤트
	/// </summary>
	public class EnemyAudioRelay : MonoBehaviour
	{


		public event Action<string> OnPlaySound;
		public event Action<string> OnStopSound;
		public event Action OnAllStopSounds;

		public void TriggerAudioStart(string name)
		{
			OnPlaySound?.Invoke(name);
		}
		public void TriggerAudioStop(string name)
		{
			OnStopSound?.Invoke(name); 
		}

		public void TriggerAudioAllStop()
		{
			OnAllStopSounds?.Invoke();
		}
	}
}

