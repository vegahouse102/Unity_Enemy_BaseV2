using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class PoolManager : MonoBehaviour
	{
		public static PoolManager Instance { get; private set; }
		private Dictionary<EntityId, Queue<GameObject>> _cache = new();

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public GameObject GetObject(GameObject prefab)
		{
			if (prefab == null) 
				return null;

			EntityId id = prefab.GetEntityId();


			if (_cache.TryGetValue(id, out Queue<GameObject> pool) && pool.Count > 0)
			{
				GameObject instance = pool.Dequeue();
				instance.SetActive(true);
				return instance;
			}

			return GetNewObject(prefab);
		}

		public void ReleaseObject(GameObject instance)
		{
			if (instance == null) 
				return;

			instance.SetActive(false); // 가독성을 위해 정석 표현으로 수정

			if (instance.TryGetComponent<PooledObject>(out PooledObject pooledObject))
			{
				// 주머니가 없으면 생성하고, 있으면 가져옵니다.
				if (!_cache.ContainsKey(pooledObject.id))
				{
					_cache[pooledObject.id] = new Queue<GameObject>();
				}

				_cache[pooledObject.id].Enqueue(instance);
			}
			else
			{
				
				Destroy(instance);
			}
		}

		private GameObject GetNewObject(GameObject prefab)
		{
			GameObject instance = Instantiate(prefab);
			PooledObject pooledObject = instance.AddComponent<PooledObject>();
			pooledObject.id = prefab.GetEntityId();

			instance.transform.parent = transform;
			return instance;
		}
	}

	public class PooledObject : MonoBehaviour
	{
		public EntityId id;
	}
}