using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class displayhealth : MonoBehaviour {
	//GET STATS 
	public Transform health;
	public Transform myhealth;
	public Transform unitinfo;
	public Transform enemylist;
	//GOLD AND HEALTH POTIONS
	public int gold;
	public int healthpotions=4;
	public float hpp;
	public float myhp;
	//STARTING GOLD
	public int startinggold=100;
	private bool charge;
	private bool enableheal;
	private bool heal;
	private float chargtime;
	public List<Transform> orderunits;
	//order all units
	private bool orderenable;
	private bool onme;
	private bool guardfront;
	private bool crystal;
	private bool free;
	private bool portal;
	private bool orderallunits;
	public bool enablemenu=true;
	
	//ai take over
	public bool playerplaying;
	public bool aiplaying;
	
	// Use this for initialization
	void Start () {
		PointLocate locate=(PointLocate)GetComponent("PointLocate");
			locate.enabled=false;
		
		playerplaying=true;
		enableheal=true;
	gold=startinggold;
	}
	
	// Update is called once per frame
	void Update () {
		
		//PLAYER PLAYING
	if(playerplaying){
			//AI ai=(AI)myhealth.transform.GetComponent("AI");
			freefly control=(freefly)myhealth.transform.GetComponent("freefly");
			//MouseLook mouse=(MouseLook)myhealth.transform.GetComponent("MouseLook");
			//ai.enabled=false;
			control.enabled=true;
			//mouse.enabled=true;
			
		}
		else{
			//AI PLAYING FOR PLAYER
		if(aiplaying){
			AI ai=(AI)myhealth.transform.GetComponent("AI");
			freefly control=(freefly)myhealth.transform.GetComponent("freefly");
			//MouseLook mouse=(MouseLook)myhealth.transform.GetComponent("MouseLook");
			ai.enabled=true;
			control.enabled=false;
			//mouse.enabled=false;
			
		}
		}
	
		if(charge){
			chargtime+=Time.deltaTime;
			if(chargtime>1){
				enableheal=true;
				chargtime=0;
			}
		}
		//HEAL WITH HEALTH POTION
		if(heal | Input.GetKey(KeyCode.H)){
			
			if(enableheal&healthpotions>0){
				health hp=(health)myhealth.transform.GetComponent("health");
				if(hp.currenthealth==hp.maxhealth){}
				else{
				hp.currenthealth=hp.currenthealth+100;
				charge=true;
					healthpotions=healthpotions-1;
				enableheal=false;
				}
			}
		}
		

		
	}
	
	void OnGUI(){
		//ORDER ALL UNITS
		if(enablemenu){
		if(orderallunits){}
		else{
		if(GUI.Button(new Rect(0, 200, 202, 26), "Order All Units Menu(0)")|Input.GetKeyUp(KeyCode.Alpha0)){
				orderallunits=true;
			}
		}
		
		if(orderallunits){
		if(GUI.Button(new Rect(0, 200, 222, 26), "All Units On Me!(1)")|Input.GetKeyUp(KeyCode.Alpha1)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){
			orderai orunit=(orderai)orderunits[i].GetComponent("orderai");
			AI ai=(AI)orderunits[i].GetComponent("AI");
				ai.changestate=true;
				orunit.followme=true;
			orderallunits=false;
				
			}
			}
			
			if(orderallunits){
		if(GUI.Button(new Rect(0, 230, 222, 26), "Go to Location!(2)")|Input.GetKeyUp(KeyCode.Alpha2)){
			
			PointLocate locate=(PointLocate)GetComponent("PointLocate");
					locate.orderunits.Clear();
				int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){	
				
				locate.orderunits.Add(orderunits[i]);	
					}
			locate.enabled=true;
				orderallunits=false;
			}
			}
			
			
		if(GUI.Button(new Rect(0, 260, 222, 26), "All Units Guard The Front!(3)")|Input.GetKeyUp(KeyCode.Alpha3)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){
			orderai orunit=(orderai)orderunits[i].GetComponent("orderai");
			AI ai=(AI)orderunits[i].GetComponent("AI");
				ai.changestate=true;
				orunit.guardfront=true;
				orderallunits=false;
				
			}

			}
		if(GUI.Button(new Rect(0, 290, 222, 26), "All Units Protect The Crystal!(4)")|Input.GetKeyUp(KeyCode.Alpha4)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){
			orderai orunit=(orderai)orderunits[i].GetComponent("orderai");
			AI ai=(AI)orderunits[i].GetComponent("AI");
				ai.changestate=true;
					orunit.guardcrystal=true;
				orderallunits=false;
			}

			}
		if(GUI.Button(new Rect(0, 320, 222, 26), "All Units Attack At Will!(5)")|Input.GetKeyUp(KeyCode.Alpha5)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){
			orderai orunit=(orderai)orderunits[i].GetComponent("orderai");
			AI ai=(AI)orderunits[i].GetComponent("AI");
				ai.changestate=true;
					orunit.freeroam=true;
				orderallunits=false;
			}

			}
		if(GUI.Button(new Rect(0, 350, 222, 26), "All Units ATTACK THE PORTAL!(6)")|Input.GetKeyUp(KeyCode.Alpha6)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){
			orderai orunit=(orderai)orderunits[i].GetComponent("orderai");
			AI ai=(AI)orderunits[i].GetComponent("AI");
				ai.changestate=true;
				orunit.attackportal=true;
		orderallunits=false;
			}
				
			}
		}
		
			//DISPLAY HEALTHS AND OTHER STATS
	health hp=(health)health.transform.GetComponent("health");
		health hp2=(health)myhealth.transform.GetComponent("health");
		if(hp){
		hpp=hp.currenthealth;
		if(GUI.Button(new Rect(0, 30, 300, 26), "Crystal Life Source.  Health: "+hpp)){
			}
			Spawner unit=(Spawner)unitinfo.transform.GetComponent("Spawner");
			AI enemys=(AI)enemylist.transform.GetComponent("AI");
			if(GUI.Button(new Rect(0, 90, 170, 26), "Active AI skeletons: "+unit.dude)){
			
			}
			if(GUI.Button(new Rect(0, 60, 300, 26), "Your Health: "+hp2.currenthealth)){
			}
			
			int listsize=enemys.enemys.Count;
			for (int i = 0; i < listsize; i++){
			health ai=(health)enemys.enemys[i].transform.GetComponent("health");
			if(ai.dead&ai.givereward){
				gold=ai.goldtogive+gold;
			     ai.givereward=false;
				}
					
					}
			if(GUI.Button(new Rect(0, 120, 130, 26), "Gold: "+gold)){
			
			}
			
			if(GUI.Button(new Rect(0, 150, 130, 26), "HealthPotions(H) "+healthpotions)){
			heal=true;
			}
			else heal=false;
			//quit
			if(GUI.Button(new Rect(0, 0, 65, 26), "Exit(esc)")|Input.GetKeyUp(KeyCode.Escape)){
		Application.Quit();
			
			}
			}
		
		}
		
	}
}
