using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class patrolpath : MonoBehaviour {
	public bool enablepatrol;
	public bool patrolwalk;
	public bool patrolrun;
	public List<Transform> Patrolpoints;
	private int currentarg;
	private bool countup;
	private bool countdown;
	private bool count;
	public bool checkpatrol;

	
	// Use this for initialization
	void Start () {
		if(enablepatrol){
			AI ai=(AI)GetComponent("AI");
		ai.gototarget=true;
			if(patrolwalk){
				ai.walktotarget=true;	
				ai.runtotarget=false;
			}
		if(patrolrun){
				ai.walktotarget=false;
				ai.runtotarget=true;
			}
			checkpatrol=true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if(count){
			if(currentarg==0){
				countdown=false;
				countup=true;
			}
			
			if(currentarg==Patrolpoints.Count-1){
				countup=false;
				countdown=true;
			}
			
			
			if(countup){
				currentarg=currentarg+1;
			}
			
			if(countdown){
				currentarg=currentarg-1;
			}
			count=false;
		}
		
		
		if(enablepatrol){
			if(checkpatrol){
		AI ai=(AI)GetComponent("AI");

		
		ai.targetlocateto=Patrolpoints[currentarg];
			ai.targetdistancestop=0;
		
		checkpatrol=false;
			}	
		}
		
	
	}
	void OnTriggerStay(Collider other){
		
		if(other.transform){
			if(enablepatrol&Patrolpoints.Count>0){
			
			if(other.transform==Patrolpoints[currentarg]){
				checkpatrol=true;
count=true;
				}	
			}
			
			
		}
		
	}
}
