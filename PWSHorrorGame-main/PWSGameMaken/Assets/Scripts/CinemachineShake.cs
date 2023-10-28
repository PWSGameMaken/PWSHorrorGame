using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
	public static CinemachineShake Instance { get; private set; }	

	private	CinemachineVirtualCamera  cinemachineVirtualCamera;
	private float shakeTimerTotal;
	private float shakeTimer;
	private float startingIntensity;
	private GameStatsMB gameStatsMB;

	private float defaultIntensity;
	private float defaultFrequency;

	private bool _hasClimaxed = false;

	private void Awake()
	{
		Instance = this;
		cinemachineVirtualCamera  = GetComponent<CinemachineVirtualCamera>();
		gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
	}

	public void ShakeCamera(float intensity, float frequency, float time)
	{
		CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
			cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		
		defaultIntensity = cinemachineBasicMultiChannelPerlin.m_AmplitudeGain;
		defaultFrequency = cinemachineBasicMultiChannelPerlin.m_FrequencyGain;
		
		cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
		cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

		startingIntensity = intensity;
		shakeTimerTotal = time;
		//shakeTimer = time;
	}

	private void Update()
	{
		if(shakeTimerTotal > 0)
		{
			if(_hasClimaxed == false)
			{
				shakeTimer += Time.deltaTime;
				CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
					cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

				cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1- (shakeTimer / shakeTimerTotal));

				if (shakeTimer >= shakeTimerTotal)
				{
					_hasClimaxed = true;
					gameStatsMB.CollapseMap();
				}

			}
			else if(_hasClimaxed == true)
			{
				shakeTimer -= Time.deltaTime;
				CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
					cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

				cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
				if(shakeTimer <= 0f)
				{
					ResetVariables(cinemachineBasicMultiChannelPerlin);
				}
			}
		}
	}

	private void ResetVariables(CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin)
	{
		shakeTimerTotal = 0f;
		_hasClimaxed = false;
		cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = defaultIntensity;
		cinemachineBasicMultiChannelPerlin.m_FrequencyGain = defaultFrequency;
	}
}
