using Unity.Behavior;
using UnityEngine;
using System;

namespace Enemy
{
	public class EnemyCurDirectionHandler : MonoBehaviour
	{
		[SerializeField]
		BehaviorGraphAgent _agent;
		[SerializeField]
		EnemyEntity _enemyEntity;

		private BlackboardVariable<bool> _isFrontLeft;


		public event System.Action OnTurn;
		private void Start()
		{
			_agent.GetVariable<bool>("IsFrontLeft", out _isFrontLeft);
#if UNITY_EDITOR
			Debug.Assert(_isFrontLeft != null);
#endif
		}
		public int GetXDirection()
		{
			return _isFrontLeft.Value ? -1 : 1;
		}

		public void SetXDirection(bool xDirection)
		{
			_isFrontLeft.Value = xDirection;
		}
		public void Turn()
		{
			if (_isFrontLeft == null) return;
			
			_isFrontLeft.Value = !_isFrontLeft.Value;
			_enemyEntity.transform.Rotate(Vector3.up, 180);
			OnTurn?.Invoke();
		}
	}
}

