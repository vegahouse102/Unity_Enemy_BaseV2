using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Enemy;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get Player", story: "[self] Find Player [outPlayer]", category: "Enemy", id: "5b6a6f25db2e9c9dbb18d718a0dfa123")]
public partial class GetPlayerAction : Action
{
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	[SerializeReference] public BlackboardVariable<GameObject> OutPlayer;
	PlayerFinder _enemyPlayerFinder;
	protected override Status OnStart()
	{
		if(_enemyPlayerFinder==null)
			_enemyPlayerFinder = Self.Value.gameObject.gameObject.GetComponent<PlayerFinder>();
		if(_enemyPlayerFinder != null)
		{
			OutPlayer.Value = _enemyPlayerFinder.GetPlayerOrNull();
			if (OutPlayer.Value != null)
				return Status.Success;

		}
		
		return Status.Failure;
	}

	protected override Status OnUpdate()
	{
		return Status.Success;
	}

	protected override void OnEnd()
	{
	}
}

