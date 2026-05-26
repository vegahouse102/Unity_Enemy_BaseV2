using System;
using Unity.Behavior;
using UnityEngine;
using Sensor;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsFrontWall", story: "[self] is Front Wall : isLeft [isFrontLeft]", category: "Conditions/Enemy", id: "ccc54770a614dc7c006b16cdaa97b602")]
public partial class IsFrontWallCondition : Condition
{
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	[SerializeReference] public BlackboardVariable<bool> IsFrontLeft;
	SensorContext _context;
	public override bool IsTrue()
	{
		if (_context == null)
			return false;
		if (IsFrontLeft)
		{
			return _context.WallSensor.IsTouchLeftWall;
		}
		return _context.WallSensor.IsTouchRightWall;
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
