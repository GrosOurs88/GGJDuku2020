using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementScript : MonoBehaviour
{
    public float speed;


    void Update()
    {
        transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
    }
}
