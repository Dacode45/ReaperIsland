using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class ContextSensor : MonoBehaviour {

	//List of objects in frustrum

	private PriorityQueue<Collider> contextualObjects;
	//Distance modifiers
	float far;
	float near;

	//Point light for context object
	GameObject lightGameObject;


	int updateEvery = 50;//how often to run
	private int pastUpdates = 1;

	void Start(){
		//Getting Frustrum size
		Bounds b = GetComponent<MeshFilter>().mesh.bounds;
		far = (b.extents-b.center).z*this.transform.localScale.z;
		near = far/2;
		contextualObjects = new PriorityQueue<Collider>();
		lightGameObject = new GameObject("Immediate Object Light");
		lightGameObject.AddComponent<Light>();
		lightGameObject.light.color = Color.blue;
		lightGameObject.light.intensity *= 10;
		lightGameObject.transform.position = new Vector3(0, 5, 0);
	}
	void Update(){

		if(pastUpdates%updateEvery == 0){//Every 5 fixed updates sort.
			Vector3 currentPos = transform.position;
			lightGameObject.SetActive(false);
			
			contextualObjects.Sort();
			if(contextualObjects[0] != null){
				lightGameObject.SetActive(true);
				lightGameObject.transform.position = this[0].transform.position;
			}
			pastUpdates = 0;
		}

		pastUpdates++;
	
	}

	void OnTriggerEnter(Collider other){
		contextualObjects.Add(other, CalculatePriority(other));
		
	}
	void OnTriggerExit(Collider other){
		contextualObjects.Remove(
			other
		               
	);
		
	}

	int CalculatePriority(Collider other){
		float distance = Vector3.Distance(this.transform.position, other.transform.position);
		if(other.gameObject.GetComponent<Context>() != null){
			return (int)(other.gameObject.GetComponent<Context>().getWeight()*distance);}
		else
			return (int)distance;
	}


	public override string ToString ()
	{
		return "There are " + contextualObjects.Count + " objects in context";
	}

	public Collider this[int index] {
		get {
			return (contextualObjects.Count > 0)? contextualObjects[0]: null;
		}
	}


}
