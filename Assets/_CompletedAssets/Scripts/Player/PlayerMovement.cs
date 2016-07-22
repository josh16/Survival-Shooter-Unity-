using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;



public class PlayerMovement : MonoBehaviour
{

//Variables
	public float speed = 6f;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;




	void Awake()
	{
		//Setting up all the references on Awake
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();

	}


	void FixedUpdate() /* FixedUpdate runs with physics*/
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		/*Calling our functions in fixedUpates*/
		Move(h,v);
		Turning();
		Animating(h,v);

	}


	/*Character Movement Code Here*/
	void Move(float h, float v)
	{
		movement.Set( v, 0f, h);

		movement = movement.normalized * speed * Time.deltaTime; // Normalizing the movement so the speed never changes/

		playerRigidbody.MovePosition(transform.position + movement);// Moving the ridibody from it's current position with our movement.

	}


	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); /*Ray casting from camera through the screen towards the mousePosition*/

		RaycastHit floorHit; //Getting information back 


		/* Casting a Ray and getting info back from the floor, recieving length and making sure it hits anything on the "FloorMask"*/
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
			{
				Vector3 PlayerToMouse = floorHit.point - transform.position;
				PlayerToMouse.y = 0f;

				Quaternion newRotation = Quaternion.LookRotation(PlayerToMouse);
				playerRigidbody.MoveRotation (newRotation);
			}
	}


	void Animating(float h, float v)
	{
		bool walking = h !=0f ||  v !=0f;
		anim.SetBool ("IsWalking", walking);
	}



}
