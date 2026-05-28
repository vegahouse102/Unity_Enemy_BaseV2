using Unity.Behavior;
using UnityEngine;

namespace Enemy 
{
	



	public class TurnBehaviour : EnemyBehaviour
	{
		[SerializeField]
		EnemyCurDirectionHandler _directonHandler;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert( _directonHandler != null );
#endif
		}
		public override void OnEnd()
		{
		}

		public override Node.Status OnStart()
		{
			_directonHandler.Turn();
			return Node.Status.Success;
		}

		public override Node.Status OnUpdate()
		{
			return Node.Status.Success;
		}
	}

}