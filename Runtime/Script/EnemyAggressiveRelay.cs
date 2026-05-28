using Unity.Behavior;
using UnityEngine;

namespace Enemy
{
	public class EnemyAggressiveRelay : MonoBehaviour
	{
		[SerializeField]
		BehaviorGraphAgent _agent;
		BlackboardVariable<bool> _isPeaceful;
		private void Start()
		{
			_agent.GetVariable("IsPeacefull",out _isPeaceful);
#if UNITY_EDITOR
			Debug.Assert(_isPeaceful!=null);
#endif
		}

		public void SetPeacefull(bool value)
		{
			_isPeaceful.Value = value;
		}
	}
}

