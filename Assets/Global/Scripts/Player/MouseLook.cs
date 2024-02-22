using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxYAngle = 80f; 

    private float rotationX = 0;

    public bool Enable = false;

    void Update()
    {
        if (Enable)
        {

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");


            transform.Rotate(Vector3.up * mouseX * sensitivity);


            rotationX -= mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);


            transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
}
