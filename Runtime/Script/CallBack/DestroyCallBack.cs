using UnityEngine;
namespace Enemy.Callback 
{
	public class DestroyCallBack : MonoBehaviour
	{
		public void Destory()
		{
			Destroy(gameObject);
		}
	}
}


