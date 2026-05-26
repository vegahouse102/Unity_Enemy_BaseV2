using UnityEngine;
namespace Sensor
{
	public class SensorContext : MonoBehaviour
	{
		[SerializeField] private EntityCeilSensor _ceilSensor;
		[SerializeField] private EntityGroundSensor _groundSensor;
		[SerializeField] private EntityWallSensor _wallSensor;

		public EntityCeilSensor CeilSensor => _ceilSensor;
		public EntityGroundSensor GroundSensor => _groundSensor;
		public EntityWallSensor WallSensor => _wallSensor;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_ceilSensor != null);
			Debug.Assert(_groundSensor != null);
			Debug.Assert(_wallSensor != null);
#endif
		}
	}

}
