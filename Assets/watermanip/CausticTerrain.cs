using UnityEngine;

using System.Collections;



public class CausticTerrain : MonoBehaviour {
	
	
	
	public Texture2D[] bumpMaps;
	
	public float[] specularPowers;
	
	public Color specularColor = Color.grey;
	
	public Texture2D[] detailMaps;
	
	public float detailFPS = 15.0F;
	
	
	
	void Awake () {
		
		Terrain terrain = (Terrain)GetComponent(typeof(Terrain));
		
		int splatCount = terrain.terrainData.alphamapLayers;
		
		
		
		float[] tileSizeX = new float[splatCount];
		
		float[] tileSizeZ = new float[splatCount];
		
		for (int i = 0; i < splatCount; i++) {
			
			tileSizeX[i] = terrain.terrainData.splatPrototypes[i].tileSize.x;
			
			tileSizeZ[i] = terrain.terrainData.splatPrototypes[i].tileSize.y;
			
			string tileX = "_TileX0"+i.ToString();
			
			string tileZ = "_TileZ0"+i.ToString();
			
			Shader.SetGlobalFloat (tileX, tileSizeX[i]);
			
			Shader.SetGlobalFloat (tileZ, tileSizeZ[i]);
			
		}
		
		
		
		for (int i = 0; i < bumpMaps.Length; i++) {
			
			if (bumpMaps[i] != null) {
				
				string bump = "_Bump0"+i.ToString();
				
				Shader.SetGlobalTexture(bump, bumpMaps[i]);
				
			}
			
		}
		
		
		
		for (int i = 0; i < specularPowers.Length; i++) {
			
			string spec = "_SpecPow0"+i.ToString();
			
			Shader.SetGlobalFloat(spec, specularPowers[i]);
			
		}
		
		Shader.SetGlobalColor("_SpecCol", specularColor);
		
		Shader.SetGlobalFloat("_TerX", terrain.terrainData.size.x);
		
		Shader.SetGlobalFloat("_TerZ", terrain.terrainData.size.z);
		
	}
	
	
	
	void Update () {
		
		float i = Time.time * detailFPS;
		
		i = i % detailMaps.Length;
		
		//DetailTX = detailMaps[(int)i];
		
		Shader.SetGlobalTexture("_DetailMap", detailMaps[(int)i]);
		
	}
	
}