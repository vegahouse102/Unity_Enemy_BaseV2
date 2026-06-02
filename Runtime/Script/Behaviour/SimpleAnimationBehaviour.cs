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

		[Tooltip("animation트리거타입 Trigger or Bool")]
		[SerializeField]
		AnimationType _animationType = AnimationType.Trigger;
		[SerializeField]
		[Tooltip("Animation을 시작할 trigger이름")]
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
			if(_animationType == AnimationType.Trigger)
				_animator.SetTrigger(_triggerName);
			else
			{
				_animator.SetBool(_triggerName,true);
			}
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
			if(_animationType == AnimationType.Bool)
			{
				_animator.SetBool(_triggerName, false);
			}
		}

		public void OnAnimationDone(string eventName)
		{
			if(eventName==_animationDoneEventName)
				_isDone = true;
		}
		private enum AnimationType
		{
			Bool,
			Trigger
		}
	}
}

