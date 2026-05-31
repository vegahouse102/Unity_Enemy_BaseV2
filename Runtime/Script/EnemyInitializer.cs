using UnityEngine;

namespace Enemy
{
	public class EnemyInitializer : MonoBehaviour
	{
		[SerializeField]
		EnemyHealth _health;
		[SerializeField]
		EnemyHealthSO _healthSO;


		void Start()
		{

#if UNITY_EDITOR
			Debug.Assert(_health != null);

			Debug.Assert(_healthSO != null);
#endif
			_health.Initialize(_healthSO);

		}

	}
}

