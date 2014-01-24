using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gridscript : MonoBehaviour {
	public List<Transform> waysansens;
	public string waypointtag="Waypoint";
	public bool gatherdata;
	public bool comunicate;
	private int count;
	public bool mustbegrounded=true;
	public string groundtag="ground";

	
	// Use this for initialization
	void Start () {
	
	if(gatherdata){
AddAllways();
		}
	}
	public void AddAllways()
	{
		
		GameObject[] way = GameObject.FindGameObjectsWithTag(waypointtag);

		foreach(GameObject ways in way)
		
			Addtarget(ways.transform);
	//AddAllways();
		}
	
	public void Addtarget(Transform ways)
	{
	if(gatherdata){
	waysansens.Add(ways);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(comunicate){
		count=count+1;
			if(count>=1){
	int lsize=waysansens.Count;
			for (int i = 0; i < lsize; i++){
					if(waysansens[i]){
		
		Meshsensor sens=(Meshsensor)waysansens[i].GetComponent("Meshsensor");
			//if(way){
				
						//way.enabled=true;
			//}
			
			if(sens){
				if(mustbegrounded) sens.MustBeGrounded=true;
				else sens.MustBeGrounded=false;
				
				sens.groundtag=groundtag;
				sens.enabled=true;
			}
			
			comunicate=false;
		}
		}
		}
		}
	}
}
