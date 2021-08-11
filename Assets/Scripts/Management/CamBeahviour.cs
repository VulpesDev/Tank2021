using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamBeahviour : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShortShake()
    {
        StartCoroutine(ShakeCam(10, 10, 0.1f));
    }
    IEnumerator ShakeCam(float amplitude, float frequency, float seconds)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        yield return new WaitForSeconds(seconds);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
