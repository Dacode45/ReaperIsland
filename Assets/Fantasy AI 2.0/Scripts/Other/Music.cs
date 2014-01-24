using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public AudioClip explore;
	public AudioClip buildup;
	public AudioClip crystalmusic;
	public Transform crystalarea;
	public Transform explorearea;
	public Transform explorearea2;
	public Transform buildarea;
	public Transform objecttopositionon;
	public Transform currentmusicarea;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(objecttopositionon){
			transform.position=objecttopositionon.transform.position;
		}
		
		
	}
	void OnTriggerEnter(Collider other){
	if(other.transform==explorearea){
			currentmusicarea=other.transform;
		if(audio.clip==explore){}
			else{
			audio.clip=explore;	
			audio.Play();
			}
		}
		
		if(other.transform==explorearea2){
			currentmusicarea=other.transform;
		if(audio.clip==explore){}
			else{
			audio.clip=explore;	
			audio.Play();
			}
		}
		
		
		if(other.transform==buildarea){
			currentmusicarea=other.transform;
		if(audio.clip==buildup){}
			else{
			audio.clip=buildup;	
			audio.Play();
			}
		}
		
		if(other.transform==crystalarea){
			currentmusicarea=other.transform;
		if(audio.clip==crystalmusic){}
			else{
			audio.clip=crystalmusic;	
			audio.Play();
			}
		}
		
		
	}
}
