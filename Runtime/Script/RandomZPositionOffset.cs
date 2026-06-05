using UnityEngine;

namespace Enemy
{
	public class RandomZPositionOffset : MonoBehaviour
	{
		private void Awake()
		{
			Vector3 pos = transform.position;
			float offset = Random.Range(-0.01f, -0.0001f);
			pos.z += offset;
			transform.position = pos;
		}
	}
}

