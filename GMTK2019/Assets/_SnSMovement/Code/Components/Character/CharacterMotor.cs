using UnityEngine;

namespace SnSMovement.Character
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class CharacterMotor : MonoBehaviour
	{
		public float speed = 5f;
		public float accelerationDuration = 0.5f;

		private Rigidbody2D rb;

		private Vector2 goalVelocity;
		private Vector2 currentVelocityRef;

		private void Start ()
		{
			rb = GetComponent<Rigidbody2D> ();
		}

		private void Update ()
		{
			goalVelocity.Normalize ();
			goalVelocity *= speed;
			float x = Mathf.SmoothDamp (rb.velocity.x, goalVelocity.x, ref currentVelocityRef.x, accelerationDuration, float.MaxValue, Time.deltaTime);
			rb.velocity = new Vector2(x,rb.velocity.y);
		}

		public void Move (Vector2 direction)
		{
			goalVelocity = direction;
		}

		public void MoveHorizontal (float amount)
		{
			goalVelocity.x = amount;
		}
		
		public void MoveVertical(float amount)
		{
			goalVelocity.y = amount;
		}

		public void Jump(float jumpVelocity)
		{
			rb.velocity = rb.velocity + Vector2.up * jumpVelocity;
		}
	}
}