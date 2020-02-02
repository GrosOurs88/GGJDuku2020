using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public bool activateModule;
    private bool allowfire = true;
    public GameObject bullet;
    public float fireRate = 0.25f;
    public AudioSource shotSource;


    public void Update()
    {
        if (activateModule)
        {
            if ((Input.GetMouseButton(0)) && allowfire)
            {
                StartCoroutine(Fire());
            }
        }
    }

    public IEnumerator Fire()
    {
        allowfire = false;

        Instantiate(bullet, transform.position, transform.rotation);
        shotSource.Play();

        yield return new WaitForSeconds(fireRate);

        allowfire = true;
    }
}
