using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	//list for dudes
	public List<Transform> dudestospawn;
	//the current round
	public int currentround;
	//if enabled the first round starts right away
	public bool firsttroundatstart;
	//the length of each round
	public float roundlength=20;
	public float roundtimer;
	//limit of total units
	public int dudelimit=40;
	//amount of units that come each round
	public int roundaddamount=2;
	//enable spawn
	public bool spawndudes;
	//time between dudes spawning so they dont all spawn at once
	private float spawndudetimer;
	//dude checklist
	public int dude;
	private bool diactivate=true;
	private int dudecounter;
	//private GameObject newdude;
	
	
	// Use this for initialization
	void Start () {
		
		
		if(firsttroundatstart)roundtimer=-1;
		else roundtimer=roundlength;
	}
	
	// Update is called once per frame
	void Update () {
		//start round timer countdown
		roundtimer-=Time.deltaTime;
		if(roundtimer<=0){
			currentround=currentround+1;
			spawndudes=true;
			roundtimer=roundlength;
		}
		
		//LET THE SPAWNING BEGIN!
	if(spawndudes){
			spawndudetimer+=Time.deltaTime;
			if(spawndudetimer>=0.2f){
					if(dude<dudelimit){
			Instantiate (dudestospawn[0].gameObject, transform.position, transform.rotation);
			

							
				dude=dude+1;
				dudecounter=dudecounter+1;
					spawndudetimer=0;
					}
				}
			}
				if(dudecounter>=roundaddamount){
				dudecounter=0;
				spawndudes=false;
			
			}
	
			
		//deactivate ai's
		if(diactivate){
		int listsize=dudestospawn.Count;
		for (int i = 0; i < listsize; i++){
			
			dudestospawn[i].transform.gameObject.SetActiveRecursively(false);	
			
				
				diactivate=false;
			}
		}
		}
		//}
		

}
