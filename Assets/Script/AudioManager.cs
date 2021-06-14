using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _ins;
    public static AudioManager Ins { get { return _ins; } }

    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip hurtSound;
    [SerializeField]
    private AudioClip getConllectionSound;

    private AudioSource audioSource;
    void Start()
    {
        if(_ins == null)
        {
            _ins = this;
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void JumpAudioPlay()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }
    public void HurtAudioPlay()
    {
        audioSource.clip = hurtSound;
        audioSource.Play();
    }
    public void GetCollectionAudioPlay()
    {
        audioSource.clip = getConllectionSound;
        audioSource.Play();
    }
}
