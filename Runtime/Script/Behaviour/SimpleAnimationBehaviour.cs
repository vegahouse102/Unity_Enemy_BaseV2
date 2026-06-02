using Unity.Behavior;
using UnityEngine;

namespace Enemy
{
	public class SimpleAnimationBehaviour : EnemyBehaviour
	{



		[SerializeField]
		Animator _animator;
		[SerializeField]
		AnimationEventRelay _animationEventBroadcaster;
		[SerializeField]
		[Tooltip("Animation을 끝낼 trigger이름")]
		private string _triggerName;
		[SerializeField]
		[Tooltip("AnimationEventBroadcaster를 이용해 animator에 이벤트를 붙일때 실행할 이름")]
		private string _animationDoneEventName;

		private bool _isDone = false;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_animator != null);
			Debug.Assert(_animationEventBroadcaster != null);
#endif
			_animationEventBroadcaster.OnTriggerEvent += OnAnimationDone;
		}
		private void OnDestroy()
		{
			_animationEventBroadcaster.OnTriggerEvent -= OnAnimationDone;
		}
		protected override Node.Status OnStartProcess()
		{
			_isDone = false;
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

		}

		public void OnAnimationDone(string eventName)
		{
			if(eventName==_animationDoneEventName)
				_isDone = true;
		}

	}
}

