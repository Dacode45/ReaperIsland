/**
 * Combat:
Should be able to press Tab and target Enemy.
Should be able to press E and grab targeted or closest Enemy
Should be able to left click and swip to swing sword.
Should be able to right click and block, or right click and swipe to parry.
When targeting { Should be able to tap space to jump back, hold to charge jump, directional roll on tap, and lunge on charged jump.}

Health
Use AI health script.

Navigation,
Should be able to walk
Should be able to hold shift and run
Should be able to lunge to nearest obstacle on jump tap
Should be able to charge jump.
Should be able to swim.
Should be able to hold space to swim faster

Stats:
No methods, list of parameters that affect other scripts. Each property has two perameters, default and multiplier. Say walkspeed is a propertie. Its default is 1.5. Its multiplier is >1

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (RStats))]
public class RFirstPersonCharacter : FirstPersonCharacter
{

	//moving
	Vector3 desiredMove;
	Vector3 towards; //Vector with the direction you want to go based on Objeect in Context

	//Cover
	Transform cover;

	//Swimming
	public float swimSpeed = 4.0f;
	bool swimming = false;

	//Crouching
	private bool crouch = false;
	private float defaultCapsuleHeight;
	public float crouchSpeed = 2f;
	private float lastCrouchTime = 0;

	//Jumping
	private float yv; //Jump Height Vector
	private float jumpStrength = 0; // controls jump charging
	private float lastJumpTime = 0;
	public float defaultJumpStrength = 0.25f;
	bool jump = false;
	bool beginJump = false;
	bool endJump = false;
	//Grabing
	public float defaultGrabStrength = 0.25f; //Percentage of Weight that this object can grab
	bool grab = false;
	bool beginGrab = false;
	bool endGrab = false;

	public enum Action {Defualt = 0, Swimming = 1};
	public Action act = Action.Defualt;

	//Stats
	public RStats stats;

	//Context
	ContextSensor cSensor;
	Context oic; //Object in front of you
	

	public void Start(){
		swimSpeed = 4.0f;
		stats = GetComponent<RStats>();
		cSensor = GetComponentInChildren<ContextSensor>();
		defaultCapsuleHeight = capsule.height;
	}


	public override void FixedUpdate(){

		float h = CrossPlatformInput.GetAxis("Horizontal");
		float v = CrossPlatformInput.GetAxis("Vertical");
		if(jump != CrossPlatformInput.GetButton("Jump")){
			jump = CrossPlatformInput.GetButton("Jump");
			if(jump){
				beginJump = true;
				endJump = false;
			}else{
				beginJump = false;
				endJump = true;
			}

			lastJumpTime = 0;

		}else{
			if(!jump){
				beginJump = false;
				endJump = false;
			}else{
				beginJump = true;
			}
		}
		if(grab != CrossPlatformInput.GetButton("Grab")){
			grab = CrossPlatformInput.GetButton("Grab");
			if(grab){
				beginGrab = true;
				endGrab = false;
			}else{
				beginGrab = false;
				endGrab = true;
			}
			
			lastJumpTime = 0;
			
		}else{
			if(!grab){
				beginGrab = false;
				endGrab = false;
			}else{
				beginGrab = true;
			}
		}
		bool sprint = CrossPlatformInput.GetButton("Sprint");
		if(crouch != CrossPlatformInput.GetButton("Crouch")){
			lastCrouchTime = 0;
			crouch = CrossPlatformInput.GetButton("Crouch");
		}
		Vector2 input = new Vector2(h,v);
	

		if(cSensor[0] == null){ //null
			oic = Context.GetDefaultContext();
			towards = Vector3.zero;
		}else{
			oic = cSensor[0].GetComponent<Context>();
			towards = (cSensor[0].transform.position - transform.position);
		}
		switch(act){

		case Action.Defualt:

			GetComponent<RFirstPersonHeadBob>().headBobFrequency = 1.5f;
			float speed = runSpeed*stats.pacsModifier;

			//Running or Walking
			#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_WEB)
			bool walkOrRun = !sprint;
			speed = walkByDefault? (walkOrRun? runSpeed*stats.pacsModifier: walkSpeed) : (walkOrRun? walkSpeed: runSpeed*stats.pacsModifier);	
			#endif

			speed *= stats.movementModifier;
			//Crouching
			if(crouch && !jump){
				lastCrouchTime += Time.deltaTime * crouchSpeed;
				CrouchDown();
			}else{
				lastCrouchTime += Time.deltaTime * crouchSpeed;
				CrouchUp();
			}
		
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

			//Apply Jump
			
			yv = rigidbody.velocity.y;
			if(grounded && endJump){
				if(jumpStrength > .4){//Button Charged
					ChargedJump();
				}else{ //Button Tapped
					TappedJump();
				}
			}
			//Charging
			if(beginJump){
				lastJumpTime += Time.deltaTime*stats.pacsModifier;
				jumpStrength = Mathf.Lerp(defaultJumpStrength, 1, lastJumpTime);
			}else
				jumpStrength = 0;

			rigidbody.velocity = desiredMove + Vector3.up * yv;

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
			speed = swimSpeed *= stats.swimModifier;
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

		//Stuff to Do afterwards

	}

	void CrouchDown(){
		if(oic != null){
			switch( oic.onCrouch){

			case Context.Crouch.Default: 
				capsule.height = Mathf.Lerp(capsule.height, defaultCapsuleHeight/2, lastCrouchTime);
				break;
			}
		}else
			capsule.height = Mathf.Lerp(capsule.height, defaultCapsuleHeight, lastCrouchTime);

	}
	void CrouchUp(){
		if(oic != null){
			switch( oic.onCrouch){
				
			case Context.Crouch.Default: 
				capsule.height = Mathf.Lerp(capsule.height, defaultCapsuleHeight, lastCrouchTime);
				break;
			}
		}else
			capsule.height = Mathf.Lerp(capsule.height, defaultCapsuleHeight, lastCrouchTime);

	}

	void ChargedJump(){
		if(oic != null){
			switch( oic.onCrouch){
				
			case Context.Crouch.Default: 
				yv += jumpPower*jumpStrength*stats.jumpModifier;
				grounded = false;
				jumpStrength = 0;
				endJump = false;
				break;
			}
		}else
			yv += jumpPower*jumpStrength*stats.jumpModifier;
			grounded = false;
			jumpStrength = 0;
			endJump = false;
	}

	void TappedJump(){

		if(oic != null){
			switch( oic.onCrouch){
				
			case Context.Crouch.Default: 
				
				Debug.Log("Jumping Towards object");
				Debug.DrawLine(transform.position, oic.transform.position);
				yv += jumpPower*jumpStrength*stats.jumpModifier;
				desiredMove += towards*jumpPower*stats.jumpModifier;
				grounded = false;
				jumpStrength = 0;
				endJump = false;
				break;
			}
		}else
			
		yv += jumpPower*jumpStrength*stats.jumpModifier;
		//dodge.
		desiredMove *= 5*jumpPower*jumpStrength*stats.jumpModifier;
		grounded = false;
		jumpStrength = 0;
		endJump = false;


	}

	void OnGUI(){
		GUILayout.TextArea( 
		             "Jump Strength is " + jumpStrength 
		                   + "Acting on " + cSensor[0]
		);
	}
}
