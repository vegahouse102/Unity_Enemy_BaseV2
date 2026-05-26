using Damage;
using Unity.Behavior;
using UnityEngine;
namespace Enemy
{
	public class EnemyStateController : MonoBehaviour
	{
		[SerializeField]
		private BehaviorGraphAgent _enemyBehaviorAgent;
		[SerializeField]
		private GameObject _rootObject;
		private BlackboardVariable<bool> _isFrontLeft;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert( _enemyBehaviorAgent != null );
#endif
		}
		private void Start()
		{
			_enemyBehaviorAgent.GetVariable<bool>("IsFrontLeft", out _isFrontLeft);
#if UNITY_EDITOR
			Debug.Assert(_isFrontLeft != null);
#endif
		}
		public void Dead(DamageInfo info)
		{
			_enemyBehaviorAgent.SetVariableValue<bool>("IsDead", true);
		}
		public void Hurt(DamageInfo info)
		{
			_enemyBehaviorAgent.SetVariableValue<bool>("IsHurt", true);
		}
		public void SetAggressive(bool value)
		{
			_enemyBehaviorAgent.SetVariableValue<bool>("IsPeacefull", !value);
			
		}
		public void Turn()
		{

			_isFrontLeft.Value = !_isFrontLeft.Value;
			_rootObject.transform.Rotate(Vector3.up, 180);
		}


		public int GetXDirection()
		{
			return _isFrontLeft.Value ? -1 : 1;
		}
		public GameObject GetEnemyObject() => _rootObject;
	}
}

