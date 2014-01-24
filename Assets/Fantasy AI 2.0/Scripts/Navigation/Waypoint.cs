using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Waypoint : MonoBehaviour {
	
	private GameObject me;
	//BAKED IS CHECKED OFF IF THE WAYPOINT HAS BEEN BAKED ALREADY
	public bool baked;
	//WILL CLEAR BAKED DATA AND BAKE
	public bool clearbakeddata;
	
	public bool collisionconnect;
	
	//CHECKS FOR BAKE ERRORS
	private bool checkerrors;
	public int waypointid;
	public string waypointtag="Waypoint";
	public bool makelevelwithground;
	
	
	
	//COUNT CHECK
	private int waycount;
	private int cww;
	//ADDS TO LIST
	private bool addone;
	
	
	//WAYPOINTS CONNECTED
	public Transform way1;
	public Transform way2;
	public Transform way3;
	public Transform way4;
	//ID SCANNING
	private bool idscan=true;
	public bool idassignscan=true;
	private int listcheck;
	private bool donecurrent;
	private int donefcounter;
	
	private int counterr;

	
	//disable after scan is finished(speeds up framerate a bit but wont show connections for debugging)
	public bool disableafterscan=false;
	
	//just the object that represents the waypoint
	public Transform waypointrepresentation;
	
	public bool showconnections;
	
	//ray distance
	public float raylength=4;
	public bool autoraylength;
	public bool staircase;
	public bool hidewaypoint=true;
	
	//scanways
	public bool scanways;
	public bool scanways2;
	
	public int cw;
	public int connectionamount;
	
	//lists.  The nextways and waysfrom player are matched up with the id number of the targest in target list
	public List<Transform> waypoints;
	public List<float> waysfromways;
	public List<Transform> connectingwaypoints;
	public List<int> connectingwaysid;
	public List<Transform> connectingwaypointschecklist;
	public int connectwayscount;
	public bool addtargswithtag;

	//character tag
	public string addtargetswithtag="character";

	
	//transform
	public Transform mytransform;
	
	//check list
	public int wcount;
	private float percent;
	private float percentdone;
	private float percentcheck;
	private bool missinglinks;
	private float percentdisplay;
	private int precount;

	
	//DESTROY THE WAYPOINT IF TOUCHING SOMETHING(USEFUL WHEN MAKING GRID NAVIGATION)
	public bool destroyoncollision;
	private bool makerigidbody;
	private int obtimer;
	private int listscan;
	public int clistsize;

	public bool finish;
	
	// Use this for initialization
	void Start () {
		if(collisionconnect&clearbakeddata){
		transform.localScale=transform.localScale*1.13f;
			
			GameObject me=gameObject;
		me.AddComponent<Rigidbody>();
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;	
		}
		
		
		collider.enabled=true;
		//AUTO RAYLENGTH
		if(autoraylength){
			raylength=(transform.lossyScale.x)*1.2f;
			
		}
		
		//HIDE WAYPOINT 
		if(hidewaypoint){
		renderer.enabled=false;
		if(waypointrepresentation) waypointrepresentation.transform.gameObject.SetActiveRecursively(false);
		}
		
		if(baked){}
		else{
		wcount=1;
		
		me=gameObject;
		
		if(clearbakeddata){
			waypoints.Clear();
			waysfromways.Clear();
				connectingwaypoints.Clear();
			way1=null; way2=null; way3=null; way4=null;
		}

		
	if(destroyoncollision)makerigidbody=true;
		
		mytransform=transform;
		cw=1;
	scanways=true;
		idassignscan=true;
	scanways2=true;	
		idscan=true;
		addone=true;

		if(baked){
			idscan=false;
			addone=false;
			
		}
			else{
			
		waysfromways.Add(99999);
		}
		}
AddAllways();
	}
	public void AddAllways()
	{
		if(baked){}
		else{
		GameObject[] way = GameObject.FindGameObjectsWithTag(waypointtag);

		foreach(GameObject ways in way)
			Addtarget(ways.transform);
	//AddAllways();
		}
	}
	public void Addtarget(Transform ways)
	{
		
		
		if(baked){}
		else waypoints.Add(ways);

	}
	
	// Update is called once per frame
	void Update () {
		if(waypointid==1){
		if(baked){}
			else{
			if(missinglinks){}
			else{
				counterr=counterr+1;
			
			if(counterr==2){
				percentcheck=percent;
			}
			if(counterr>5){
				if(percentcheck==percent)missinglinks=true;
				counterr=0;
					
				}
				
			}

			
				
				
			if(percent<=100){
			if(missinglinks){
			percent=percent+1;
			if(percentdisplay<percent-10){percentdisplay=percentdisplay+10; Debug.Log("Waypoints are missing links..Continuing Bake.."+percentdisplay+"%");}
			else if(percentdisplay<percent-5){percentdisplay=percentdisplay+5; Debug.Log("Waypoints are missing links..Continuing Bake.."+percentdisplay+"%");}
			else if(percentdisplay<percent){percentdisplay=percentdisplay+1; Debug.Log("Waypoints are missing links..Continuing Bake.."+percentdisplay+"%");}	
				
			}
			else{
		//BAKE FINISHED
			if(percentdisplay<percent-20){percentdisplay=percentdisplay+20; Debug.Log("Baking Waypoints "+percentdisplay+"%");}
			else if(percentdisplay<percent-10){percentdisplay=percentdisplay+10; Debug.Log("Baking Waypoints "+percentdisplay+"%");}
			else if(percentdisplay<percent-5){percentdisplay=percentdisplay+5; Debug.Log("Baking Waypoints "+percentdisplay+"%");}
			else if(percentdisplay<percent){percentdisplay=percentdisplay+1; Debug.Log("Baking Waypoints "+percentdisplay+"%");}
			}
		}
		}
		
		
		if(percentdisplay>=100){
				counterr=0;
				if(donefcounter<=1)Debug.Log("Finalizing...");
				donefcounter=donefcounter+1;	
				}
		
			if(donefcounter>8){
			finish=true;
			percent=0;
			percentdisplay=0;
			donefcounter=0;
		}
		
				if(finish){
			
				int listsize=waypoints.Count;
			for (int i = 0; i < listsize; i++){
				//GET COMPONENT OF ALL WAYPOINTS	
						if(i!=1){
			Waypoint way=(Waypoint)waypoints[i].GetComponent("Waypoint");	
			way.wcount=waysfromways.Count-1;										
			way.checkerrors=true;
			way.clearbakeddata=false;
			way.idassignscan=false;
			way.baked=true;	
					clearbakeddata=false;
					baked=true;
		finish=false;
										}
														}	
				}
		
		}
		
		
		connectwayscount=connectingwaypoints.Count;
		
		//find other ways
		if(showconnections){
			int l=connectingwaypoints.Count;
			for (int k = 0; k < l; k++){
				
			
			
			if(connectingwaypoints[k])Debug.DrawLine(transform.position, connectingwaypoints[k].transform.position, Color.black);
			}
		}
		
		if(baked){}
			else{
			
		//shows the connections between waypoints 
		if(makerigidbody){
			//GameObject myGameObject = new GameObject("Test Object");
			me.AddComponent<Rigidbody>();
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
	
			makerigidbody=false;
		}
		else{
			obtimer=obtimer+1;
				if(obtimer>5){
			
					scanways=false;
			scanways2=false;
				if(rigidbody)Destroy(rigidbody);
			}
			
		}
		
				if(idscan){
				if(waypoints[0].transform==transform) Debug.Log("Connecting Waypoints..."); 
					
				}
	

				
				
		var fwd = transform.TransformDirection (Vector3.forward);
		var right = transform.TransformDirection (Vector3.right);
		
		RaycastHit hit = new RaycastHit();
				
				
				
		if(scanways){
		//scan for a max of 4 waypoints in 4 diff directions
			
				if(collisionconnect){
				}
				else{
			//RAYCAST FOREWARD
		if (Physics.Raycast (transform.position, fwd, out hit, raylength)) {
				if(hit.transform.gameObject.tag == "Waypoint"&hit.transform!=transform){
			Debug.DrawLine(transform.position, hit.point, Color.red);
			//ADD CONNECTNG WAYPOINTS FROM THE BOTTOM OF LIST TO TOP
							
						
						//add connecting way point to list
							Waypoint way=(Waypoint)hit.transform.GetComponent("Waypoint");
							
						     way.connectingwaypoints.Add(transform);
							connectingwaypoints.Add(way.transform);
							
							connectingwaypointschecklist.Add(transform);
							
				}
				}
			//RAYCAST BACKWARDS
		if (Physics.Raycast (transform.position, -fwd, out hit, raylength)) {
				if(hit.transform.gameObject.tag == "Waypoint"&hit.transform!=transform){
			Debug.DrawLine(transform.position, hit.point, Color.red);
							
						//add connecting way point to list
							Waypoint way=(Waypoint)hit.transform.GetComponent("Waypoint");
							way.connectingwaypoints.Add(transform);
							connectingwaypoints.Add(way.transform);
							
							connectingwaypointschecklist.Add(transform);
			
							
				
				}
				}
			//RAYCAST RIGHT
		if (Physics.Raycast (transform.position, right, out hit, raylength)) {
				if(hit.transform.gameObject.tag == "Waypoint"&hit.transform!=transform){
			Debug.DrawLine(transform.position, hit.point, Color.red);
							
							//add connecting way point to list
							Waypoint way=(Waypoint)hit.transform.GetComponent("Waypoint");
							way.connectingwaypoints.Add(transform);
							connectingwaypoints.Add(way.transform);
							
							connectingwaypointschecklist.Add(transform);

				}
			}
			//RAYCAST LEFT
			if (Physics.Raycast (transform.position, -right, out hit, raylength)) {
				if(hit.transform.gameObject.tag == "Waypoint"&hit.transform!=transform){
			Debug.DrawLine(transform.position, hit.point, Color.red);
							
							//add connecting way point to list
							Waypoint way=(Waypoint)hit.transform.GetComponent("Waypoint");
							way.connectingwaypoints.Add(transform);
							connectingwaypoints.Add(way.transform);
							
							connectingwaypointschecklist.Add(transform);
	
				}
				}
				}	
						
		scanways=false;
			scanways2=true;
					
		}
		}
		if(scanways2) {
			//Destroy(rigidbody);
			var down = transform.TransformDirection (Vector3.up);
			RaycastHit ground = new RaycastHit();
		if(makelevelwithground){
		if (Physics.Raycast (transform.position, -down, out ground, 999999)) {
			transform.position=ground.point;		
					
				}
			}

			
			
		if(connectingwaypoints.Count==0){
			int listsize=waypoints.Count;	
			for (int i = 0; i < listsize; i++){
				
			Waypoint way=(Waypoint)waypoints[i].GetComponent("Waypoint");
			if(way.waypoints[i].transform==transform){}
					else{
						way.waypoints.Remove(transform);
						Debug.Log("Removing singled out waypoints...");
						way.waysfromways.Remove(99999);
					
					}
						
				Destroy(gameObject);
			}
			}
		transform.localScale=transform.localScale*.87f;	
			
			//if(connectingwaypoints.Count==0)Destroy(gameObject);
		scanways2=false;	
					
		
		}
		
		
	
			//CHECK IF WAYPOINT COUNT IS GREATER THAN 0
		
		
		
		if(waypoints.Count>0){
			
			if(idassignscan){
				
				
			int listsize=waypoints.Count;
				
			for (int i = 0; i < listsize; i++){
						//GET COMPONENT OF ALL WAYPOINTS	
					Waypoint way=(Waypoint)waypoints[i].GetComponent("Waypoint");
							
		           if(addone){
					way.waysfromways.Add(99999);
				}
				else{
					if(idscan){
						
						
					}
					else{
									
					if(waysfromways.Count>2){
								
							waysfromways[waypointid]=1;
								
						//CALCULATE HOW MANY WAYPOINTS AWAY EVERY OTHER WAYPOINT IS FROM ITSELF
									if(connectingwaypoints.Count>0){
									
									if(way.waysfromways[waypointid]<9999){	
										
			                      for (int c = 0; c < way.connectwayscount; c++){
												
													
											if(way.connectingwaypoints[c]){
										Waypoint wayy1=(Waypoint)way.connectingwaypoints[c].GetComponent("Waypoint");
													
											
													
									if(way.waysfromways[waypointid]<wayy1.waysfromways[waypointid]){
													//float distance = Vector3.Distance (wayy1.waypoints[waypointid-1].transform.position, way.waypoints[waypointid-1].transform.position);		
													wayy1.waysfromways[waypointid]=way.waysfromways[waypointid]+1;
													//wayy1.waysfromways[waypointid]=way.waysfromways[waypointid]+distance;
													//waysfromways[wayy1.waypointid]=waysfromways[wayy1.waypointid];	
														
													
										}
												}
										}
										}
										
										
								
									
									
										//LETS FIGURE OUT HOW FAR ALONG THE BAKE IS AND WHEN ITS DONE
				precount=precount+1;
											
			if(waypointid==1){
													
					//DISPLAY THE PERCENT OF BAKE
								
					int l=waysfromways.Count;
				
						
			for (int j = 0; j < l; j++){
								if(waysfromways[j]!=99999){
										
											percentdone=percentdone+1;
											
										}
										}
									
									
									
									if(missinglinks){}
												else{
											percent=percentdone/(waysfromways.Count-1)*(100);
percentdone=0;
										}
												}
								
											}
													
						 
									
										
										
								
		
														
						}
						if(waycount==waypoints.Count){
			idscan=false;
			
			waycount=0;	
									}
						}
					
		}
				}
			
			}
			
				
				//ID SCAN
		if(idscan){
		int lsize=waypoints.Count;
				
			for (int i = 0; i < lsize; i++){	
					if(waypoints[i].transform==transform)waypointid=i+1;
					idscan=false;	
					}
		}
				
				

		
		addone=false;
		waycount=waycount+1;
		}
		
		if(checkerrors){
				//BAKE FINISHED AND SUCCESSFUL
			Debug.Log("Bake Successful! To keep baked data, select all waypoints in scene without stopping gameplay, copy them, stop gameplay, delete old waypoints in edtor, then paste the new ones copied.");
			int errorlist=waysfromways.Count;
			for (int i = 0; i < errorlist; i++){
				if(i==0){}
				else{
					
				if(waysfromways[i]>9998){
							//MISSING LINKS ERROR MESSAGE
				Debug.Log("Bake Complete..  Waypoints are not all linked. This may have undesired affects in Navigation.");	
				}
				
					}
					checkerrors=false;

			}
			}
			
			
		int connectsize=connectingwaypoints.Count;
				for (int f = 0; f < connectsize; f++){
			if(connectsize==connectingwaypoints.Count){
		if(connectingwaypoints[f]=connectingwaypoints[f]){
					
					
					
					}
					}
			}
				
			
		
		//if(disableafterscan)enabled=false;
	}
	//FOR DESTROYING WAYPOINTS ON COLLISION
	void OnTriggerEnter(Collider other){
	if(collisionconnect){
			Waypoint way=(Waypoint)other.transform.GetComponent("Waypoint");
				if(way){
				connectingwaypoints.Add(way.transform);
				//checkcollision=false;	
				}

				}
		
		}

	
}
