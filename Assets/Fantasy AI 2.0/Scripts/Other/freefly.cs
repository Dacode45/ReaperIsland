using UnityEngine;
using System.Collections;

public class freefly : MonoBehaviour {
	//move speed for player
	public int movespeed=9;
	//the camera to sync with
	public Transform imitate;
	//the physical body of player
	public Transform playertarget;
	//animations
	public AnimationClip run;
	public AnimationClip idleattack;
	public AnimationClip death;
	public AnimationClip attackanim;
	//states
	public bool die;
	private bool deathanim=true;
	public bool charge;
	public bool attack;
	public bool idle;
	private float jumptime;
	private bool jump;
	private float timer;
	public bool enableattackdam=true;
	public int damage=35;
	//the 3 target positions to raycast to for attack range
	public Transform attackrange;
	public Transform attackrange2;
	public Transform attackrange3;
	public bool attackenable=true;
	private bool dead;
	private float respawntimer;
	private int displaytime;
	
	//get a refrence of what units are on ur team rather than making a new list
	public Transform unitlistrefrence;

	//audio
	public AudioClip swing;
	public AudioClip hitt;
	public AudioClip footsteps;
	
	
	
	// Use this for initialization
	void Start () {
	respawntimer=21;
		displaytime=20;
	}
	
	// Update is called once per frame
	void Update () {
		//get health
		health die=(health)GetComponent("health");
		//dead state
		if(die){
		if(die.dead){
			charge=false;
				idle=false;
				attack=false;
			dead=true;
			
			
				//gameObject.GetComponent("MouseTimeLook").enabled=false;
				
				
//stuff=gameObject.GetComponent("MouseTimeLook");
				
				
				
			if(death){
				
				if(deathanim){
				
		animation[death.name].speed = animation[death.name].length / 2f;
			animation.Play( death.name);
					deathanim=false;
					}
				
			}
			
		}
		else{
		
		RaycastHit hit = new RaycastHit();
		
		
		
				//run animation
		if(run){
				if(charge){
		animation[run.name].speed = animation[run.name].length / 0.8f;
			animation.CrossFade( run.name, 0.12f);	

				}
			}
		
	//idle animation
			if(idleattack){
				if(idle){
		animation[idleattack.name].speed = animation[idleattack.name].length / 2f;
			animation.CrossFade( idleattack.name, 0.25f);

				}
			}
		//attack animation
		if(attackanim){
				if(attack){
		animation[attackanim.name].speed = animation[attackanim.name].length / 1f;
			animation.CrossFade( attackanim.name, 0.15f);

				}
			}
			
		
		
		if(timer>1f){
				
				attack=false;
				timer=0;
			}
				//draw lines to attack range points
		if(playertarget&attackrange&attackrange2&attackrange3){
		Debug.DrawLine(playertarget.transform.position, attackrange.transform.position, Color.red);
		Debug.DrawLine(playertarget.transform.position, attackrange2.transform.position, Color.red);
		Debug.DrawLine(playertarget.transform.position, attackrange3.transform.position, Color.red);
			}
				//ATTACK
				if(attack){

		
			if(timer>0.5&timer<0.68){
			if(enableattackdam){
						if(playertarget&attackrange&attackrange2&attackrange3){
					if(Physics.Linecast(playertarget.transform.position, attackrange.transform.position, out hit)|Physics.Linecast(playertarget.transform.position, attackrange2.transform.position, out hit)|Physics.Linecast(playertarget.transform.position, attackrange3.transform.position, out hit)){
				
						if(hit.transform){
							audio.clip=hitt;
							audio.Play();
					 health dam=(health)hit.transform.GetComponent("health");
							if(dam){
								team team=(team)hit.transform.GetComponent("team");
								team pteam=(team)GetComponent("team");
								if(team.teamnumber==pteam.teamnumber){}
								else dam.currenthealth=dam.currenthealth-damage;
							}
								}
						}
							}
						enableattackdam=false;
		}
			}
			timer+=Time.deltaTime;
			if(timer<0.3f){
						//play audio swing
				audio.clip=swing;
				audio.Play();
			}
			
			idle=false;
			charge=false;
				
		}
		else{
			
		if(charge){
			idle=false;
		} else{
				idle=true;
			}
		}
			
		//control inputs
		if(Input.GetKey(KeyCode.W)){
		transform.position += transform.forward * movespeed * Time.deltaTime;
			
		}
		
		
		if(Input.GetKey(KeyCode.S)){
		transform.position += transform.forward * -movespeed * Time.deltaTime;	
			
		}
		
		
		if(Input.GetKey(KeyCode.D)){
		transform.position += transform.right * movespeed * Time.deltaTime;	
			
		}
		
		
		if(Input.GetKey(KeyCode.A)){
		transform.position += transform.right * -movespeed * Time.deltaTime;
			
		}
		
		jumptime-=Time.deltaTime;
		if(jumptime<=0)jumptime=0;
		
				//jump
		if(jumptime==0){
		if(Input.GetKey(KeyCode.Space)){
						
		rigidbody.AddForce(Vector3.up * 1900);
						jumptime=1;
		//transform.position += transform.up * +movespeed * Time.deltaTime;
		}
		}
		//charge=true if any of these buttons r being pressed
		if(Input.GetKey(KeyCode.A)|Input.GetKey(KeyCode.W)|Input.GetKey(KeyCode.S)|Input.GetKey(KeyCode.D)){
			
			if(attack){}
			else charge=true;
			
		}
		else charge=false;
		
	
		//attack input
		if(attackenable){
		if(Input.GetKeyUp(KeyCode.Mouse0)){
	  enableattackdam=true;
			attack=true;
		}
		}
		}
		if(dead){
			respawntimer-=Time.deltaTime;
				if(displaytime>respawntimer)displaytime=displaytime-1;
				if(respawntimer<=0){
					dead=false;
					
					displaytime=20;
					respawntimer=21;
				}
				
			}
			
		}
	}
	
	void OnGUI(){
		//message to the player if he is dead
		if(dead){
			
		GUI.Box(new Rect(200, 300, 190, 50), "You died..Respawn in.. "+displaytime);
		}
		
	//raycast for close units and shopping
		RaycastHit hit = new RaycastHit();
		if(playertarget&attackrange){
		if(Physics.Linecast(playertarget.transform.position, attackrange.transform.position, out hit)){
			shop shop=(shop)hit.transform.GetComponent("shop");
			 orderai ai=(orderai)hit.transform.GetComponent("orderai");
			
			if(ai){
				if(ai.enableaiorder){}
				else{
				GUI.Box(new Rect(200, 300, 150, 50), "Press E To Give Orders!");
					if(Input.GetKey(KeyCode.E)) ai.enableaiorder=true;
			}
			}
			
			if(shop){
				if(shop.menuactive){}
				else{
					GUI.Box(new Rect(200, 300, 150, 50), "Press E To Shop!");
					if(Input.GetKey(KeyCode.E)) shop.menuactive=true;
				}
				}
			}
		}
	}
}
