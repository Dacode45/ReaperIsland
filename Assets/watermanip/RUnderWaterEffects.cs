using UnityEngine;
using System.Collections;

public class RUnderWaterEffects : MonoBehaviour {

	[SerializeField] float waterLevel =-1000;
	[SerializeField] bool isUnderwater = false;
	ParticleSystem bubbles;
	[SerializeField] bool hasBubbles;
	RFirstPersonCharacter chMotor;
	[SerializeField]Color normalColor;
	[SerializeField]Color underWaterColor;
	[SerializeField] float normalFogDensity = .002f;
	[SerializeField] float underwaterFogDensity = .03f;
	[SerializeField] float lastTimeUnderWater;
	CausticProject[] causticProjector;


	// Use this for initialization
	void Start () {
		normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		if(underWaterColor == null) underWaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
		bubbles = GetComponentInChildren<ParticleSystem>();
		chMotor = transform.parent.GetComponent<RFirstPersonCharacter>();
		causticProjector = GetComponentsInChildren<CausticProject>();
		hasBubbles = (bubbles != null);
		setNormal();
	}
	
	// Update is called once per frame
	void Update () {

	
		if(transform.position.y <= waterLevel+1){
			chMotor.act = RFirstPersonCharacter.Action.Swimming;
		}else
			chMotor.act = RFirstPersonCharacter.Action.Defualt;

		if(transform.position.y < waterLevel){
			isUnderwater = true;
		}else
			isUnderwater = false; 
		if(isUnderwater){
			setUnderwater();
		}else	
			setNormal();
		if(Time.time - lastTimeUnderWater > 10 && isUnderwater == false){
			waterLevel = -100;
		}


	}


	void OnTriggerEnter(Collider c){
		Debug.Log("Collision");
		if(c.tag == "AboveWater"){
		Debug.Log("Toggling Under Water Effects" );
			waterLevel = c.transform.position.y;
		
		}


	}


	void setUnderwater(){
		RenderSettings.fogColor = underWaterColor;
		RenderSettings.fogDensity = underwaterFogDensity;
		if(hasBubbles){
			bubbles.Play();
		}
		lastTimeUnderWater = Time.time;

		foreach(CausticProject c in causticProjector){
			c.gameObject.SetActive(true);

		}
	}

	void setNormal(){
		RenderSettings.fogColor = normalColor;
		RenderSettings.fogDensity = normalFogDensity;
		if(hasBubbles){
			bubbles.Stop();
			bubbles.Clear();
		}

		foreach(CausticProject c in causticProjector) c.gameObject.SetActive(false);

	}
}
