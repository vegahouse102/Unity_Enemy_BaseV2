using UnityEngine;
using Unity.Behavior;
namespace Enemy
{
	public abstract class EnemyBehaviour :MonoBehaviour
	{
		public abstract Node.Status OnStart();
		public abstract Node.Status OnUpdate();
		public abstract void OnEnd();
	}

}
