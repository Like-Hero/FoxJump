using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum AudioType
{
    MainAudio,
    BGMAudio,
    EffectAudio
}
public class AudioSettingController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioType audioType;
    public Sprite muteIcon;
    public Sprite normalIcon;
    public Image audioIcon;
    private float m_prevVolume;
    private Slider m_slider;
    private bool isMute;
    private void Awake()
    {
        m_slider = GetComponent<Slider>();
        isMute = false;
    }
    public void SetVolume(float value)
    {
        isMute = Mathf.Abs(value - m_slider.minValue) < Mathf.Epsilon;
        switch (audioType)
        {
            case AudioType.MainAudio:
                audioMixer.SetFloat("MainVolume", isMute ? -80 : value);
                break;
            case AudioType.BGMAudio:
                audioMixer.SetFloat("BGMVolume", isMute ? -80 : value);
                break;
            case AudioType.EffectAudio:
                audioMixer.SetFloat("EffectVolume", isMute ? -80 : value);
                break;
        }
        ChangeVolumeMode();
    }
    private void ChangeVolumeMode()
    {
        audioIcon.sprite = isMute ? muteIcon : normalIcon;
    }
    public void ChangeVolumeEvent()
    {
        if (isMute)
        {
            m_slider.value = m_prevVolume;
        }
        else
        {
            m_prevVolume = m_slider.value;
            m_slider.value = m_slider.minValue;
        }
        ChangeVolumeMode();
    }
}
