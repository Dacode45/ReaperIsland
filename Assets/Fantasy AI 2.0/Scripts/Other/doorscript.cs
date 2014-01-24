using UnityEngine;
using System.Collections;

public class doorscript : MonoBehaviour {
	//player to target
	public Transform playertarget;
	//check if you want door locked
	public bool locked;
	//key id number that opens door
	public int keynumber;
	//distance player has to be to open door
	public float distancetoopen=8;
	//player distance
	private float playerdist;
	//animation for door opening/closing
	public AnimationClip dooropen;
	//enable ai if assigned
	public Transform aitoenable;
	
	//door active 
	private bool dooractive;
	//door is closed
	public bool doorclosed;
	//door is open
	public bool dooropened;
	//timerrr
	private float timer;
	
	// Use this for initialization
	void Start () {
		dooractive=true;
	doorclosed=true;
	}
	
	// Update is called once per frame
	void Update () {
	//check distance from player
		playerdist = Vector3.Distance(transform.position, playertarget.transform.position);
		
		//check if door is locked
		if(locked){
		}
		else{
			//open door if its unlocked
		if(playerdist<distancetoopen){
				if(dooractive){
				if(Input.GetKeyUp(KeyCode.F)){
                timer=0;
				if(doorclosed){
			if(dooropen){
								//enable ai if it exists
						if(aitoenable){
									AI ai=(AI)aitoenable.GetComponent("AI");
								orderai order=(orderai)aitoenable.GetComponent("orderai");
									
									if(ai) ai.enabled=true;
									if(order) order.enabled=true;
								}
								//play animation for door opening
							animation[dooropen.name].time = 0;
		animation[dooropen.name].speed = animation[dooropen.name].length / 0.8f;
			animation.Play( dooropen.name);	
			dooractive=false;
								
					}	
				}
						
						if(dooropened){
			if(dooropen){
								//reverse animation for door closing
		animation[dooropen.name].time = animation[dooropen.name].length;
		animation[dooropen.name].speed = animation[dooropen.name].length / -0.8f;
			animation.Play( dooropen.name);	
				dooractive=false;
							}	
					}	
				}
					}
				}
			
		}
		timer+=Time.deltaTime;
		
		//make sure door knows if its open or closed
		if(dooractive){}
		else{
			
			if(timer>1){
		if(doorclosed){
				dooropened=true;
					doorclosed=false;
					dooractive=true;
		}
			}
	if(timer>1.2f){
		if(dooropened){
				dooropened=false;
					doorclosed=true;
					dooractive=true;
		}		
			}
			}
		
		
	}
	void OnGUI(){
		if(playerdist<distancetoopen){
			if(locked){
				//message if door is locked
			GUI.Box(new Rect(200, 220, 130, 50), "The door is locked!");	
			}
			else{
				//message if door is unlocked
	GUI.Box(new Rect(200, 220, 130, 50), "Press F to open door");	
			}
		}
		
	}
}
