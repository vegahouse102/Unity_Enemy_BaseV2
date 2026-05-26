using System;
using Unity.Behavior;
using UnityEngine;
using Sensor;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsOnCliff", story: "[self] is on cliff", category: "Conditions/Enemy", id: "a76adc0fdddbd7133aab061b188b25bb")]
public partial class IsOnCliffCondition : Condition
{
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	SensorContext _context;

	public override bool IsTrue()
	{
		if (_context == null)
			return false;
		return !_context.GroundSensor.IsGroundedLeft
			|| !_context.GroundSensor.IsGroundedRight;
	}

	public override void OnStart()
	{
		if(_context==null)
			_context = Self.Value.gameObject.GetComponent<SensorContext>();
#if UNITY_EDITOR
		Debug.Assert(_context != null);
#endif
	}

	public override void OnEnd()
	{
	}
}
