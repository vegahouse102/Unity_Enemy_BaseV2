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
		protected override void OnEndProcess()
		{
		}

		protected override Node.Status OnStartProcess()
		{
			_directonHandler.Turn();
			return Node.Status.Success;
		}

		protected override Node.Status OnUpdateProcess()
		{
			return Node.Status.Success;
		}
	}

}