using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D rock;
	public Transform rockReleasePoint;
	public Vector2 speed = new Vector2(10, 10);
	public GameObject pivot;
	public int jumpSpeed = 500;

	private Animator animator;
	private Rigidbody2D rb;

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {

		float inputX = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(speed.x * inputX, 0, 0);
		
		movement *= Time.deltaTime;

		if (inputX >= 0.2 || inputX <= -0.2) 
		{
			transform.Translate(movement);
		};

		var angle = Mathf.Atan2 (Input.GetAxis ("Horizontal2"), Input.GetAxis ("Vertical2")) * Mathf.Rad2Deg;
		pivot.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		pivot.transform.position = transform.position;

		if (Input.GetButtonDown("Fire1") ) 
		{
			Rigidbody2D clone;
			clone = Instantiate(rock, rockReleasePoint.position, pivot.transform.rotation) as Rigidbody2D;

			clone.velocity = pivot.transform.TransformDirection(Vector3.down * 5);
		};

		if (inputX >= 0.2 && rb.velocity.y == 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
			animator.SetInteger("AnimState", 1);

		}
		else if (inputX <= -0.2 && rb.velocity.y == 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
			animator.SetInteger("AnimState", 1);
		}; 

		if (inputX >= -0.2 && inputX <= 0.2)
		{
			animator.SetInteger("AnimState", 0);
		}; 

		if (Input.GetButtonDown("Jump") && rb.velocity.y == 0) 
		{
			rb.AddForce(Vector2.up * jumpSpeed);
			animator.SetInteger("AnimState", 3);
		};

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Platform") && Input.GetAxis ("Vertical") > 0.2)
		{
			rb.Sleep();
			animator.SetInteger("AnimState", 5);
			Debug.Log ("working");
		}
	}
}
