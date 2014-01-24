using UnityEngine;
using System.Collections;

public class Navmeshtarget : MonoBehaviour {
	//THE CURRENT WAYPOINT TARGET IS TOUCHING
	public Transform currentwaypoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	//GET THE COLLIDING WAYPOINT
	void OnTriggerEnter(Collider other){
		Waypoint waypoint=(Waypoint)other.GetComponent("Waypoint");
		if(waypoint){
			currentwaypoint=waypoint.transform;
		}
		
	}
}
