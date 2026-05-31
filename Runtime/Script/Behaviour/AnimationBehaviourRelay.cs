using UnityEngine;

namespace Enemy
{
	
	//animatorbehaviour에 사용되는 animationevent
	public class AnimationBehaviourRelay : MonoBehaviour
	{

		IAnimationEventReceiver _animationBehaviour;
		public void SetAnimationBehaviour(IAnimationEventReceiver animationBehaviour)
		{
			_animationBehaviour = animationBehaviour;
		}

		//animation에 사용되는 event
		public void Done()
		{
			_animationBehaviour?.OnAnimationDone();
		}
	}
	public interface IAnimationEventReceiver
	{
		void OnAnimationDone();
	}
}

