using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Navigation : MonoBehaviour {
	//the collision layer of character pathfinder
	public int charactercollisionlayer=9;
	//disables pathfinding.  used for objects that need to be targest but dont pathfind

	
	//waypoint tag
	
	public string waypointtag="Waypoint";
	public bool navmeshnavigation=true;
	public int CheckNewPathFramecount=12;
	private int checkcount;
	private Transform curway;

	
	//SMOOTH PATHFINDING
	public bool smoothpath=true;
	//THE MAX HEIGHT AI WILL SMOOTH PATH TO
	public float SmoothpathMaxStep=0.6f;
	//THE MAX DROP AI WILL SMOOTH PATH TO
	public float SmoothpathMaxDrop=5;
	private Transform waypointcheck;
	//WHEN THE SMOOTH PATH LOOKS FURTHER IN THE FUTURE WAYPOINTS
	private bool checkfurther;
	//TRANSFORM OF THE AI
	private Transform aitrans;
	//THE AMOUNT OF FRAMES BEFORE AI WILL DO A VISIBILITY CHECK DURING SMOOTH PATH
	public float visibilitycheckcounter=15;
	//VISIBILITY CHECK COUNTER
	private int checkvis;
	//WAYPOINT CHOICES FOR RETREAT
	public List<float> waychoices;


	//the goal target
	public Vector3 goal;
	private Transform goalobject;
	private Transform gobjectsave;
	//the goal waypoint(closest waypoint to target that can see target)
	public Transform goalwaypoint;
	//nextway point for ai to go to
	public Transform nextway;
	private Transform currentwaypoint;

	//makes grid green for mapped out path possibilities
	public bool showpath;


//CHECKS FOR THE NEXT WAYPOINT
	public bool checknextway;
	//PATH FINDING ENABLED FOR RETREAT
	public bool retreat;
	private bool faceaway;
	
	// Use this for initialization
	void Start () {
		//CHECK FURTHER FOR SMOOTH PATH ENABLED AT START
		checkfurther=true;

	
	}
	// Update is called once per frame
	void Update () {
		//check to see if there are any waypoints
		

		
				//GET THE AI SCRIPT
		AI ai=(AI)GetComponent("AI");
		
		
		//DRAW LINE TO NEXT WAYPOINT TARGET
		if(ai){
			if(nextway){
				if(aitrans){
						if(showpath) Debug.DrawLine(aitrans.position, ai.currenttarget, Color.green);
				}
			}
			
			
			if(ai.aieye)aitrans=ai.aieye;
			else aitrans=transform;
		}
		
		if(faceaway){
		
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - goalobject.position), 40 * Time.deltaTime);
		}
		//faceaway=true;	
		//CHECK IF TARGET HAS CHANGED.  IF IT HAS RECALCULATE PATH
		if(gobjectsave!=goalobject){
			nextway=null;
			checkcount=0;
			gobjectsave=goalobject;
		}
		
			//PATH FINDING IS DISABLED IF THESE BAHAVORS ARE ACTIVE
			if(navmeshnavigation){
		if(ai.attack|ai.idle|ai.dead|currentwaypoint==goalwaypoint){
				checknextway=false;
				nextway=null;
				faceaway=false;
			}
			else checknextway=true;
			if(ai.gototargetnostop){
				retreat=false;
			}
			else{
			if(ai.retreat){
					if(ai.findhelponretreat){}		
                 else retreat=true;
					
				}
			else retreat=false;
			}
			
				
			//PATH SETUP FOR GOTO TARGET BAHAVORS
			if(ai.gototarget|ai.gototargetnostop){
				
				if(ai.targetlocateto){

				Navmeshtarget target=(Navmeshtarget)ai.targetlocateto.GetComponent("Navmeshtarget");
				if(target){
						if(nextway==goalwaypoint|currentwaypoint==goalwaypoint){
							ai.currenttarget=ai.locationvector;
						}
					else{
							if(nextway)ai.currenttarget=nextway.transform.position;
						else ai.currenttarget=ai.locationvector;
						}
						
				goal=ai.locationvector;	
				goalobject=ai.targetlocateto;	
					if(target.currentwaypoint) goalwaypoint=target.currentwaypoint;
				}
				}
			}
		}
		//PATH SETUP FOR GOTO ENEMYS
			if(ai.gototarget|ai.gototargetnostop|ai.enemydead){}
		else{
			if(ai.enemy){
		
					
				Navmeshtarget target=(Navmeshtarget)ai.enemy.GetComponent("Navmeshtarget");
				if(target){
				goal=ai.enemy.transform.position;
				goalobject=ai.enemy;
					if(nextway==goalwaypoint|currentwaypoint==goalwaypoint){
						if(retreat){
							if(nextway){
								faceaway=false;
							}
							else faceaway=true;
							
						}
							else ai.currenttarget=ai.enemy.position;
						ai.currenttarget=ai.enemy.position;
					}
					else{
						if(nextway)ai.currenttarget=nextway.transform.position;
						else{
							if(ai.retreat)ai.currenttarget=-ai.enemy.position;
							else ai.currenttarget=ai.enemy.position;
						}
					}
					
					if(target.currentwaypoint) goalwaypoint=target.currentwaypoint;
				}
			}
			
			
			}
		
		

	}
		
	
	//PATH FINDING IS ACTIVE DURING COLLISION WITH A WAYPOINT
	void OnTriggerStay(Collider other){
		
				//CHECK IF COLLIDING WITH A WAYPOINT
		
	Waypoint wayray=(Waypoint)other.GetComponent("Waypoint");
					
	if(wayray){
				currentwaypoint=wayray.transform;
				if(checknextway){
		if(checkcount<1){		
	if(goalwaypoint){
					Waypoint goalway=(Waypoint)goalwaypoint.GetComponent("Waypoint");
				
				
			if(nextway){}
				
				//GET COMPONENT OF CONNECTING WAYPOINTS TO THE COLLIDING WAYPOINTS
				int listsize=wayray.connectingwaypoints.Count;
			for (int i = 0; i < listsize; i++){
				if(wayray.connectingwaypoints[i]){
				Waypoint connectways=(Waypoint)wayray.connectingwaypoints[i].GetComponent("Waypoint");
				if(retreat){
									if(wayray.transform==goalwaypoint.transform){
										faceaway=true;
									}
									else{
										
										faceaway=false;
			waychoices.Add(goalway.waysfromways[connectways.waypointid]);
						waychoices.Sort();
							if(goalway.waysfromways[connectways.waypointid]>=waychoices[waychoices.Count-1]){
									nextway=connectways.transform;		
										}
										else faceaway=false;
									}
										
								}				
									else{
								//SMOOTH PATHFINDING	
				if(smoothpath){
						
						//GET THE NEXT CLOSEST WAYPOINT TO START
						if(nextway){}
						else{
							if(goalway.waysfromways[connectways.waypointid]<goalway.waysfromways[wayray.waypointid]){
									nextway=connectways.transform;
								}
						checkfurther=false;
						
							}
							
					
							
					
						//RAYCASTING VARS
						RaycastHit hitt = new RaycastHit();
		              LayerMask layyy=9;
						if(nextway){
						
							Waypoint way2=(Waypoint)nextway.GetComponent("Waypoint");
							
							
							
							if(wayray.transform==nextway){
								if(goalway.waysfromways[connectways.waypointid]<goalway.waysfromways[wayray.waypointid]){
									nextway=connectways.transform;
								}
								checkfurther=true;
							
											
							}
										
							if(checkfurther){}
							else{
							
							
							
								
							if(goalway.waysfromways[way2.waypointid]>goalway.waysfromways[wayray.waypointid]){
								if(goalway.waysfromways[connectways.waypointid]<goalway.waysfromways[wayray.waypointid]){
									nextway=connectways.transform;
								}
							checkfurther=true;	
								
								}
							
							//CHECKS TO SEE IF HIS TARGET IS STILL ACCESSABLE, IN CASE SOMETHING CAUSE HIM TO CHANGE LOCATIONS
											if(checkvis>visibilitycheckcounter){
							if(Physics.Linecast(aitrans.position, nextway.transform.position, out hitt, layyy)){
								if(goalway.waysfromways[connectways.waypointid]<goalway.waysfromways[wayray.waypointid]){
									nextway=connectways.transform;
								}
								checkfurther=true;
								checkvis=0;
							}
								}
							}
							
							
							//get connecting ways of next waypoints
							if(checkfurther&nextway!=goalwaypoint){
				int listsize2=way2.connectingwaypoints.Count;
			for (int l = 0; l < listsize2; l++){
				Waypoint connectways2=(Waypoint)way2.connectingwaypoints[l].GetComponent("Waypoint");
								
							
								if(goalway.waysfromways[connectways2.waypointid]<goalway.waysfromways[way2.waypointid]){
											
											waypointcheck=connectways2.transform;
										}
							
							
							if(waypointcheck){
				
						//SMOOTH PATH.  CHECKS INTO THE FUTURE OF WAYPOINTS UNTIL A WAYPOINT IS INACCESSABLE							
							if(Physics.Linecast(aitrans.position, waypointcheck.transform.position, out hitt, layyy)|wayray.transform.position.y-waypointcheck.position.y>=SmoothpathMaxDrop|waypointcheck.position.y>=(wayray.transform.position.y+SmoothpathMaxStep)){
														checkfurther=false;

									}
									else{
								
															nextway=waypointcheck;
										checkfurther=true;
														
													
									}
									
							}
							}
										}
							
						}
					}
			else{	
					//NORMAL PATHFINDING.
					if(goalway.waysfromways[connectways.waypointid]<goalway.waysfromways[wayray.waypointid]){
									
									nextway=connectways.transform;
											waychoices.Clear();
								}	
					}
					}
						}
				}
				}
				}
			}
					}
		//FRAMECOUNTERS
		checkcount=checkcount+1;
		checkvis=checkvis+1;
		if(checkcount>CheckNewPathFramecount){
			
			checkcount=0;
			if(retreat){
			waychoices.Clear();
			}
		}
		
				}
	//RESET SOME COUNTERS AND CLEAR LISTS ON EXIT
	void OnTriggerExit(Collider other){
		
		Waypoint wayray=(Waypoint)other.GetComponent("Waypoint");
		if(wayray){
			if(retreat){
			waychoices.Clear();
			}
		
			
			checkcount=-3;
		}
		
	}
}
