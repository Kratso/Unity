using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{

	public float moveTime = 0.1f;

	public LayerMask blockingLayer;

	BoxCollider2D boxCollider;

	Rigidbody2D rigidbody2D;

	float inverseMoveTime;

	protected virtual void Start()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		inverseMoveTime = 1 / moveTime;
	}

	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRD = (transform.position - end).sqrMagnitude;
		while (sqrRD > float.Epsilon)
		{
			Vector3 newpos = Vector3.MoveTowards(rigidbody2D.position, end, inverseMoveTime * Time.deltaTime);
			rigidbody2D.MovePosition(newpos);
			sqrRD = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;

		Vector2 end = start + new Vector2(xDir, yDir);

		boxCollider.enabled = false;

		hit = Physics2D.Linecast(start, end, blockingLayer);

		boxCollider.enabled = true;

		if (hit.transform == null)
		{
			StartCoroutine(SmoothMovement(end));

			return true;
		}

		return false;
	}

	protected virtual void AttemptMove<T>(int xDir, int yDir)
			where T : Component
	{
		RaycastHit2D hit;

		bool canMove = Move(xDir, yDir, out hit);

		if (hit.transform == null)
			return;

		T hitComponent = hit.transform.GetComponent<T>();

		if (!canMove && hitComponent != null)

			OnCantMove(hitComponent);
	}

	protected abstract void OnCantMove<T>(T component)
	where T : Component;

}
