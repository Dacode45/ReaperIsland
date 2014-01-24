using UnityEngine;
using System.Collections;

public class team : MonoBehaviour {
	//the team the ai is on
	public int teamnumber;
	//if checked off ai willuse dynamic teams feature
	public bool dynamicteams;
	//will attack anyone and be attacked by anyone
	public bool hostile;
	public Transform mytransform;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mytransform=transform;
	
	}
}
