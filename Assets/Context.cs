using UnityEngine;
using System.Collections;

[System.Serializable] 
public class Context : MonoBehaviour {
	
	private int weight = 0;

	public enum Crouch{Default};
	public enum Sprint{Default};
	public enum Jump{Default, Climb};
	public enum Grab{Default, LatchOn};

	public Crouch onCrouch = Crouch.Default;
	 public Sprint onSprint = Sprint.Default;
	public Jump onJump = Jump.Default;
	 public Grab onGrab = Grab.Default;

	private static readonly Context defaultContext = new Context();

	public static Context GetDefaultContext(){
		return defaultContext;
	}

	 void Awake(){
		if (tag == "obstacle") weight = 1;
	}

	public int getWeight(){
		return weight;
	}
}
