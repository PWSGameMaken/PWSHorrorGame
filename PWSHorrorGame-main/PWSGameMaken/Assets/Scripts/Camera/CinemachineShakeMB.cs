using Cinemachine;
using UnityEngine;

public class CinemachineShakeMB : MonoBehaviour
{
	public static CinemachineShakeMB Instance { get; private set; }

	private CinemachineVirtualCamera cinemachineVirtualCamera;
	private float _shakeTimerTotal = 0;
	private float _shakeTimer = 0;
	private float _startingIntensity = 0;

	private float defaultIntensity;
	private float defaultFrequency;

	private bool _hasClimaxed = false;
	
	private CinemachineBasicMultiChannelPerlin _cBMCP;

	private void Awake()
	{
		Instance = this;
		cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
	}

	public void ShakeCamera(float intensity, float frequency, float time)
	{
		_cBMCP = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		defaultIntensity = _cBMCP.m_AmplitudeGain;
		defaultFrequency = _cBMCP.m_FrequencyGain;

		_cBMCP.m_AmplitudeGain = intensity;
		_cBMCP.m_FrequencyGain = frequency;

		_startingIntensity = intensity;
		_shakeTimerTotal = time;
	}

	private void Update()
	{
		if (_shakeTimerTotal == 0) return;

		if (_hasClimaxed == false)
		{
			_shakeTimer += Time.deltaTime;
			if (_shakeTimer >= _shakeTimerTotal/2) Climax();
		}
		else
		{
			_shakeTimer -= Time.deltaTime;
			if (_shakeTimer <= 0f) UnshakeCamera();
		}

		_cBMCP.m_AmplitudeGain = GetAmplitude();
	}

	private void Climax()
	{
		_hasClimaxed = true;
	}

	private float GetAmplitude()
	{
		var currentIntensity = 1 - (_shakeTimer / _shakeTimerTotal);
		return Mathf.Lerp(_startingIntensity, 0f, currentIntensity);
	}

	private void UnshakeCamera()
	{
		_shakeTimerTotal = 0f;
		_hasClimaxed = false;
		_cBMCP.m_AmplitudeGain = defaultIntensity;
		_cBMCP.m_FrequencyGain = defaultFrequency;
	}
}
