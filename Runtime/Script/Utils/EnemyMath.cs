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

			if (acceleration < 0.01f)
				return (curMoveDistance < totalDistance) ? maxVelocity : endVelocity;

			totalDistance = Mathf.Max(0.01f, totalDistance);
			curMoveDistance = Mathf.Clamp(curMoveDistance, 0f, totalDistance);


			float maxAccDistance = (maxVelocity * maxVelocity - startVelocity * startVelocity) / (2f * acceleration);
			float maxDecDistance = (maxVelocity * maxVelocity - endVelocity * endVelocity) / (2f * acceleration);

			maxAccDistance = Mathf.Max(0f, maxAccDistance);
			maxDecDistance = Mathf.Max(0f, maxDecDistance);


			if (maxAccDistance + maxDecDistance > totalDistance)
			{

				float peakDistance = (2f * acceleration * totalDistance + endVelocity * endVelocity - startVelocity * startVelocity) / (4f * acceleration);
				peakDistance = Mathf.Clamp(peakDistance, 0f, totalDistance);

				float peakVelocity = Mathf.Sqrt(Mathf.Max(0f, startVelocity * startVelocity + 2f * acceleration * peakDistance));

				if (curMoveDistance < peakDistance)
				{

					return startVelocity + (acceleration * curMoveDistance);
				}
				else
				{

					float distFromPeak = curMoveDistance - peakDistance;
					return Mathf.Max(endVelocity, peakVelocity - (acceleration * distFromPeak));
				}
			}

			else
			{
				if (curMoveDistance < maxAccDistance)
				{
					return startVelocity + (acceleration * curMoveDistance);
				}

				else if (curMoveDistance < totalDistance - maxDecDistance)
				{
					return maxVelocity;
				}

				else
				{
					float distFromDecelStart = curMoveDistance - (totalDistance - maxDecDistance);
					return Mathf.Max(endVelocity, maxVelocity - (acceleration * distFromDecelStart));
				}
			}
		}
	}
}