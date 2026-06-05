using UnityEngine;

namespace Enemy
{
	public class ZFightingPreventer : MonoBehaviour
	{
		private float _baseZPos;
		private void Awake()
		{
			_baseZPos = transform.position.z;
		}
		private void OnEnable()
		{
			Vector3 pos = transform.position;
			float offset = Random.Range(-0.01f, -0.0001f);
			pos.z = _baseZPos+offset;
			transform.position = pos;
		}
	}
}

