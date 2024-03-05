using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2f;
    [Range(0,360)] public float maxYAngle = 80f; 
    [Range(0,360)] public float maxXAngle = 30f;

    private float rotationX = 0;
	private float destinationX = 0;

	private float rotationY = 0;
	private float destinationY = 0;

    public bool Enable = false;
	private Vector2 mousePos;

	private Vector2 screen = Vector2.zero;
	private Vector2 screenCenter = Vector2.zero;

	float distanceFromCenterX;
	float distanceFromCenterY;

	private void Start()
	{
		mousePos = Vector2.zero;

		screen.x = Screen.width;
		screen.y = Screen.height;

		screenCenter = screen * 0.5f;

		distanceFromCenterX = screenCenter.x - (2 * (screenCenter.x / 3));
		distanceFromCenterY = screenCenter.y - (2 * (screenCenter.y / 3));
	}

	void Update()
    {
        if (!Enable) return;
		mousePos = Input.mousePosition;

		var xy = mousePos - screenCenter;
		if (Mathf.Abs(xy.x) > distanceFromCenterX) // HORIZONTAL (YAW)
		{
			destinationY += xy.normalized.x * sensitivity;
			destinationY = Mathf.Clamp(destinationY, -maxXAngle, maxXAngle);
		}

		if (Mathf.Abs(xy.y) > distanceFromCenterY) // VERTICAL (PITCH)
		{
			destinationX -= xy.normalized.y * sensitivity;
			destinationX = Mathf.Clamp(destinationX, -maxYAngle, maxYAngle);
		}

		// RECENTER
		if (Input.GetKeyDown(KeyCode.Space))
		{
			destinationX = 0;
			destinationY = 0;
		}

		//float mouseX = Input.GetAxis("Mouse X");
		//float mouseY = Input.GetAxis("Mouse Y");
		//
		//transform.Rotate(Vector3.up * mouseX * sensitivity);
		//
		//
		//rotationX -= mouseY * sensitivity;
		//rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);

		if (!Mathf.Approximately(destinationX, rotationX))
		{
			rotationX = Mathf.Lerp(rotationX, destinationX, Time.smoothDeltaTime);
		}

		if (!Mathf.Approximately(destinationY, rotationY))
		{
			rotationY = Mathf.Lerp(rotationY, destinationY, Time.smoothDeltaTime);
		}

		transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
	}
}
