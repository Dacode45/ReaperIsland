using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Meshsensor : MonoBehaviour {
	public bool MustBeGrounded=true;
	public string groundtag="ground";
	public bool collision;
	private bool checkcollisions;
	public int counter;
	
	public Transform way4;
	public Transform way16;
	public Transform way64;
	public Transform way128;

	
	public List<GameObject> objects;
	

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {


		counter=counter+1;
		
		
		if(counter>=1){
			if(MustBeGrounded){
			if(objects.Count<1){
				if(way4) Destroy(way4.gameObject);
		if(way16)Destroy(way16.gameObject);
		if(way64)Destroy(way64.gameObject);
		if(way128)Destroy(way128.gameObject);
			}
			}
			
			int listsize=objects.Count;
			for (int i = 0; i < listsize; i++){
			
				
		if(objects[i].tag==groundtag){}
		else{	
			
		if(way4) Destroy(way4.gameObject);
		if(way16)Destroy(way16.gameObject);
		if(way64)Destroy(way64.gameObject);
		if(way128)Destroy(way128.gameObject);	
					
				}	
			}	
			}
		
		
			
		if(counter>=2){
				
			if(way16&way4) Destroy(way4.gameObject);
			if(way64&way16) Destroy(way16.gameObject);
			if(way128&way64) Destroy(way64.gameObject);
		
				
			Destroy(gameObject);
		}
		
		
	}
	void OnTriggerEnter(Collider other){
	
		objects.Add(other.gameObject);

		}
	
			
			
		

}
