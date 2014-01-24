using UnityEngine;
using System.Collections;

public class Rangedaddon : MonoBehaviour {
	//THIS HAS TO BE ASSIGNED its the projectile that he shoots.  cant be ranged unless something is shot
	public GameObject projectile;
	public int damage=60;
	public bool friendlyfire;
	private Transform cloneproj;
	public Transform projecttilestartpos;
	public float timebeforeappears=0.3f;
	public float timebeforefires=1.2f;
	private bool clone;
	private GameObject proj;
	public AudioClip firesound;
	private bool fireplay;
	
	
	// Use this for initialization
	void Start () {
		clone=true;
		if(projectile){
//if(projectile)	projectile.gameObject.SetActiveRecursively(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	AI ai=(AI)GetComponent("AI");
		team team=(team)GetComponent("team");
		
		if(ai.attack){}
		else{
			if(proj){
			projectile arrow=(projectile)proj.GetComponent("projectile");
			if(arrow){
				if(arrow.enabled){}
					else{
					Destroy(proj.gameObject);
				}
				}
			}
			
			clone=true;
	
		}
		if(projectile){
		if(ai.attack){
			
			if(ai.attacktimer>timebeforeappears&ai.attacktimer<timebeforefires){
				if(clone){
			proj=(GameObject)Instantiate (projectile.gameObject, projecttilestartpos.position, transform.rotation);
				
					projectile arrow=(projectile)proj.GetComponent("projectile");
					arrow.damage=damage;
					fireplay=true;
					proj.audio.Stop();
					if(friendlyfire){}
					else arrow.team=team.teamnumber;
					arrow.shooter=transform;
					if(arrow)arrow.enabled=false;
				//if(proj.rigidbody)proj.rigidbody.isKinematic=true;
					clone=false;
					
				}
				if(proj){
				proj.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ai.eyespot.transform.position - proj.transform.position), 8000000 * Time.deltaTime);
				proj.transform.position=projecttilestartpos.position;
				}
			}
				if(ai.attacktimer>timebeforefires){
				audio.clip=firesound;
				if(fireplay){
					audio.Play();
					if(proj) if(proj.audio) proj.audio.Play();
				}
				fireplay=false;
				clone=true;
					if(proj){
					
				projectile arrow=(projectile)proj.GetComponent("projectile");
			if(arrow)arrow.enabled=true;

				}
				
			}
			}
		}
		
			
	}
}
