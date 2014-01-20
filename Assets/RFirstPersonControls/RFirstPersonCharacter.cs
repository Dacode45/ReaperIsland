using UnityEngine;


public class RFirstPersonCharacter : FirstPersonCharacter
{
	public float swimSpeed = 4.0f;
	bool swimming = false;

	public enum Action {Defualt, Swimming};
	public Action act = Action.Defualt;

	public void Start(){
		swimSpeed = 4.0f;
	}

	public override void FixedUpdate(){

		float h = CrossPlatformInput.GetAxis("Horizontal");
		float v = CrossPlatformInput.GetAxis("Vertical");
		bool jump = CrossPlatformInput.GetButton("Jump");

		Vector2 input = new Vector2(h,v);
		Vector3 desiredMove;

		switch(act){

		case Action.Defualt:

			GetComponent<RFirstPersonHeadBob>().headBobFrequency = 1.5f;
			float speed = runSpeed;

			#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_WEB)
			bool walkOrRun = Input.GetKey(KeyCode.LeftShift);
			speed = walkByDefault? (walkOrRun? runSpeed: walkSpeed) : (walkOrRun? walkSpeed: runSpeed);	
			#endif

			Ray ray = new Ray(transform.position, -Vector3.up);

			RaycastHit[] hit = Physics.RaycastAll(ray, capsule.height * jumpRayLength);

			float nearest = Mathf.Infinity;

			//Check if Grounded

			if(grounded || rigidbody.velocity.y < .01){
	
				grounded = false;

				foreach(RaycastHit H in hit){

					if(!H.collider.isTrigger && H.distance < nearest){
						grounded = true;
						nearest = H.distance;
					}
				}
			}

			Debug.DrawRay(ray.origin, ray.direction*capsule.height * jumpRayLength, grounded? Color.green : Color.red);

			//Input
			if(input.sqrMagnitude > 1) input.Normalize();

			 desiredMove = transform.forward * input.y * speed + transform.right * input.x*speed;

			float yv = rigidbody.velocity.y;

			if(grounded && jump){
				yv += jumpPower;
				grounded = false;
			}

			rigidbody.velocity = desiredMove + Vector3.up*yv;

			//Use high or low friction depending on ground we are in
			if(desiredMove.sqrMagnitude > 0 || !grounded){
				collider.material = advanced.zeroFrictionMaterial;
			}else
				collider.material = advanced.highFrictionMaterial;

			rigidbody.AddForce(Physics.gravity * (advanced.gravityMultiplier-1),ForceMode.Acceleration);

			//Camera LOOKat


			break;



		case Action.Swimming:

			GetComponent<RFirstPersonHeadBob>().headBobFrequency = 15;
			grounded = false;
			speed = swimSpeed;
			swimming = true;
			Camera c = GetComponentInChildren<Camera>();
			Debug.DrawRay(c.transform.position,c.transform.forward, Color.blue);

			desiredMove = c.transform.forward * input.y * speed + c.transform.right * input.x*speed;
			if(jump){
				desiredMove *= jumpPower/2;
			}

			rigidbody.velocity = desiredMove;
			rigidbody.AddForce(Physics.gravity * (-.9f),ForceMode.Acceleration);

		break;
		}

	}
}
