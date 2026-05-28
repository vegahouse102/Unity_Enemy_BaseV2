using UnityEngine;

namespace Sensor
{
	public class PlayerFinder : MonoBehaviour
	{
		GameObject _player;
		private void Awake()
		{
			_player = GameObject.FindWithTag("Player");
		}
		public GameObject GetPlayerOrNull()
		{
			return _player;
		}
	}

}
