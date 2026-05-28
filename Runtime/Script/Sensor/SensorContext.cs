using UnityEngine;
namespace Sensor
{
	public class SensorContext : MonoBehaviour
	{
		[SerializeField] private EntityCeilSensor _ceilSensor;
		[SerializeField] private EntityGroundSensor _groundSensor;
		[SerializeField] private EntityWallSensor _wallSensor;
		[SerializeField] private PlayerFinder _playerFinder;

		public EntityCeilSensor CeilSensor => _ceilSensor;
		public EntityGroundSensor GroundSensor => _groundSensor;
		public EntityWallSensor WallSensor => _wallSensor;

		public PlayerFinder PlayerFinder => _playerFinder;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_ceilSensor != null);
			Debug.Assert(_groundSensor != null);
			Debug.Assert(_wallSensor != null);
			Debug.Assert(_playerFinder != null);
#endif
		}
	}

}
