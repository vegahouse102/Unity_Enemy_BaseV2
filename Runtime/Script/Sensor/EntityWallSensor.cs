using UnityEngine;

namespace Sensor
{
	public class EntityWallSensor : SensorBase
	{
		[Header("선택된 레이어를 감지합니다")]
		[SerializeField] private float _rayLength = 0.1f;

		[Tooltip("레이 출발지점으로부터 몸통쪽으로 움직일 offset")]
		[SerializeField] private float _insetOffset = 0.05f;

		public bool IsTouchLeftWall { get; private set; }
		public bool IsTouchRightWall { get; private set; }

		private int _combinedLayerMask; // Awake에서 한 번만 계산할 마스터 레이어마스크

		private void Awake()
		{
			_combinedLayerMask = _groundMask;
		}

		private void FixedUpdate()
		{
			// 1. 성능 최적화: bounds 캐싱
			var bounds = _bodyCollider.bounds;

			float centerY = bounds.center.y;
			float maxY = bounds.max.y;

			// 2. 버그 예방: 시작 지점을 콜라이더 내부로 살짝 들여보냅니다.
			float leftStartX = bounds.min.x + _insetOffset;
			float rightStartX = bounds.max.x - _insetOffset;

	
			float totalRayLength = _rayLength + _insetOffset;

			// --- 왼쪽 벽 체크 ---
			Vector2 leftCenter = new Vector2(leftStartX, centerY);
			Vector2 leftTop = new Vector2(leftStartX, maxY);

			IsTouchLeftWall = Physics2D.Raycast(leftCenter, Vector2.left, totalRayLength, _combinedLayerMask) ||
					  Physics2D.Raycast(leftTop, Vector2.left, totalRayLength, _combinedLayerMask);

#if UNITY_EDITOR
			Debug.DrawLine(leftCenter, leftCenter + Vector2.left * totalRayLength, Color.green);
			Debug.DrawLine(leftTop, leftTop + Vector2.left * totalRayLength, Color.green);
#endif

			// --- 오른쪽 벽 체크 ---
			Vector2 rightCenter = new Vector2(rightStartX, centerY);
			Vector2 rightTop = new Vector2(rightStartX, maxY);

			IsTouchRightWall = Physics2D.Raycast(rightCenter, Vector2.right, totalRayLength, _combinedLayerMask) ||
					   Physics2D.Raycast(rightTop, Vector2.right, totalRayLength, _combinedLayerMask);

#if UNITY_EDITOR
			Debug.DrawLine(rightCenter, rightCenter + Vector2.right * totalRayLength, Color.red);
			Debug.DrawLine(rightTop, rightTop + Vector2.right * totalRayLength, Color.red);
#endif
		}
	}
}