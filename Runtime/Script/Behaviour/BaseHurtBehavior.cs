using Unity.Behavior;
using UnityEngine;

namespace Enemy 
{
	public class BaseHurtBehavior : EnemyBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		[SerializeField]
		private Color _hitColor;
		[SerializeField]
		private float _blinkTime;

		private float _curTime;

		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_spriteRenderer != null);
#endif	
		}
		protected override Node.Status OnStartProcess()
		{
			_curTime = 0f;
			_spriteRenderer.color = _hitColor;
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
			if (_curTime > _blinkTime)
				return Node.Status.Success;
			_curTime += Time.deltaTime;
			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			_spriteRenderer.color = Color.white;
		}
	}
}



