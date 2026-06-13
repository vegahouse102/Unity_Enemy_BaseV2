using UnityEngine;
using UnityEngine.Events;



namespace Enemy.Callback
{
	public class UniversalCallback : MonoBehaviour
	{
		public UnityEvent _event;
		public void TriggerEvent() 
		{
			_event?.Invoke();
		}
	}
}

