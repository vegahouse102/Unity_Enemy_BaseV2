using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

namespace Enemy
{

	//저장된 행동을 동시에 실행
	public class EnemyParallelBehaviour : EnemyBehaviour
	{
		

		[SerializeField]
		private List<EnemyBehaviour> _enemyBehaviours = new();


		[Tooltip("All은 모든 행동이 끝날때까지 실행, Any는 한 행동이라도 종료되면 즉시 종료")]
		[SerializeField]
		private ParallelType _parallelType;

		private int _stopBehaviourCount;
		protected override Node.Status OnStartProcess()
		{
			_stopBehaviourCount = 0;
			foreach(var behaviour in _enemyBehaviours)
			{
				behaviour.OnStart();
				if (!behaviour.IsRunning)
				{
					if(_parallelType==ParallelType.Any)// any모드일때 행동하나라도 끝났을때
						return Node.Status.Success;
					else
						_stopBehaviourCount++;
				}
					
			}
			if (_stopBehaviourCount == _enemyBehaviours.Count)
			{
				return Node.Status.Success;
			}
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
			foreach (var behaviour in _enemyBehaviours)
			{
				if (!behaviour.IsRunning)
					continue;


				behaviour.OnUpdate();
				if (!behaviour.IsRunning)
				{
					if(_parallelType==ParallelType.Any)
						return Node.Status.Success;
					else
						_stopBehaviourCount++;
				}
			}
			if (_stopBehaviourCount == _enemyBehaviours.Count)
			{
				return Node.Status.Success;
			}
			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			foreach (var behaviour in _enemyBehaviours)
			{	
				if(behaviour.IsRunning)
					behaviour.OnEnd();
			}
		}

		enum ParallelType
		{
			All,
			Any
		}
	}
}

