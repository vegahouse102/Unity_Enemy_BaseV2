using UnityEngine;


namespace Enemy
{
	[CreateAssetMenu(fileName = "EnemyHealthSO", menuName = "Scriptable Objects/Enemy/HealthSO")]
	public class EnemyHealthSO : ScriptableObject
	{
		[SerializeField]
		private int _maxHealth;

		public int MaxHealth => _maxHealth;

	}

}

