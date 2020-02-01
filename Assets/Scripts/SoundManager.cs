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

    public AudioClip blowtorch;

    public AudioClip fanOn;
    public AudioClip fanOff;
    public AudioClip fanReset;

    public AudioClip engineOn;
    public AudioClip engineOff;
    public AudioClip engineReset;

    public AudioClip repartitorOff;
    public AudioClip repartitorReset;

    public AudioClip windLight;
    public AudioClip windHeavy;

    private float indexGlassDamagedLight = 0;
    private float indexGlassDamagedHeavy = 0;
    public AudioClip glassDamageLight;
    public AudioClip glassDamageHeavy;

    public AudioSource engineSource;
    public AudioSource fanSource;
    public AudioSource sfxSource;
    public AudioSource windSource;
    public AudioSource blowtorchSource;

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


    public void GlassDamageLight()
    {
        sfxSource.PlayOneShot(glassDamageLight);
        indexGlassDamagedLight += 1;
        CheckForWindSound();
    }
    public void GlassDamageHeavy()
    {
        sfxSource.PlayOneShot(glassDamageHeavy);
        indexGlassDamagedHeavy += 1;
        CheckForWindSound();
    }
    public void GlassDamageLightRepaired()
    {
        indexGlassDamagedLight -= 1;
        CheckForWindSound();
    }
    public void GlassDamageHeavyRepaired()
    {
        indexGlassDamagedHeavy -= 1;
        CheckForWindSound();
    }


    public void BlowtorchOn()
    {
        blowtorchSource.Play();
    }
    public void BlowtorchOff()
    {
        blowtorchSource.Stop();
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


    public void CheckForWindSound()
    {
        if (indexGlassDamagedLight > 0 && indexGlassDamagedHeavy == 0)
        {
            windSource.clip = windLight;
            windSource.Play();
        }

        else if (indexGlassDamagedHeavy > 0)
        {
            windSource.clip = windHeavy;
            windSource.Play();
        }

        else
        {
            windSource.Stop();
        }
    }
}
