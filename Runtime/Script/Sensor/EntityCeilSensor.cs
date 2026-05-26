using UnityEngine;
namespace Sensor
{
	public class EntityCeilSensor : SensorBase
	{

		[SerializeField]
		float _upRayLength = 0.1f;
		[SerializeField]
		int _upRayCount = 5;

		public bool IsOnCeiling { get; private set; }

		private void FixedUpdate()
		{
			CeilCheck();
		}

		private void CeilCheck()
		{
			IsOnCeiling = false;

			if (_upRayCount <= 0 || _bodyCollider == null) 
				return;


			float minx = _bodyCollider.bounds.min.x;
			float maxx = _bodyCollider.bounds.max.x;

			float _diff = (_upRayCount > 1) ? (maxx - minx) / (_upRayCount - 1) : 0;

			float curx = minx;

			for (int i = 0; i < _upRayCount; i++)
			{
			
				Vector2 pos = new Vector2(curx, _bodyCollider.bounds.max.y);

				RaycastHit2D hit = Physics2D.Raycast(
				    pos,
				    Vector2.up, 
				    _upRayLength,
				    _groundMask); 

#if UNITY_EDITOR
				// 디버그 드로잉도 Raycast 방향(위쪽)과 동일하게 수정
				// Color.red를 추가하여 충돌 감지 레이임을 명확히 표시
				Debug.DrawRay(pos, Vector3.up * _upRayLength, Color.red);
#endif

				curx += _diff;

				if (hit.collider != null && hit.normal.y < -0.9f)
				{
					IsOnCeiling = true;
					break;
				}
			}
		}
	}
}

