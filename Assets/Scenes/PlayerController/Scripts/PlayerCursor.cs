using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCursor : MonoBehaviour
{
	SphereCollider _collider;

	public delegate void TriggerEnter(ref Collider col);
	public TriggerEnter triggerEnter;
	public delegate void TriggerStay(ref Collider col);
	public TriggerEnter triggerStay;
	public delegate void TriggerExit(ref Collider col);
	public TriggerExit triggerExit;

	private void Awake()
	{
		_collider = GetComponent<SphereCollider>();
		_collider.isTrigger = true;
	}

	private void OnDestroy()
	{
		triggerEnter = null;
		triggerStay = null;
		triggerExit = null;
	}

	private void Update()
	{

	}

	private void OnTriggerEnter(Collider other) => triggerEnter?.Invoke(ref other);
	private void OnTriggerStay(Collider other) => triggerStay?.Invoke(ref other);
	private void OnTriggerExit(Collider other) => triggerExit?.Invoke(ref other);
}