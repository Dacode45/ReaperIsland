using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalLOD : MonoBehaviour {
	public List<Transform> rendersshow;
	public List<Transform> rendershide;
	public Transform player;
	
	// Use this for initialization
	void Start () {
		int listsize=rendersshow.Count;
	for (int i = 0; i < listsize; i++){
			if(rendersshow[i]){
				if(rendersshow[i].renderer)rendersshow[i].renderer.enabled=false;
			}
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if(other.transform==player.transform){
		int listsize=rendersshow.Count;
			for (int i = 0; i < listsize; i++){
			if(rendersshow[i]){
				if(rendersshow[i].renderer)rendersshow[i].renderer.enabled=true;
			}
			}
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.transform==player.transform){
		int listsize=rendersshow.Count;
			for (int i = 0; i < listsize; i++){
			if(rendersshow[i]){
				if(rendersshow[i].renderer)rendersshow[i].renderer.enabled=false;
			}
			}
		}
	}
}
