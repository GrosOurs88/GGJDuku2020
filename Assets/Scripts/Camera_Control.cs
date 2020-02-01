using UnityEngine;
using System.Collections;

public class Camera_Control : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
    float rotationX = 0F;

    void Update ()
	{
        //enum MouseXAndY
        if (axes == RotationAxes.MouseXAndY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);

            //rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            //rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
            //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        //enum MouseX
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
		}

        //enum MouseY
        else
        {
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}


