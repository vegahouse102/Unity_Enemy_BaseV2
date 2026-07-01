using UnityEngine;
using Damage;
using Unity.Behavior;
namespace Enemy
{
	public class EnemyDamageRelay : MonoBehaviour
	{
		[SerializeField]
		private BehaviorGraphAgent _agent;

		private BlackboardVariable<bool> _isDeath;
		private BlackboardVariable<bool> _isHurt;
		void Start()
		{
			_agent.GetVariable<bool>("IsDeath", out _isDeath);
			_agent.GetVariable<bool>("IsHurt", out _isHurt);
#if UNITY_EDITOR
			Debug.Assert(_isDeath != null);
			Debug.Assert(_isHurt != null);
#endif
		}

		public void Death(DamageInfo info)
		{
			_isDeath.Value = true;
		}
		public void Hurt(DamageInfo info)
		{
			_isHurt.Value = true;
		}
	}
}

