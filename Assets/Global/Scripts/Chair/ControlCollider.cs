using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class ControlCollider : MonoBehaviour
{
    [SerializeField] private Collider col;

	public UnityEvent onMouseEnter;
	public UnityEvent onMouseStay;
	public UnityEvent onMouseExit;

	private void Start()
	{
		col = GetComponent<Collider>();
	}

	private void OnMouseEnter() => onMouseEnter?.Invoke();
	private void OnMouseOver() => onMouseStay?.Invoke();
	private void OnMouseExit() => onMouseExit?.Invoke();
}
