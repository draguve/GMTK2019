using UnityEngine;

namespace SnSMovement.Character
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class CharacterMotor : MonoBehaviour
	{
		public float speed = 5f;
		public float accelerationDuration = 0.5f;

		private Rigidbody2D rb;
		private Collider2D _collider;

		private Vector2 goalVelocity;
		private Vector2 currentVelocityRef;
		
		
		public float rayCastDistance = 2f;
		public LayerMask collisionMask;

		private bool _canMoveRight = true;
		private bool _canMoveLeft = true;
		private void Start ()
		{
			rb = GetComponent<Rigidbody2D> ();
			_collider = gameObject.GetComponentInChildren<Collider2D>();
		}

		private void Update ()
		{
			goalVelocity.Normalize ();
			goalVelocity *= speed;
			float x = Mathf.SmoothDamp (rb.velocity.x, goalVelocity.x, ref currentVelocityRef.x, accelerationDuration, float.MaxValue, Time.deltaTime);
			rb.velocity = new Vector2(x,rb.velocity.y);
			
			RaycastHit2D hit = Physics2D.Raycast((transform.position),Vector2.right, rayCastDistance, collisionMask);
			if (hit)
			{
				if (Physics2D.IsTouching(_collider, hit.collider))
				{
					_canMoveRight = false;
				}
				else
				{
					_canMoveRight = true;
				}
			}
        
			hit = Physics2D.Raycast(transform.position,Vector2.left, rayCastDistance, collisionMask);
			if (hit)
			{
				if (Physics2D.IsTouching(_collider, hit.collider))
				{
					_canMoveLeft = false;
				}
				else
				{
					_canMoveLeft = true;
				}	
			}
			
		}

		public void Move (Vector2 direction)
		{
			goalVelocity = direction;
		}

		public void MoveHorizontal (float amount)
		{
			if (!_canMoveLeft && amount < 0) amount = 0;
			if (!_canMoveRight && amount > 0) amount = 0;
			goalVelocity.x = amount;
		}
		
		public void MoveVertical(float amount)
		{
			goalVelocity.y = amount;
		}

		public void Jump(float jumpVelocity)
		{
			var velocity1 = rb.velocity;
			Vector2 velocity = new Vector2(velocity1.x,Mathf.Min(velocity1.y + jumpVelocity,jumpVelocity));
			rb.velocity = velocity;
		}
	}
}