using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCursor : MonoBehaviour
{
	SphereCollider _collider;

	public delegate void TriggerEnter(ref Collider col);
	public TriggerEnter triggerEnter;
	public delegate void TriggerStay(ref Collider col);
	public TriggerStay triggerStay;
	public delegate void TriggerExit(ref Collider col);
	public TriggerExit triggerExit;

	public delegate void CollisionEnter(ref Collision col);
	public CollisionEnter collisionEnter;
	public delegate void CollisionStay(ref Collision col);
	public CollisionStay collisionStay;
	public delegate void CollisionExit(ref Collision col);
	public CollisionExit collisionExit;

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

	private void OnCollisionEnter(Collision col) => collisionEnter?.Invoke(ref col);
	private void OnCollisionStay(Collision col) => collisionStay?.Invoke(ref col);
	private void OnCollisionExit(Collision col) => collisionExit?.Invoke(ref col);

	private void OnTriggerEnter(Collider other) => triggerEnter?.Invoke(ref other);
	private void OnTriggerStay(Collider other) => triggerStay?.Invoke(ref other);
	private void OnTriggerExit(Collider other) => triggerExit?.Invoke(ref other);
}