using UnityEngine;

public class Buff : MonoBehaviour {
	
	public enum Effects { HEALTH = 1, SPEED = 2};
	public enum Affects { DEFAULT = 0, SWIMMING = 1, JUMP = 11};
	public int constrainTo;
	public int eff;
	private float strength; 
	private float duration;
	private float startTime;

	

	/*
	 *
	 * Takes Effect, value, and duration. duration of 0 is instant heal
	  */
	 public  Buff( float value=0, int e=0, float d = 0.0f, int a = 0){
		eff = e;
		strength = value;
		duration = d;
		constrainTo = a;
	}

	public float GetDuration(){
		return duration;
	}

	public float GetStrength(){
		return strength;
	}

	public float GetStartTime(){
		return startTime;
	}
	public void SetStartTime(float s){
		startTime = s;
	}
}
