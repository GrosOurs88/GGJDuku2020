using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip bipConfirm;
    public AudioClip bipDenied;

    public AudioClip shock;
    public AudioClip lighting;
    public AudioClip fire;

    public AudioClip fanOn;
    public AudioClip fanOff;
    public AudioClip fanReset;

    public AudioClip engineOn;
    public AudioClip engineOff;
    public AudioClip engineReset;

    public AudioClip repartitorOff;
    public AudioClip repartitorReset;

    public AudioSource engineSource;
    public AudioSource fanSource;
    public AudioSource sfxSource;

    private void Start()
    {
        engineSource.clip = engineOn;
        engineSource.Play();

        fanSource.clip = fanOn;
        fanSource.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            
        }        
    }

    public void BipConfirm()
    {
        sfxSource.PlayOneShot(bipConfirm);
    }
    public void BipDenied()
    {
        sfxSource.PlayOneShot(bipDenied);
    }
    public void Shock()
    {
        sfxSource.PlayOneShot(shock);
    }
    public void Lighting()
    {
        sfxSource.PlayOneShot(lighting);
    }


    public void RepartitorOff()
    {
        sfxSource.PlayOneShot(repartitorOff);
    }
    public void RepartitorReset()
    {
        sfxSource.PlayOneShot(repartitorReset);
    }


    public void FanOff()
    {
        fanSource.Stop();
    }
    public void FanReset()
    {
        fanSource.Play();
    }


    public void EngineOff()
    {
        engineSource.Stop();
    }
    public void EngineReset()
    {
        engineSource.Play();
    }
}
