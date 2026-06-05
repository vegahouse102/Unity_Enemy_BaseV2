using System;
using UnityEngine;

namespace Enemy
{
	/// <summary>
	/// animator에 사용될 event를 다루는 컴포넌트
	/// </summary>
	//animator에 사용될 event
	public class EnemyEventRelay : MonoBehaviour
	{

		public event Action<string> OnTriggerEvent;
		//animation에 사용되는 event
		public void TriggerEvent(string eventName)
		{
			OnTriggerEvent?.Invoke(eventName);
		}
	}


}

