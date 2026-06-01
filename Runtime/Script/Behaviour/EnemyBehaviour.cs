using UnityEngine;
using Unity.Behavior;
using UnityEngine.Events;
namespace Enemy
{
	public abstract class EnemyBehaviour :MonoBehaviour
	{
		public UnityEvent OnStartEvent;
		public UnityEvent OnEndEvent;
		public bool IsRunning { get; private set; }
		public Node.Status OnStart()
		{

			OnStartEvent?.Invoke();
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
			return OnUpdateProcess();
		}
		public void OnEnd()
		{
			OnEndEvent?.Invoke();
			OnEndProcess();
			IsRunning = false;
		}


		protected abstract Node.Status OnStartProcess();
		protected abstract Node.Status OnUpdateProcess();
		protected abstract void OnEndProcess();
	}

}
