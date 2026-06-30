using UnityEngine;
namespace Enemy.Callback 
{
	public class DestroyCallBack : MonoBehaviour
	{
		public void Destory()
		{
			if (PoolManager.Instance != null)
				PoolManager.Instance.ReleaseObject(gameObject);
			else
				Destroy(gameObject);
		}
	}
}


