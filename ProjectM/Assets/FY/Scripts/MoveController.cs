using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	public float h;
	public float v;
	public float speed;

	private Rigidbody rb;

	Vector3 movement;

	int floorMask;
	float camRayLength =100f;


	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update (){
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw("Vertical");

		Move (h, v);
		Turning ();

	}

	void Move(float h,float v)
	{
		movement.Set (h, 0f, v);
		
		movement = movement.normalized * speed * Time.deltaTime;
		
		rb.MovePosition (transform.position + movement);
	}
	
	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		RaycastHit floorHit;
		
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			rb.MoveRotation(newRotation);
		}
	}
}
