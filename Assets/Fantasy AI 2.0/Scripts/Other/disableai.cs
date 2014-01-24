using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class disableai : MonoBehaviour {
	public List<Transform> unitlist;
	public bool disableaiatstart=false;
	public bool enableaiatstart=true;
	private int listsize;
	public bool disableais;
	
	
	// Use this for initialization
	void Start () {
		
	
AddAllEnemies();
	}
	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("AI");
		
		
		//foreach(GameObject go in GameObject.FindObjectsOfType<AI>())
		foreach(GameObject waynogo in go)
			Addtarget(waynogo.transform);
		}
	
	public void Addtarget(Transform waynogo)
	{
		unitlist.Add(waynogo);

	}
	

	
	// Update is called once per frame
	void Update () {
		listsize=unitlist.Count;
			for (int i = 0; i < listsize; i++){
				AI ais=(AI)unitlist[i].GetComponent("AI");
		if(disableais){
			
				ais.enabled=false;
				
		
		}
		else ais.enabled=true;
		
		}
	}
}
