using UnityEngine;
using System.Collections;


public class health : MonoBehaviour {
	//max health. you set this to what you want his health to be
	public int maxhealth=50;
	public int currenthealth;
	//cant die
	public bool invincible;
	public bool dead;
	//check if ou want to enable health regeneration
	public bool regenerate;
	//time between each generate
	public float regenerationtime=0.8f;
	//the timer
	private float regtimer;
	//amount of hp to regenerate per generate
	public int regenerationamount=2;
	private int healthsave;
	public bool givereward;
	private bool rewardgiven;
	public int xptogive=10;
	public int goldtogive=10;
	
	
	// Use this for initialization
	void Start () {
		givereward=true;
		
		AI ai=(AI)GetComponent("AI");
		if(ai) ai.health=maxhealth;
		
	currenthealth=maxhealth;
		healthsave=maxhealth;
	}
	
	// Update is called once per frame
	void Update () {
	
		
		
		//tell ai when health is changed
		if(healthsave>currenthealth|healthsave<currenthealth){
			AI ai=(AI)GetComponent("AI");
			if(ai){
			ai.health=currenthealth;
			healthsave=currenthealth;
			}
		}
		
		//death 
		if(dead){
			AI ai=(AI)GetComponent("AI");
			//tell ai that he is dead
			if(ai)ai.dead=true;
		}
		else givereward=true;
		
		if(currenthealth>=maxhealth) currenthealth=maxhealth;
		
		//if health is less than zero
		if(currenthealth<=0){
			//check if invincible if not death
			if(invincible)dead=false;
			else dead=true;
			currenthealth=0;
		}
		//regenerate
		if(regenerate){
			if(currenthealth<maxhealth&currenthealth>0){
		regtimer+=Time.deltaTime;
		if(regtimer>regenerationtime){
					currenthealth=currenthealth+regenerationamount;
					regtimer=0;
				}
		
		}
		}
	}
}
