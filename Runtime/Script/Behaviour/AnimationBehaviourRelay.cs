using UnityEngine;

namespace Enemy
{
	public class AnimationBehaviourRelay : MonoBehaviour
	{

		IAnimationEventReceiver _animationBehaviour;
		public void SetAnimationBehaviour(IAnimationEventReceiver animationBehaviour)
		{
			_animationBehaviour = animationBehaviour;
		}
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

