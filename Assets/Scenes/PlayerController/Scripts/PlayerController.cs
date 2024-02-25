using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Mode { Free, Locked_TopDown, Locked_Chair }
    private Mode playerMode;

    [Header("Cursor")]
    [SerializeField] GameObject cursorPrefab;
	PlayerCursor cursor;

	public void Start()
	{
		cursor = Instantiate(cursorPrefab.gameObject).GetComponent<PlayerCursor>() ?? GetComponentInChildren<PlayerCursor>();
        cursor.triggerStay += TryInteract;

		playerMode = Mode.Locked_TopDown;
		
		gameObject.transform.position = Camera.main.transform.position;
		gameObject.transform.rotation = Camera.main.transform.rotation;
	}

	public void Update()
	{
		switch (playerMode)
		{
			case Mode.Free:
				break;
			case Mode.Locked_TopDown:
			case Mode.Locked_Chair:
				Cursor();
				break;
		}
	}

	public void TryGrab(ref Collider col)
    {
        if (col.TryGetComponent<Rigidbody>(out var rb))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				rb.transform.parent = cursor.transform;
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
	public void TryInteract(ref Collider col)
	{
		if (col.TryGetComponent<Interactable>(out var interactable))
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				interactable.OnInteract();
				return;
			}
		}
	}

	public void Cursor()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hits = Physics.RaycastAll(ray, Mathf.Infinity);

		if (hits.Length > 0)
			cursor.transform.position = hits[0].point;
	}
}
