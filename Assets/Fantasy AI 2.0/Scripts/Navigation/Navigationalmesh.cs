using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Navigationalmesh : MonoBehaviour {
	
	public bool BuildNavigationalMesh=true;
	public bool CollisionConnection;
	public bool RaycastConnection;
	public bool BakeWaypoints;
	public bool MustBeGrounded;
	public string groundtag="ground";
	public bool hidemesh;
	public bool hidewaypoints;
	public bool debugshowconnections=true;
	public bool disablewaypointscripts;
	public bool x64;
	public bool x256=true;
	public bool x576;
	public bool x1024;
	public bool x2304;
	public bool x4608;
	public bool x9216;
	public bool x18432;
	public bool x36864;
	
	private int listcounts;
	public bool makelevelwithground;
	
	public GameObject grid;
	public GameObject grid2;
	public List<Transform> gridlist256;
	public GameObject grid3;
	public List<Transform> gridlist512;
	public List<Transform> grids;
	public Transform grid64position;
	public List<Transform> grid256positions;
	public List<Transform> grid576positions;
	public List<Transform> grid1024positions;
	public List<Transform> grid2304positions;
	public List<Transform> waypoints;
	private bool activategrids;
	public string waypointtag="Waypoint";
	private bool endmeshcreate;
	private Transform gridposition;
	private int gridposcount;
	private GameObject gridclone;
	private bool gridclonedone;
	
	// Use this for initialization
	void Start () {
		if(BuildNavigationalMesh)Debug.Log("Building Mesh...");
		
		
	if(waypoints.Count>0){
			int wlistsize=waypoints.Count;
			for (int w = 0; w < wlistsize; w++){
				Waypoint way=(Waypoint)waypoints[w].GetComponent("Waypoint");	
					if(CollisionConnection)way.collisionconnect=true;
						else if(RaycastConnection)way.collisionconnect=false;
				if(hidewaypoints)waypoints[w].renderer.enabled=false;
			if(debugshowconnections)way.showconnections=true;
						else way.showconnections=false;
				
				if(disablewaypointscripts)way.enabled=false;
			
			}
			}
		
	gameObject.SetActiveRecursively(true);
	//renderer.enabled=false;
	AddAllways();
	}
	public void AddAllways()
	{
		
		if(BakeWaypoints){
		GameObject[] way = GameObject.FindGameObjectsWithTag(waypointtag);

		foreach(GameObject ways in way)
			Addtarget(ways.transform);
		}
		
	}
	public void Addtarget(Transform ways)
	{
		
		if(BakeWaypoints){
		waypoints.Add(ways);
		}
	}
	
	// Update is called once per frame
	void Update () {
	if(hidemesh) gameObject.renderer.enabled=false;
		
		//create navigational mesh
		if(BuildNavigationalMesh){
			
		//create grids
		
		if(gridclonedone){
			
			
		}
		else{
			if(x64) listcounts=1;
			else{
					if(x256) listcounts=grid256positions.Count;
					else{
					if(x576) listcounts=grid576positions.Count;	
						else{
					if(x1024) listcounts=grid256positions.Count;	
							else{
					if(x2304) listcounts=grid576positions.Count;	
								else{
									if(x4608) listcounts=grid1024positions.Count;	
									else{
									if(x9216) listcounts=grid2304positions.Count;
										else{
									if(x18432) listcounts=grid1024positions.Count;
										else{
									if(x36864) listcounts=grid2304positions.Count;
					}
					}
					}
				}
				}
				}
				}
				}
				
		if(grids.Count<listcounts){
					if(x64)gridposition=grid64position;
			else{
					if(x256) gridposition=grid256positions[gridposcount];
					else{
					if(x576) gridposition=grid576positions[gridposcount];
							else{
					if(x1024) gridposition=grid256positions[gridposcount];	
								else{
					if(x2304) gridposition=grid576positions[gridposcount];	
									else{
					if(x4608) gridposition=grid1024positions[gridposcount];
										else{
										if(x9216) gridposition=grid2304positions[gridposcount];
										else{
										if(x18432) gridposition=grid1024positions[gridposcount];
									else{
										if(x36864) gridposition=grid2304positions[gridposcount];
					}
					}
					}
				}
					}
					}
						}
					}
				
			if(x18432|x36864){
			gridclone=(GameObject)Instantiate (grid3.gameObject, gridposition.position, transform.rotation);			
					int lsize=gridlist512.Count;
			for (int i = 0; i < lsize; i++){
							Gridscript gridcom=(Gridscript)gridlist512[i].GetComponent("Gridscript");
							if(MustBeGrounded)gridcom.mustbegrounded=true;
							else gridcom.mustbegrounded=false;
							gridcom.groundtag=groundtag;
							}
					}
					else{
		if(x9216|x4608|x2304|x1024){
		gridclone=(GameObject)Instantiate (grid2.gameObject, gridposition.position, transform.rotation);			
			int lsize=gridlist256.Count;
			for (int i = 0; i < lsize; i++){
							Gridscript gridcom=(Gridscript)gridlist256[i].GetComponent("Gridscript");
							if(MustBeGrounded)gridcom.mustbegrounded=true;
							else gridcom.mustbegrounded=false;
							gridcom.groundtag=groundtag;
							}
						}
					else{
							gridclone=(GameObject)Instantiate (grid.gameObject, gridposition.position, transform.rotation);
						Gridscript gridcom=(Gridscript)gridclone.GetComponent("Gridscript");
							if(MustBeGrounded)gridcom.mustbegrounded=true;
							else gridcom.mustbegrounded=false;
							gridcom.groundtag=groundtag;
						}
						}
						
			grids.Add(gridclone.transform);
			gridposcount=gridposcount+1;	
			gridclone.transform.localScale=gridposition.transform.lossyScale;
			gridclone.gameObject.SetActiveRecursively(true);
			gridclone.transform.parent=transform;
		
		}
				else{
					
					gridclonedone=true;
				}
				
		}
	
		

		
		int listsize=grids.Count;
			for (int i = 0; i < listsize; i++){
		
			grids[i].gameObject.SetActiveRecursively(true);
				
				
				waypoints.TrimExcess();
				endmeshcreate=true;
				
		}
		}
			if(endmeshcreate){
			int wlistsize=waypoints.Count;
			for (int w = 0; w < wlistsize; w++){
				if(waypoints[w]){
			Waypoint way=(Waypoint)waypoints[w].GetComponent("Waypoint");
				if(way){
				way.baked=false;
				way.clearbakeddata=true;
				way.enabled=false;
					endmeshcreate=false;
						
				}
				}
		}
			
		}
		
		if(BakeWaypoints){
			int wlistsize=waypoints.Count;
			for (int w = 0; w < wlistsize; w++){
				if(waypoints[w]){
			Waypoint way=(Waypoint)waypoints[w].GetComponent("Waypoint");
				if(way){
						
						
						if(makelevelwithground)way.makelevelwithground=true;
						else way.makelevelwithground=false;
					if(hidewaypoints)waypoints[w].renderer.enabled=false;
						if(debugshowconnections)way.showconnections=true;
						else way.showconnections=false;
				way.clearbakeddata=true;
						way.collisionconnect=true;
				way.baked=false;
				way.enabled=true;
						BakeWaypoints=false;
				}
				}
		}
		
		}
		
		if(gridclonedone){
			if(gridposcount>=listcounts){
				Debug.Log("Build Complete!");
				gridposcount=0;
			}
			BuildNavigationalMesh=false;
		}
			
	}
}
