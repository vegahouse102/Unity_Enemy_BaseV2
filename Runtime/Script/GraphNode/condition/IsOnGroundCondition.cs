using Sensor;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsOnGround", story: "[self] is on ground", category: "Conditions/Enemy", id: "1ee354e87dc7ea72596ac63c49ce8551")]
public partial class IsOnGroundCondition : Condition
{
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	SensorContext _context;
	public override bool IsTrue()
	{
		if (_context == null)
			return false;
		return _context.GroundSensor.IsOnGround;
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
