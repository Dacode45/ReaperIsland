using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {
	public Transform shooter;
	public int flyspeed=50;
	public float destroytime=20f;
	public bool enablegravityfall=true;
	public float gravityaccelerationmultiply=1;
	private float grav=0;
	//this stops the projectile in its tracks
	public bool stopforce;
	private float timer;
	//the amount of time to destroy projectile
	private float destroytimer=10;
	public bool destroyonimpact;
	public int damage=100;
	//this is set to -9999 cause it needs to be told by the shooter what team its on if friendly fire is disabled
	public int team=-9999;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		destroytimer+=Time.deltaTime;
		if(destroytimer>destroytime)Destroy(gameObject);
		
		
		if(stopforce){}
		else{

			if(enablegravityfall){
			timer+=Time.deltaTime;
			if(timer>0.1f){
			grav=grav+0.4f*gravityaccelerationmultiply;
				timer=0;
			}
			}
			
			if(grav>15)grav=15;
			
			transform.position += transform.forward * flyspeed * Time.deltaTime;
			transform.position -= transform.up * grav * Time.deltaTime;
		
		}
	}
	void OnCollisionEnter(Collision other) {
		if(shooter){
		
		health hp=(health)other.gameObject.GetComponent("health");
		team tm=(team)other.gameObject.GetComponent("team");
		if(hp&tm){
			if(other.transform==shooter){}
			else{
			
				if(tm.teamnumber==team||tm.teamnumber<0){}
					else{
						stopforce=true;
			hp.currenthealth=hp.currenthealth-damage;
						Destroy(gameObject);
					}
			
				}
			}
		else{
		if(other.transform==shooter){}
			else{
   if(destroyonimpact){
			Destroy(gameObject);			
					}
					
	stopforce=true;
	if(rigidbody)Destroy(rigidbody);
					collider.enabled=false;
					audio.Stop();
				}
				
			}
		}
	
	}
}
