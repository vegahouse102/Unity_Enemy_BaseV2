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
		public override void OnEndProcess()
		{
		}

		public override Node.Status OnStartProcess()
		{
			_directonHandler.Turn();
			return Node.Status.Success;
		}

		public override Node.Status OnUpdateProcess()
		{
			return Node.Status.Success;
		}
	}

}