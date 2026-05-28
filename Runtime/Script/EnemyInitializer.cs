using UnityEngine;

namespace Enemy
{
	public class EnemyInitializer : MonoBehaviour
	{
		[SerializeField]
		Animator _animator;
		[SerializeField]
		EnemyHealth _health;


		[SerializeField]
		RuntimeAnimatorController _animationController;
		[SerializeField]
		EnemyHealthSO _healthSO;


		void Start()
		{

#if UNITY_EDITOR
			Debug.Assert(_animator != null);
			Debug.Assert(_health != null);
			Debug.Assert(_animationController != null);
			Debug.Assert(_healthSO != null);
#endif
			_animator.runtimeAnimatorController = _animationController;
			_health.Initialize(_healthSO);

		}

	}
}

