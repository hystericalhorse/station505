using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Mode { Free, Locked_TopDown, Locked_Chair }
    private Mode playerMode;

	private Vector3 fakeRotation = Vector3.zero;
	private Quaternion fakeRotationQuat = Quaternion.identity;

    [Header("Locked Mode Properties")]
	private Vector2 handPos;
    [SerializeField] GameObject handPrefab;
    PlayerCursor hand;

	public void Start()
	{
		hand = Instantiate(handPrefab.gameObject).GetComponent<PlayerCursor>() ?? GetComponentInChildren<PlayerCursor>();
        hand.triggerStay += TryHold;

		playerMode = Mode.Locked_TopDown;

		gameObject.transform.position = Camera.main.transform.position;
		gameObject.transform.rotation = Camera.main.transform.rotation;
	}

	public void Update()
	{
		RayThroughCamera();

		switch (playerMode)
		{
			case Mode.Free:
				break;
			case Mode.Locked_TopDown:
			case Mode.Locked_Chair:
				
				break;
		}
	}

	public void TryHold(ref Collider col)
    {
        if (col.TryGetComponent<Rigidbody>(out var rb))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				rb.transform.parent = hand.transform;
				return;
			}

			if (Input.GetKey(KeyCode.Mouse0))
			{

			}

			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				rb.transform.parent = null;
			}

		}
	}

	public void MoveHand()
	{
		Vector2 mov = Vector2.zero;
		mov.x = Input.GetAxis("Mouse X");
		mov.y = Input.GetAxis("Mouse Y");
		mov.Normalize();

		hand.transform.Translate(mov.x * Time.deltaTime, 0, mov.y * Time.deltaTime);
		Debug.Log($"Move: {mov}");
	}

	public void RayThroughCamera()
	{
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		var hits = Physics.RaycastAll(ray, Mathf.Infinity);

		if (hits.Length > 0)
			hand.transform.position = hits[0].point;
	}
}
