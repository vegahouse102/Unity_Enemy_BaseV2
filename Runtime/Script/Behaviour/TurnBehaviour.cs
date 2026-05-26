using Unity.Behavior;
using UnityEngine;

namespace Enemy 
{
	



	public class TurnBehaviour : EnemyBehaviour
	{
		[SerializeField]
		EnemyStateController _baseBehaviour;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert( _baseBehaviour != null );
#endif
		}
		public override void OnEnd()
		{
		}

		public override Node.Status OnStart()
		{
			_baseBehaviour.Turn();
			return Node.Status.Success;
		}

		public override Node.Status OnUpdate()
		{
			return Node.Status.Success;
		}
	}

}