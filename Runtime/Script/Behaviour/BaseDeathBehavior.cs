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
		public override Node.Status OnStart()
		{
			_animator.SetTrigger("Death");
			return Node.Status.Success;
		}

		public override Node.Status OnUpdate()
		{
			return Node.Status.Success;
		}

		public override void OnEnd()
		{
			
		}
	}
}



