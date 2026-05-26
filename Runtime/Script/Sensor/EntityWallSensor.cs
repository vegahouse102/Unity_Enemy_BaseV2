using UnityEngine;
namespace Sensor
{
	public class EntityWallSensor : SensorBase
	{
		[Header("Default 와 선택된 레이어를 감지합니다")]
		[SerializeField]
		private float _rayLength = 0.1f;
		public bool IsTouchLeftWall { get; private set; }
		public bool IsTouchRightWall { get; private set;}

		private int defaultLayer;

		private void Awake()
		{
			defaultLayer =  LayerMask.GetMask("Default");
		}

		private void FixedUpdate()
		{

			Vector2 start = new Vector2(_bodyCollider.bounds.min.x, _bodyCollider.bounds.center.y);
			Vector2 top = new Vector2(_bodyCollider.bounds.min.x, _bodyCollider.bounds.max.y);
			IsTouchLeftWall = Physics2D.Raycast(
				start
				, Vector2.left
				, _rayLength, _groundMask| defaultLayer) |

				Physics2D.Raycast(
				top
				, Vector2.left
				, _rayLength, _groundMask | defaultLayer)
				;
#if UNITY_EDITOR
				Debug.DrawLine(start, start + Vector2.left * _rayLength);
				Debug.DrawLine(top, top + Vector2.left * _rayLength);
#endif

			start = new Vector2(_bodyCollider.bounds.max.x, _bodyCollider.bounds.center.y);
			top = new Vector2(_bodyCollider.bounds.max.x, _bodyCollider.bounds.max.y);
			IsTouchRightWall = Physics2D.Raycast(
				start
				, Vector2.right
				, _rayLength, _groundMask| defaultLayer)|
				Physics2D.Raycast(
				top
				, Vector2.right
				, _rayLength, _groundMask | defaultLayer);

#if UNITY_EDITOR
				Debug.DrawLine(start, start + Vector2.right * _rayLength);
			Debug.DrawLine(top, top + Vector2.right * _rayLength);
#endif
		}
	}
}

