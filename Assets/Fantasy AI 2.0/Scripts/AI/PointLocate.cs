using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointLocate : MonoBehaviour {
	public Transform arrow;
	public List<Transform> orderunits;
	public GameObject locaternode;
	public GameObject newnode;
	private int framecount;
	private bool rightclicked;
	private bool leftclicked;
	
	// Use this for initialization
	void Start () {
	framecount=9999999;
	}
	
	// Update is called once per frame
	void Update () {
	RaycastHit hit = new RaycastHit();	
		arrow.renderer.enabled=true;
		//var fwd = transform.TransformDirection (Vector3.forward);
		if(Physics.Raycast(transform.position, transform.forward, out hit, 1000000, 9)){
		
		}
		arrow.transform.position=hit.point;
		if(Input.GetKeyUp(KeyCode.Mouse1)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){	
			AI ai=(AI)orderunits[i].GetComponent("AI");		
				ai.changestate=true;
				ai.runtotarget=true;
				ai.walktotarget=false;
			}
			rightclicked=true;
			leftclicked=false;
			Locatorsensor loc=(Locatorsensor)arrow.GetComponent("Locatorsensor");
			loc.currentwaypoint=null;
			framecount=3;
		}
		
		if(Input.GetKeyUp(KeyCode.Mouse0)){
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){	
			AI ai=(AI)orderunits[i].GetComponent("AI");		
				ai.changestate=true;
				ai.runtotarget=true;
				ai.walktotarget=false;
			}
			
			leftclicked=true;
			rightclicked=false;
			Locatorsensor loc=(Locatorsensor)arrow.GetComponent("Locatorsensor");
			loc.currentwaypoint=null;
			framecount=3;
		}
		
		framecount=framecount-1;
		
		if(framecount<=0){
			Locatorsensor loc=(Locatorsensor)arrow.GetComponent("Locatorsensor");
			if(loc.currentwaypoint){
		newnode=(GameObject)Instantiate (locaternode.gameObject, arrow.position, arrow.transform.rotation);
			
			
			int listsize=orderunits.Count;
			for (int i = 0; i < listsize; i++){
			AI ai=(AI)orderunits[i].GetComponent("AI");
					
			if(rightclicked) ai.gototarget=true;
			if(leftclicked) ai.gototargetnostop=true;
			ai.targetlocateto=newnode.transform;
			ai.Dialogrespond=true;
				arrow.renderer.enabled=false;
				enabled=false;
				framecount=9999999;
			}
			}
			else framecount=9999999;
			}
		}
	
	void OnGUI ( )
{
   
        GUI.Label(new Rect(Screen.width * 0.5f - 50f, Screen.height * 0.5f - 10f, 600f, 20f), "Rightclick to order units to location. LeftClick to order units to location without stopping");
    
}
}
