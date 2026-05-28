using UnityEngine;
using Damage;
using Unity.Behavior;
namespace Enemy
{
	public class EnemyDamageRelay : MonoBehaviour
	{
		[SerializeField]
		private BehaviorGraphAgent _agent;

		private BlackboardVariable<bool> _isDead;
		private BlackboardVariable<bool> _isHurt;
		void Start()
		{
			_agent.GetVariable<bool>("IsDead", out _isDead);
			_agent.GetVariable<bool>("IsHurt", out _isHurt);
#if UNITY_EDITOR
			Debug.Assert(_isDead != null);
			Debug.Assert(_isHurt != null);
#endif
		}

		public void Dead(DamageInfo info)
		{
			_isDead.Value = true;
		}
		public void Hurt(DamageInfo info)
		{
			_isHurt.Value = true;
		}
	}
}

