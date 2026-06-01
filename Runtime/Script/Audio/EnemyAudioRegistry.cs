using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Audio
{
	public class EnemyAudioRegistry : MonoBehaviour
	{
		[Header("AudioSource 목록")]
		[SerializeField]
		private List<AudioSourceContainer> _audioSourceContainers = new List<AudioSourceContainer>();

		private Dictionary<string, AudioSourceContainer> _audioCache = new Dictionary<string, AudioSourceContainer>();

		private void Awake()
		{

			InitializeAudioCache();
		}

		private void InitializeAudioCache()
		{
			foreach (var cue in _audioSourceContainers)
			{
				// 공백 키값 예외 처리
				if (string.IsNullOrEmpty(cue.Name)) continue;

				// 인스펙터에서 실수로 같은 이름(Key)을 두 번 등록했을 때의 중복 버그 방지
				if (!_audioCache.ContainsKey(cue.Name))
				{
					_audioCache.Add(cue.Name, cue);
				}
				else
				{

#if UNITY_EDITOR
					Debug.LogWarning($"[{gameObject.name}] 중복된 사운드 키가 발견되었습니다: '{cue.Name}'. 첫 번째 등록된 사운드만 캐싱됩니다.");
#endif
				}
			}
		}

		public void PlaySound(string name)
		{
			
			if (_audioCache.TryGetValue(name, out AudioSourceContainer sourceContaincer))
			{

				if(sourceContaincer.IsOneShotAudio)
				{
					sourceContaincer.AudioSource.PlayOneShot(sourceContaincer.AudioSource.clip);
				}
				else
				{
					sourceContaincer.AudioSource.Play();
				}
			}
			else
			{
#if UNITY_EDITOR
				Debug.LogWarning($"[{gameObject.name}] '{name}' 키에 해당하는 AudioCue를 레지스트리에서 찾을 수 없습니다. 오타가 없는지 확인하세요.");
#endif
			}
		}

		public void StopSound(string name)
		{
			if (_audioCache.TryGetValue(name, out AudioSourceContainer sourceContainer))
			{
				sourceContainer.AudioSource.Stop();
			}
			else
			{
#if UNITY_EDITOR
				Debug.LogWarning($"[{gameObject.name}] '{name}' 키에 해당하는 AudioCue를 레지스트리에서 찾을 수 없습니다. 오타가 없는지 확인하세요.");
#endif
			}
		}
		public void StopAllSounds()
		{
			foreach(var source in _audioSourceContainers)
			{
				source.AudioSource.Stop();
			}
		}
	}

	[System.Serializable]
	public class AudioSourceContainer 
	{
		public string Name;
		
		public AudioSource AudioSource;
		[Tooltip("걷는 소리처럼 소리가 곂쳐서 나오도록하는 설정")]
		public bool IsOneShotAudio = false;

	}

}