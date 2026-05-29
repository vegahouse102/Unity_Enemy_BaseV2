using Unity.Behavior;
using UnityEngine;

namespace Enemy
{
	public class SimpleAnimationBehaviour : EnemyBehaviour, IAnimationEventReceiver
	{



		[SerializeField]
		Animator _animator;
		[SerializeField]
		AnimationBehaviourRelay _animationBehaviourRely;
		[SerializeField]
		private string _triggerName;

		private bool _isDone = false;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_animator != null);
			Debug.Assert(_animationBehaviourRely != null);
#endif
		}
		protected override Node.Status OnStartProcess()
		{
			_isDone = false;
			_animationBehaviourRely.SetAnimationBehaviour(this);
			_animator.SetTrigger(_triggerName);
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
			if(!_isDone)
				return Node.Status.Running;
			return Node.Status.Success;
		}
		protected override void OnEndProcess()
		{
			_animationBehaviourRely.SetAnimationBehaviour(null);
		}

		public void OnAnimationDone()
		{
			_isDone = true;
		}

	}
}

