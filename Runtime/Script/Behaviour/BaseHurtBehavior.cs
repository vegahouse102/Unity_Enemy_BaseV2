using Unity.Behavior;
using UnityEngine;

namespace Enemy 
{
	public class BaseHurtBehavior : EnemyBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		[SerializeField]
		private float _blinkTime;
		[SerializeField]
		private float _blinkSpeed;

		private float _curTime;
		private float _curBlinkTime;
		private bool _isblink;
		private void Awake()
		{
#if UNITY_EDITOR
			Debug.Assert(_spriteRenderer != null);
#endif	
		}
		protected override Node.Status OnStartProcess()
		{
			_curTime = 0f;
			_curBlinkTime = 0f;
			_isblink = false;
			return Node.Status.Running;
		}

		protected override Node.Status OnUpdateProcess()
		{
			if (_curTime > _blinkTime)
				return Node.Status.Success;
			if( _curBlinkTime >= _blinkSpeed)
			{
				_curBlinkTime = 0f;
				_isblink = !_isblink;
				if (_isblink)
				{
					_spriteRenderer.color = Color.red;
				}
				else
				{
					_spriteRenderer.color = Color.white;
				}
			}
			_curTime += Time.deltaTime;
			_curBlinkTime += Time.deltaTime;
			return Node.Status.Running;
		}

		protected override void OnEndProcess()
		{
			_spriteRenderer.color = Color.white;
		}
	}
}



