using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
	
	public Transform playertarget;
	//assign for door to unlock
	public Transform doortounlock;
	//door unlocked
	public bool doorunlocked;
	//the distance player has to be to unlock door
	public float distancetopickup=5;
	//player distance
	private float playerdist;
	//key picked up
	public bool pickedup;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if F key is pressed open door
		if(Input.GetKeyUp(KeyCode.F)&playerdist<distancetopickup){
			pickedup=true;
		}
		//if key is picked up unlock door
		if(pickedup){
			doorscript door=(doorscript)doortounlock.GetComponent("doorscript");
			if(door){
				door.locked=false;
			}
			gameObject.active=false;
		}
		
		//get player distance
		playerdist = Vector3.Distance(transform.position, playertarget.transform.position);
	
	}
	void OnGUI(){
		//pick up key message
		if(pickedup){}
		else{
		if(playerdist<distancetopickup){
			GUI.Box(new Rect(200, 300, 150, 50), "Press F to pick up key!");
			
		}
		}
	}
	
}
