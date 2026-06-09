using UnityEngine;
namespace Enemy.Utils
{
	public static class EnemyMath
	{
		/// <summary>
		/// 사다리꼴/삼각형 등가속도 운동 법칙에 따른 현재 속도를 계산
		/// </summary>
		/// <param name="totalDistance">총 이동해야 할 거리</param>
		/// <param name="curMoveDistance">현재까지 이동한 거리</param>
		/// <param name="maxVelocity">최고 제한 속도</param>
		/// <param name="acceleration">가속도 (감속도도 동일하게 적용)</param>
		/// <param name="startVelocity">진입(초기) 속도</param>
		/// <param name="endVelocity">중단(종료) 속도</param>
		public static float GetAdvancedTrapezoidalVelocity(
			float totalDistance,
			float curMoveDistance,
			float maxVelocity,
			float acceleration,
			float startVelocity = 0f,
			float endVelocity = 0f)
		{
			// 가속도가 없으면 즉시 최대 속도 혹은 종료 속도 반환
			if (acceleration < 0.0001f)
				return (curMoveDistance < totalDistance) ? maxVelocity : endVelocity;

			// 예외 방지 가드
			totalDistance = Mathf.Max(0.01f, totalDistance);
			curMoveDistance = Mathf.Clamp(curMoveDistance, 0f, totalDistance);

			float maxAccDistance = (maxVelocity * maxVelocity - startVelocity * startVelocity) / (2f * acceleration);
			float maxDecDistance = (maxVelocity * maxVelocity - endVelocity * endVelocity) / (2f * acceleration);

			maxAccDistance = Mathf.Max(0f, maxAccDistance);
			maxDecDistance = Mathf.Max(0f, maxDecDistance);

			// =========================================================================
			// 케이스 A: 총 거리가 짧아 최고 속도(maxVelocity)에 도달하지 못하는 경우 (삼각형 궤적)
			// =========================================================================
			if (maxAccDistance + maxDecDistance > totalDistance)
			{
				// [수정] peakDistance 분모 오류 수정
				float peakDistance = (2f * acceleration * totalDistance + endVelocity * endVelocity - startVelocity * startVelocity) / (4f * acceleration);
				peakDistance = Mathf.Clamp(peakDistance, 0f, totalDistance);

				// 최고점에서의 이론상 속도
				float peakVelocity = Mathf.Sqrt(Mathf.Max(0f, startVelocity * startVelocity + 2f * acceleration * peakDistance));

				if (curMoveDistance < peakDistance)
				{
					// [수정] 거리에 따른 정확한 등가속도 공식 적용: v = sqrt(v_0^2 + 2ax)
					return Mathf.Sqrt(Mathf.Max(0f, startVelocity * startVelocity + 2f * acceleration * curMoveDistance));
				}
				else
				{
					// 감속 구간
					float distFromPeak = curMoveDistance - peakDistance;
					return Mathf.Max(endVelocity, Mathf.Sqrt(Mathf.Max(0f, peakVelocity * peakVelocity - 2f * acceleration * distFromPeak)));
				}
			}
			// =========================================================================
			// 케이스 B: 거리가 충분하여 가속 -> 등속 -> 감속을 모두 거치는 경우 (사다리꼴 궤적)
			// =========================================================================
			else
			{
				
				if (curMoveDistance < maxAccDistance)
				{
					return Mathf.Sqrt(Mathf.Max(0f, startVelocity * startVelocity + 2f * acceleration * curMoveDistance));
				}
				
				else if (curMoveDistance < totalDistance - maxDecDistance)
				{
					return maxVelocity;
				}
				
				else
				{
					float distFromDecelStart = curMoveDistance - (totalDistance - maxDecDistance);
					return Mathf.Max(endVelocity, Mathf.Sqrt(Mathf.Max(0f, maxVelocity * maxVelocity - 2f * acceleration * distFromDecelStart)));
				}
			}
		}
	}
}