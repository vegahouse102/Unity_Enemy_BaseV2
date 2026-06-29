using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class PoolManager : MonoBehaviour
	{

		public static PoolManager Instance { get; private set; }
		private Dictionary<EntityId, Queue<GameObject>> _cache = new(); // 유효조건 : queue내부에는 destroy되지 않은 비활성화된 오브젝트여야함 _pooledObjectList있는 요소가 전부 존재함

		private LinkedList<GameObject> _pooledObjectList = new LinkedList<GameObject>(); // 앞에서부터 먼저 release된 destory되지 않은 비활성화된 오브젝트 _cache에 있는 오브젝트의 요소들이 전부 있다.
		[SerializeField,Min(1)]
		private int _maxCount = 200; // _pooledObjectList에 들어갈 수 있는 최대오브젝트수 
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
		/// <summary>
		/// 전제조건은 없음 생성할 프리팹넣으면됨
		/// 
		/// 나온 오브젝트는 PooledObject가 있어야 하고 그 PooledObject는 유효한값이어야 한다.
		/// null 넣으면 null나옴
		/// return된 오브젝트는 활성화되어있음
		/// </summary>
		/// <param name="prefab"></param>
		/// <returns></returns>
		public GameObject GetObject(GameObject prefab ,Vector2 position)
		{
			if (prefab == null) 
				return null;

			EntityId id = prefab.GetEntityId();

			if (_cache.TryGetValue(id, out Queue<GameObject> pool) && pool.Count > 0)
			{
				GameObject instance = pool.Dequeue();
				PooledObject pooledObject = instance.GetComponent<PooledObject>();
				_pooledObjectList.Remove(pooledObject.node);//  이미 넣어졌다면 pool과 _pooledObjectList에 없어져야함
				pooledObject.isPooled = false;
				instance.transform.position = position;
				instance.SetActive(true);//넣어져있던 비활성화 오브젝트를 활성화함
				return instance;
			}
		
			return GetNewObject(prefab,position);
		}
		/// <summary>
		/// 이 메서드를 실행하면 반환이 된다.
		/// instance의조건은 반환할 오브젝트인데 아무 오브젝트면 된다. 
		/// 
		/// 메서드가 끝나면 _cache에 집어넣어지고 _poolObjectList가 업데이트된다.
		/// 
		/// 집어넣을 수 없으면 destory되고
		/// 넣을수 있으면 비활성화된다.
		/// </summary>
		/// <param name="instance"></param>
		public void ReleaseObject(GameObject instance)
		{
			if (instance == null) 
				return;

			instance.SetActive(false); // 가독성을 위해 정석 표현으로 수정

			if (instance.TryGetComponent<PooledObject>(out PooledObject pooledObject))
			{
				if(pooledObject.isPooled)//이미 넣어져있는경우 넣지 않기
					return;

				if (_pooledObjectList.Count >= _maxCount)
				{
					DequeuePooledObject();
				}

				


				// 주머니가 없으면 생성하고, 있으면 가져옵니다.
				if (!_cache.ContainsKey(pooledObject.id))
				{
					_cache[pooledObject.id] = new Queue<GameObject>();
				}

				_cache[pooledObject.id].Enqueue(instance);
				_pooledObjectList.AddLast(instance);
				pooledObject.node = _pooledObjectList.Last;
				pooledObject.isPooled = true;
			}
			else
			{
				
				Destroy(instance);
			}
		}
		//기존리스트맨처음꺼 없애기
		/// <summary>
		/// _pooledObjectList의 맨처음값을 없앤다.
		/// </summary>
		private void DequeuePooledObject()
		{
			if (_pooledObjectList.Count <= 0)
				return;
			LinkedListNode<GameObject> instanceNode = _pooledObjectList.First;

			_pooledObjectList.RemoveFirst();

			/// 리스트의 첫번째 요소는 없어져야함

			GameObject targetObj = instanceNode.Value; //없어져야할 오브젝트 


			//_pooledObjectList안의 내용물은 유효하기때문에 null이 넣어질 수 없다.  targetObj는 null 이 될 수 없다.
			PooledObject pooledObject = targetObj.GetComponent<PooledObject>();

			if (_cache.TryGetValue(pooledObject.id, out Queue<GameObject> pool))
			{
				pool.Dequeue();
			}
			Destroy(targetObj);

		}
		/// <summary>
		/// 생성된후에는 pooledobject가 붙고 poolmanager의 자식오브젝트가 되어야 한다. 
		/// prefab은 null이 아니어야한다.
		/// </summary>
		/// <param name="prefab">오브젝트를 생성할 prefab</param>
		/// <returns></returns>
		private GameObject GetNewObject(GameObject prefab, Vector2 position)
		{
			GameObject instance = Instantiate(prefab, position,Quaternion.identity);
			PooledObject pooledObject = instance.AddComponent<PooledObject>();
			pooledObject.id = prefab.GetEntityId();
			pooledObject.node = null; // 초기화시에는 null이어야함
			pooledObject.isPooled = false;
			instance.transform.parent = transform;
			return instance;
		}
	}

	public class PooledObject : MonoBehaviour
	{
		public LinkedListNode<GameObject> node; // 조건 처음 생성시에는 null 반환될때 반환될시  LinkedList의 노드 주소가 저장됨
		public EntityId id; // 오브젝트의 prefab의 entityid 오브젝트가 생성될시에 초기화됨
		public bool isPooled;
	}
}