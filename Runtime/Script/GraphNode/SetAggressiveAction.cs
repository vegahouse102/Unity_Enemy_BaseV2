using System;
using Unity.Behavior;
using UnityEngine;
using Enemy;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAggressive", story: "[self] Set Aggressive [value]", category: "Enemy", id: "13b78f16ea3ad9991272a1cab01cf5c5")]
public partial class SetAggressiveAction : Action
{
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	[SerializeReference] public BlackboardVariable<bool> Value;

	EnemyStateController _baseBehaviour;
	protected override Status OnStart()
	{
		if(_baseBehaviour==null)
			_baseBehaviour = Self.Value.GetComponent<EnemyStateController>();
		if (_baseBehaviour == null)
			return Status.Failure;
		_baseBehaviour.SetAggressive(Value);
		return Status.Success;
	}

	protected override Status OnUpdate()
	{
		return Status.Success;
	}

	protected override void OnEnd()
	{

	}
}

