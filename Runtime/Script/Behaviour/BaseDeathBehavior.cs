using UnityEngine;

using Unity.Behavior;

namespace Enemy 
{
	public class BaseDeathBehavior : EnemyBehaviour
	{
		[SerializeField]
		Animator _animator;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert( _animator != null );
#endif
		}
		protected override Node.Status OnStartProcess()
		{
			_animator.SetTrigger("Death");
			return Node.Status.Success;
		}

		protected override Node.Status OnUpdateProcess()
		{
			return Node.Status.Success;
		}

		protected override void OnEndProcess()
		{
			
		}
	}
}



