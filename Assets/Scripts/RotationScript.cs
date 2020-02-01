using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed;

    private Vector3 rotVec;

    private void Start()
    {
        rotVec = new Vector3(Random.Range(10f, 30f), Random.Range(10f, 30f), Random.Range(10f, 30f));
    }


    void Update()
    {
        transform.Rotate(rotVec * rotationSpeed * Time.deltaTime);
    }
}
