
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Enemy
{
	public class EnemyEventRegistry : MonoBehaviour
	{
		[SerializeField]
		private AnimationEventRelay _animationEventRelay;

		[SerializeField]
		private List<EventContainer> _eventContainers = new();

		private Dictionary<string, UnityEvent<string>> _eventCache = new();


		private void Awake()
		{
			foreach(var container in _eventContainers)
			{
				_eventCache.Add(container.EventName, container.Events);
			}
			_animationEventRelay.OnTriggerEvent += ExecuteEvent ;
		}


		private void OnDestroy()
		{
			_animationEventRelay.OnTriggerEvent -= ExecuteEvent;
		}
		

		public void ExecuteEvent(string eventName)
		{
			if (_eventCache.TryGetValue(eventName, out UnityEvent<string> unityEvent))
			{
				unityEvent?.Invoke(eventName);
			}
		}


		[Serializable]
		public  class EventContainer
		{
			public string EventName;
			public UnityEvent<string> Events;
		}
	}

	
}

