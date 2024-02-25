using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// Mouse-Screen Interactions
/// </summary>
public class ScreenInteractable : MonoBehaviour
{
	bool _hovering;

	public UnityEvent onInteract;
	public UnityEvent onMouseEnter;
	public UnityEvent onMouseStay;
	public UnityEvent onMouseExit;

	public virtual void Start()
	{
		onMouseEnter.AddListener(() => { _hovering = true; });
		onMouseExit.AddListener(() => { _hovering = false; });
	}

	public virtual void Update()
	{
		if (_hovering)
		{
			onMouseStay?.Invoke();

			if (Input.GetMouseButtonDown(0))
				onInteract?.Invoke();
		}
	}

	private void OnMouseEnter() => onMouseEnter?.Invoke();
	private void OnMouseExit() => onMouseExit?.Invoke();
}