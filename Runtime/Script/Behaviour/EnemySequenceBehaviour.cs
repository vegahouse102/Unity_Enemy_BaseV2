using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

namespace Enemy
{
	public class EnemySequenceBehaviour : EnemyBehaviour
	{
		[SerializeField]
		private List<EnemyBehaviour> _enemyBehaviours = new();
		private EnemyBehaviour _curBehaviour;
		private IEnumerator _sequenceProcess;
		protected override Node.Status OnStartProcess()
		{
			if (_enemyBehaviours.Count == 0)
				return Node.Status.Success;
			_sequenceProcess = SequenceProcess();
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{

			if (_sequenceProcess.MoveNext())
				return Node.Status.Running;
			return Node.Status.Success;
		}

		protected override void OnEndProcess()
		{
			if (_curBehaviour != null)
				_curBehaviour.OnEnd();
		}

		private IEnumerator SequenceProcess()
		{
			foreach(var behaviour in _enemyBehaviours)
			{
				_curBehaviour = behaviour; 
				behaviour.OnStart();
				while (behaviour.IsRunning)
				{
					behaviour.OnUpdate();
					yield return null;
				}
				behaviour.OnEnd();
			}
			yield break ;
		}

	}
}

