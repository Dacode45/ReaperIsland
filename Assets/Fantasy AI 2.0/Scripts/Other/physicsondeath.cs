using UnityEngine;
using System.Collections;

public class physicsondeath : MonoBehaviour {
	public Transform objecttodisable;
	public Transform otherobject;
	public bool wongame;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	health hp=(health)GetComponent("health");
		if(otherobject){
		health hp2=(health)otherobject.GetComponent("health");
		if(hp.dead&hp2.dead){
				if(objecttodisable) objecttodisable.transform.gameObject.SetActiveRecursively(false);
		}
		}
		if(hp.dead){
			wongame=true;
			rigidbody.isKinematic=false;
			
		}
		
	}
}
