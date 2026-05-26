using System;
using Unity.Behavior;
using UnityEngine;
using Enemy;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ExecuteBehavior", story: "[self] execute [name] behaviour", category: "Enemy", id: "3b6d2a1f59e3c6a836c3a833534c1b96")]
public partial class ExecuteBehaviorAction : Action
{
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	[SerializeReference] public BlackboardVariable<string> Name;

	EnemyBehaviorRegistry _registry;
	EnemyBehaviour _behavior;
	protected override Status OnStart()
	{
		if (_registry == null)
			_registry = Self.Value.gameObject.GetComponent<EnemyBehaviorRegistry>();
		if (_registry == null)
			return Status.Failure;
		_behavior = _registry.GetBehaviourOrNull(Name);
		if (_behavior == null)
			return Status.Failure;
		return _behavior.OnStart();
	}

	protected override Status OnUpdate()
	{
		if (_behavior == null)
			return Status.Failure;
		return _behavior.OnUpdate();
	}

	protected override void OnEnd()
	{
		if (_behavior == null)
			return;
		_behavior.OnEnd();
	}
}

