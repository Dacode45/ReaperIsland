using UnityEngine;
using System.Collections;

public class Locatorsensor : MonoBehaviour {
	public Transform currentwaypoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
void OnTriggerStay(Collider other){
		Waypoint waypoint=(Waypoint)other.GetComponent("Waypoint");
		if(waypoint){
			currentwaypoint=waypoint.transform;
		}
		
	}
}
