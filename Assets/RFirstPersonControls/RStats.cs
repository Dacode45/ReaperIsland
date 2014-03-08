using UnityEngine;
using System.Collections;

public class RStats : MonoBehaviour {
	private float movement = .9f;
	private float run = .7f;
	private float swim = .7f;
	private float jump = 2f;
	private float pacs = .7f; //Physical Ability Charge Speed
	private float strength = .25f;

	public float movementModifier{
		get{ return movement * ApplyMovementBuffs();}
	}

	public float swimModifier{
		get{ return swim * ApplySwimBuffs();}
	}
	public float jumpModifier{
		get{ return jump * ApplyJumpBuffs();}
	}
	public float pacsModifier{
		get{ return pacs * ApplyMovementBuffs();}
	}
	public float runModifier{
		get{return run;}
	}
	public float strengthModifier{
		get{return strength;}
	}

	private ArrayList buffs = new ArrayList();
	

	// Update is called once per frame
	void Update () {
		foreach(Buff b in buffs){
			if(b.GetStartTime() + b.GetDuration() < Time.time){
				buffs.Remove(b);
				Destroy(b);
			}
		
		}
	}

	
	public void AddBuff(Buff b){
		b.SetStartTime(Time.time);
		buffs.Add(b);
	}

	private float ApplyMovementBuffs(){
		float ret = 1;
		foreach(Buff b in buffs){
			if(b.eff == (int)Buff.Effects.SPEED && b.constrainTo == (int)RFirstPersonCharacter.Action.Defualt){
				ret *= b.GetStrength();
			}
		}

		return ret;
	}

	private float ApplySwimBuffs(){
		float ret = 1; 
		foreach(Buff b in buffs){
			if(b.eff == (int)Buff.Effects.SPEED && b.constrainTo == (int)RFirstPersonCharacter.Action.Swimming){
				ret *= b.GetStrength();
			}
		}
		return ret;
	}

	private float ApplyJumpBuffs(){
		float ret = 1; 
		foreach(Buff b in buffs){
			if(b.eff == (int)Buff.Effects.SPEED && b.constrainTo == (int)Buff.Affects.JUMP){
				ret *= b.GetStrength();
			}
		}
		return ret;
	}

	
}
