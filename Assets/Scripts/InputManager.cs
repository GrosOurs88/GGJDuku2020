using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float inputDistance = 3;
    public float inputDiameter = 1;
    

    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = LayerMask.GetMask("Clickable");
            RaycastHit[] hit = Physics.SphereCastAll(camera.transform.position, inputDiameter, camera.transform.forward, inputDistance,
                layerMask);

            foreach (var raycastHit in hit)
            {
                raycastHit.collider.BroadcastMessage("OnClick");
            }
        }
    }
    
}
