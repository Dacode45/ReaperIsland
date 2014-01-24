using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shop : MonoBehaviour {
	//DISABLE THIS TO TAKE AWAY HELP TIP
	public bool enablehelpstart;
	private bool diactivecam;
	
	
	//framecounter
	private int framecount;
	//checks to see if pillars are destroyed
	public Transform woncheck;
	//YOU WON!
	public bool won;
	public Transform playercam;
	public Transform menucam;
	public Transform playerbod;
	//swords
	public Transform kingsword;
	public Transform crapsword;
	
	public bool menuactive;
	public List<Transform> guards;
	private int cg;
	public int maxunits=20;
	public Transform spawnspot;
	private GameObject newguard;
	private int listsize;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	framecount=framecount+1;
		
		if(menuactive){
			if(Input.GetKeyDown(KeyCode.Alpha1)){
			int listsize=guards.Count;
		
			if(cg>=maxunits){}
				else{
					displayhealth gold=(displayhealth)playercam.GetComponent("displayhealth");
					if(gold.gold>=250){
			newguard=(GameObject)Instantiate (guards[0].gameObject, spawnspot.transform.position, transform.rotation);
				
						AI ai=(AI)newguard.GetComponent("AI");
				if(ai){
							ai.DisableRespawn=false;
							ai.respawntime=0;
							ai.respawn=true;
							
						}
						
						listsize=ai.enemys.Count;
						for (int i = 0; i < listsize; i++){
						AI addenemy=(AI)ai.enemys[i].GetComponent("AI");
						if(addenemy) addenemy.enemys.Add(newguard.transform);	
						}
				ai.Dialogrespond=true;
						gold.gold=gold.gold-250;
				cg=cg+1;
					}
				}
	
			}
			if(Input.GetKeyDown(KeyCode.Alpha2)){
			int listsize=guards.Count;
		
			if(cg>=maxunits){}
				else{
					displayhealth gold=(displayhealth)playercam.GetComponent("displayhealth");
					if(gold.gold>=300){
			newguard=(GameObject)Instantiate (guards[1].gameObject, spawnspot.transform.position, transform.rotation);
				
						AI ai=(AI)newguard.GetComponent("AI");
						if(ai){
							ai.DisableRespawn=false;
							ai.respawntime=0;
							ai.respawn=true;
							
						}
						
						listsize=ai.enemys.Count;
						for (int i = 0; i < listsize; i++){
						AI addenemy=(AI)ai.enemys[i].GetComponent("AI");
						if(addenemy) addenemy.enemys.Add(newguard.transform);	
						}
				ai.Dialogrespond=true;
						gold.gold=gold.gold-250;
				cg=cg+1;
						
					}
				}
	
			}
			
			if(Input.GetKeyDown(KeyCode.Alpha3)){
				displayhealth potions=(displayhealth)playercam.GetComponent("displayhealth");
				
				if(potions.gold>=50){
					potions.healthpotions=potions.healthpotions+1;
					potions.gold=potions.gold-50;
					
				}
			}
			
		}
		
		
	}
	void OnGUI(){
		//get component of pillars
		physicsondeath won=(physicsondeath)woncheck.GetComponent("physicsondeath");
		//check player health
		health lost=(health)GetComponent("health");
		//if crystal is dead, then you lost
		if(lost.dead){
			//disable gameplay for player
			//MouseLook ml=(MouseLook)playercam.GetComponent("MouseLook");
			//MouseLook ml2=(MouseLook)playerbod.GetComponent("MouseLook");
			freefly att=(freefly)playerbod.GetComponent("freefly");
			//ml.enabled=false;
			//ml2.enabled=false;
			att.attackenable=false;
			if(GUI.Button(new Rect(338, 500, 100, 26), "Press Space")|Input.GetKeyDown(KeyCode.Space)){
				Application.Quit();
			}
			Time.timeScale=0;
		GUI.TextArea(new Rect(200, 300, 380, 230), "THE CRYSTAL HAS BEEN DESTROYED!  YOU LOST.. :(");	
			
		}
		
		//YOU WON!
		if(won.wongame){
			//disable gameplay
			//MouseLook ml=(MouseLook)playercam.GetComponent("MouseLook");
			//MouseLook ml2=(MouseLook)playerbod.GetComponent("MouseLook");
			freefly att=(freefly)playerbod.GetComponent("freefly");
			//ml.enabled=false;
			//ml2.enabled=false;
			att.attackenable=false;
			if(GUI.Button(new Rect(338, 500, 80, 26), "OK")){
				Application.Quit();
				
			}
			Time.timeScale=0;
		GUI.TextArea(new Rect(200, 300, 380, 230), "CONGRADULATIONS! YOU DESTROYED THE PORTAL!  YOU WON!");	
			
		}
		//wait a bit so pause works
		if(framecount>5){	
		if(enablehelpstart){
			
			
			//disable gameplay
			//MouseLook ml=(MouseLook)playercam.GetComponent("MouseLook");
		//	MouseLook ml2=(MouseLook)playerbod.GetComponent("MouseLook");
			freefly att=(freefly)playerbod.GetComponent("freefly");
			//ml.enabled=false;
			//ml2.enabled=false;
			att.attackenable=false;
			//pause
				Time.timeScale=0;
			
			
			if(GUI.Button(new Rect(338, 500, 80, 26), "OK")|Input.GetKeyDown(KeyCode.Return)){
				//if ok is pressed start game
				//ml.enabled=true;
			//ml2.enabled=true;
			att.attackenable=true;
				Time.timeScale=1;
				enablehelpstart=false;
				
			}
			
			//HELP TOOL TIP
				
				
			GUI.TextArea(new Rect(200, 300, 380, 230), "PROECT THE CRYSTAL LIFE SOURCE! Your mission is to protect the Crystal Life Source from the evil skeletons being summoned on the top of the cliff.  There are prisoners taken captive by the evil skeletons located around the map.  Free them and they are yours to command!  When you feel you are ready, attack and destroy the summoning portal pillars at the top of the cliff.. Destroy both pillars and you win!   When near the crystal and pointing at it, you may open up the shop, where you can buy new units, health potions, and weapons!  The blue units you out rank,  you may order them to do as you please!  Controls are W A S D to move around, and mouse to look around.  To attack click.  You gain gold for every skeleton killed.  Spend wisely, and command you units wisely!  Have fun!  ");
			if(GUI.Button(new Rect(338, 500, 100, 26), "Press Enter")){}
		}
		}
		
		
		//shopping menu
		if(menuactive){
			//MouseLook ml=(MouseLook)playercam.GetComponent("MouseLook");
			//MouseLook ml2=(MouseLook)playerbod.GetComponent("MouseLook");
			freefly att=(freefly)playerbod.GetComponent("freefly");
		//	ml.enabled=false;
			//ml2.enabled=false;
			att.attackenable=false;
		GUI.Label(new Rect(Screen.width * 0.5f - 50f, Screen.height * 0.5f - 10f, 600f, 20f), "Use number keys to select purchase");	
			//box for store
		GUI.Box(new Rect(200, 300, 330, 260), "Shop Shop Shop!");
			
			//BUY GUARD
		GUI.Button(new Rect(210, 330, 210, 26), "1. Buy Guard:  $250.00");
			
	
			
			//BUY ARCHER
			GUI.Button(new Rect(210, 360, 210, 26), "2. Buy Archer:  $300.00");

			//BUY HEALTH POTION
			GUI.Button(new Rect(210, 390, 210, 26), "3. Buy HealthPotion:  $50.00");
		
				//BUY KING SWORD
				if(GUI.Button(new Rect(210, 420, 210, 26), "4. Buy Ancient King Sword:  $500.00")|Input.GetKeyDown(KeyCode.Alpha4)){
				displayhealth sword=(displayhealth)playercam.GetComponent("displayhealth");
				if(sword.gold>=500){
				if(kingsword&crapsword){
					
				kingsword.gameObject.SetActiveRecursively(true);
				crapsword.gameObject.SetActiveRecursively(false);
					sword.gold=sword.gold-500;
					freefly dam=(freefly)playerbod.GetComponent("freefly");
					dam.damage=300;
				}
				}
			}
			//CLOSE STORE BOX
			if(GUI.Button(new Rect(445, 530, 70, 26), "5. Close")|Input.GetKeyDown(KeyCode.Alpha5)){
				//ml.enabled=true;
				//ml.enabled=true;
			   // ml2.enabled=true;
				att.attackenable=true;
				menuactive=false;
			}
			
		}
	}
}
