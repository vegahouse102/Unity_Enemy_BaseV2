using UnityEngine;
namespace Sensor
{
	public abstract class SensorBase : MonoBehaviour
	{
		[SerializeField]
		protected BoxCollider2D _bodyCollider;
		[SerializeField]
		protected LayerMask _groundMask;
	}
}

