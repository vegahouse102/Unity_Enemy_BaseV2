using UnityEngine;
namespace Sensor
{
	public class EntityGroundSensor : SensorBase
	{

		[SerializeField]
		float _downRayLength = 0.1f;
		[SerializeField]
		int _downRayCount = 5;
		public bool IsGroundedLeft { get; private set; }
		public bool IsGroundedRight { get; private set; }

		public bool IsOnGround { get; private set; }

		private void FixedUpdate()
		{
			ClifCheck();
			BottomCheck();
		}

		private void ClifCheck()
		{
			IsGroundedLeft = Physics2D.Raycast(
				new Vector2(_bodyCollider.bounds.min.x, _bodyCollider.bounds.min.y)
				, Vector2.down
				, _downRayLength, _groundMask);

			IsGroundedRight = Physics2D.Raycast(
				new Vector2(_bodyCollider.bounds.max.x, _bodyCollider.bounds.min.y)
				, Vector2.down
				, _downRayLength, _groundMask);

		}
		private void BottomCheck()
		{
			IsOnGround = false;

			if (_downRayCount <= 0)
				return;
			float minx = _bodyCollider.bounds.min.x;
			float maxx = _bodyCollider.bounds.max.x;
			float _diff = (maxx - minx) / (_downRayCount - 1);

			float curx = _bodyCollider.bounds.min.x;

			for (int i = 0; i < _downRayCount; i++)
			{
				Vector2 pos = new Vector2(curx, _bodyCollider.bounds.min.y);
				RaycastHit2D hit = Physics2D.Raycast(
					pos
					, Vector2.down, _downRayLength, _groundMask);
#if UNITY_EDITOR

				Debug.DrawRay(pos, Vector3.down * _downRayLength);
#endif
				curx += _diff;
				//Debug.Log(curx);
				if (hit.normal == Vector2.up)
				{
					IsOnGround = true;
					break;
				}
			}
		}
	}
}

