using UnityEngine;
using System.Collections;

public class CausticProject : MonoBehaviour {

	float fps = 30f;
	public Texture2D[] causticImages;
	Projector[] proj;
	int textureIndex = 0;

	// Use this for initialization
	void Start () {
		proj = GetComponents<Projector>();

		InvokeRepeating("Project", 1*1/fps, 1*1/fps);

	}

	void Project (){
		//Debug.Log("Projecting Caustic");
		foreach(Projector p in proj){
		p.material.SetTexture("_ShadowTex", causticImages[textureIndex]);
		}
		textureIndex = (textureIndex +1 )%causticImages.Length;

	}
	

}
