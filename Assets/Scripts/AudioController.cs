using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> audioClips;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShotFire()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    public void PlayerWalkOne()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void PlayerWalkTwo()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void PlayerWalkThree()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
    public void PlayerWalkFour()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }


}
