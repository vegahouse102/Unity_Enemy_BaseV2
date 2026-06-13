
using Enemy.Audio;
using UnityEngine;

namespace Enemy
{
	public class DebugCallback : MonoBehaviour
	{
		[SerializeField]
		private string _debugStr;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}
		public void callback()
		{
			Debug.Log(_debugStr);
		}
	}
}


