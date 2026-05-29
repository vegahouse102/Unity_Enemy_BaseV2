using UnityEngine;
using Unity.Behavior;
namespace Enemy
{
	public abstract class EnemyBehaviour :MonoBehaviour
	{
		public bool IsRunning { get; private set; }
		public Node.Status OnStart()
		{
			IsRunning = true;
			Node.Status status = OnStartProcess();
			if(status != Node.Status.Running)
			{
				IsRunning = false;
			}
			return status;
		}
		public Node.Status OnUpdate()
		{
			Node.Status status = OnUpdateProcess();
			if(status != Node.Status.Running)
			{
				IsRunning = false;
			}
			return status;
		}
		public void OnEnd()
		{
			OnEndProcess();
			IsRunning = false;
		}


		protected abstract Node.Status OnStartProcess();
		protected abstract Node.Status OnUpdateProcess();
		protected abstract void OnEndProcess();
	}

}
