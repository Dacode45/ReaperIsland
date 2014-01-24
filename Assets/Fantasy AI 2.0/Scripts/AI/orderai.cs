using UnityEngine;
using System.Collections;

public class orderai : MonoBehaviour {
	public bool isplayer;
	public Transform playercam;
	public Transform playerbod;
	
	//ORDERS
	public bool enableaiorder;
	public bool guardfront;
	public bool guardcrystal;
	public bool followme;
	public bool freeroam;
	public bool attackportal;
	
	//locations
	public bool changestates;
	public Transform front;
	public Transform crystal;
	public Transform player;
	public Transform portal;
	public bool dialogresponnd;
	
	// Use this for initialization
	void Start () {
		displayhealth addunit=(displayhealth)playercam.GetComponent("displayhealth");
						addunit.orderunits.Add(transform);
		
		freeroam=true;
	//enableaiorder=true;
	}
	
	// Update is called once per frame
	void Update () {
		//disable mouse and attack
		
		//MouseLook ml=(MouseLook)playercam.GetComponent("MouseLook");
			//MouseLook ml2=(MouseLook)playerbod.GetComponent("MouseLook");
			freefly att=(freefly)playerbod.GetComponent("freefly");
		if(enableaiorder){
		//ml.enabled=false;
			//ml2.enabled=false;
			att.attackenable=false;
		}
		else{
			//if(ml2){
		//ml.enabled=true;
			//ml2.enabled=true;
			//att.attackenable=true;	
			//}
		}
		
		
	AI ai=(AI)GetComponent("AI");
		if(ai.dead){}
		else{
			ai.respawntime=99999;
		}
		//ORDERS
		if(guardfront){
			ai.companion=true;
			ai.companionleader=front;
			 ai.Dialogrespond=true;
			guardfront=false;
		}
		
		if(guardcrystal){
			ai.companion=true;
			ai.companionleader=crystal;
			ai.Dialogrespond=true;
			guardcrystal=false;
		}
		
		if(followme){
			ai.companion=true;
			ai.companionleader=player;
			ai.Dialogrespond=true;
			followme=false;
		}
		
		if(freeroam){
			freeroam=false;
		}
		
		if(attackportal){
			ai.gototarget=true;
			ai.runtotarget=true;
			ai.walktotarget=false;
			ai.targetlocateto=portal;
			ai.Dialogrespond=true;
			attackportal=false;
			
		}
		
		
	}
	
	void OnGUI(){
		if(enableaiorder){
			
			
		//ORDER GUI'S
		GUI.Box(new Rect(200, 300, 330, 260), "What are your orders?");	
		
			if(GUI.Button(new Rect(210, 330, 250, 26), "1. Follow Me!")|Input.GetKeyUp(KeyCode.Alpha1)){
				AI ai=(AI)GetComponent("AI");
				ai.changestate=true;
				followme=true;
				enableaiorder=false;
			}
			
		if(GUI.Button(new Rect(210, 360, 250, 26), "2. Go to location!")|Input.GetKeyUp(KeyCode.Alpha2)){
				PointLocate locate=(PointLocate)playercam.GetComponent("PointLocate");
					locate.enabled=true;
				locate.orderunits.Clear();
				locate.orderunits.Add(transform);	
					
				enableaiorder=false;
			}
			
			
			if(GUI.Button(new Rect(210, 390, 250, 26), "3. Guard the front!")|Input.GetKeyUp(KeyCode.Alpha3)){
             AI ai=(AI)GetComponent("AI");
				ai.changestate=true;
				guardfront=true;
				enableaiorder=false;
			}
			
			if(GUI.Button(new Rect(210, 420, 250, 26), "4. Protect The Crystal!")|Input.GetKeyUp(KeyCode.Alpha4)){
				AI ai=(AI)GetComponent("AI");
				ai.changestate=true;
				guardcrystal=true;
				enableaiorder=false;
			}
			
			if(GUI.Button(new Rect(210, 450, 250, 26), "5. Attack At Will!")|Input.GetKeyUp(KeyCode.Alpha5)){
				AI ai=(AI)GetComponent("AI");
				ai.changestate=true;;
				freeroam=true;
				enableaiorder=false;
			}
			
			if(GUI.Button(new Rect(210, 480, 250, 26), "6. ATTACK THE SKELETON PORTAL!")|Input.GetKeyUp(KeyCode.Alpha5)){
				AI ai=(AI)GetComponent("AI");
				ai.changestate=true;
				attackportal=true;
				enableaiorder=false;
			}
		}	
	}
}
