using UnityEngine;

public class Timer
{
	private float _duration;
	private float _startTime;
	private bool _isRunning;

	public Timer(float duration)
	{
		_duration = duration;
	}

	public void Start()//timer사용시 start호출해야함
	{
		_startTime = Time.time;
		_isRunning = true;
	}

	public void Reset()
	{
		_startTime = Time.time;
	}

	public bool IsDone()
	{
		if (!_isRunning) return false;
		return Time.time >= _startTime + _duration;
	}

	public float GetProgress()
	{
		if (!_isRunning) return 0f;
		return Mathf.Clamp01((Time.time - _startTime) / _duration);
	}
}