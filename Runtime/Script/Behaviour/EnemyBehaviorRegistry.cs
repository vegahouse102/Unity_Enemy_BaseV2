using UnityEngine;
using System.Collections.Generic;
using System;
namespace Enemy
{
	public class EnemyBehaviorRegistry : MonoBehaviour
	{
		[SerializeField]
		private List<BehaviourContainer> _baseBehaviourContainers = new();

		[SerializeField]
		private List<BehaviourContainer> _childBehaviourContainers = new();

		private Dictionary<string, EnemyBehaviour> _behaviours = new();
		private void Awake()
		{
			foreach (var behaviour in _baseBehaviourContainers)
			{
				AddOrOverride(behaviour);
			}
			foreach (var behaviour in _childBehaviourContainers)
			{
				AddOrOverride(behaviour);
			}
		}

		private void AddOrOverride(BehaviourContainer behaviour)
		{
			if (string.IsNullOrEmpty(behaviour.BehaviourName) || behaviour.EnemyBehaviour == null)
			{

				Debug.Log($"Wrong behaviour");

				return;
			}
			_behaviours[behaviour.BehaviourName] = behaviour.EnemyBehaviour;
		}

		public EnemyBehaviour GetBehaviourOrNull(string name)
		{
			if (_behaviours.TryGetValue(name, out var behaviour))
			{
				return behaviour;
			}
			Debug.Log($"didn't find behavor {name}");
			return null;
		}
	}
	[Serializable]
	public class BehaviourContainer
	{
		[SerializeField]
		private string _behaviorName;
		[SerializeField]
		private EnemyBehaviour _enemyBehaviour;

		public string BehaviourName => _behaviorName;
		public EnemyBehaviour EnemyBehaviour => _enemyBehaviour;
	}

}
