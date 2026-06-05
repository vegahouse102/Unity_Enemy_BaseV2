using System;
using UnityEngine;


namespace Enemy
{
	public class EnemyBehaviourRelay : MonoBehaviour
	{
		public event Func<string, EnemyBehaviour> OnRequestBehaviour;

		public EnemyBehaviour GetBehaviourOrNull(string name)
		{
			if (OnRequestBehaviour != null)
			{
				return OnRequestBehaviour?.Invoke(name);
			}

			return null;
		}

	}
}

