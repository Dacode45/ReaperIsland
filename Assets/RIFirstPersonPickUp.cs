using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Item))]
public class RIFirstPersonPickUp : MonoBehaviour {

	//Assign this script to an Item if you want to pick it up in First Person. If this script is not attached the Item can only be picked up when clicking on it with the mouse.
	
//	GUISkin InstructionBoxSkin; //The skin to use. Default one is 'OtherSkin' under the 'Resources' folder.
	float PickUpDistance = 1.7f; //The distance from where the Item can be picked up. Remember that this is relative to the center of the Item and the center of the Player.
	
	//These store information about the Item, if we can pick it up, the Player and the distance to the Player.
	private bool canPickUp = false;
	private Item theItem;
	private Transform thePlayer;
	private float dist = 9999f;
	
	[AddComponentMenu ("RInventory/Items/RFirst Person Pick Up")]
			
			//This is where we find the usefull information which we can later access.
	void Awake ()
	{
		theItem = GetComponent<Item>();

		/*
		if (InstructionBoxSkin == null)
		{
			InstructionBoxSkin = Resources.Load("OtherSkin", GUISkin);
		}
		*/
	}
	
	void RetrievePlayer ( Inventory theInv)
	{
		thePlayer = theInv.transform.parent;
	}
	
	void OnGUI ()
	{
		//This is where we draw a box telling the Player how to pick up the item.
		//GUI.skin = InstructionBoxSkin;
		GUI.color = new Color(1, 1, 1, 0.7f);
		
		if (canPickUp == true)
		{
			if (transform.name.Length <= 7)
			{
				GUI.Box (new Rect (Screen.width*0.5f-(165*0.5f), 200, 165, 22), "Press E to pick up " + transform.name + ".");
			}
			else
			{
				GUI.Box (new Rect (Screen.width*0.5f-(185*0.5f), 200, 185, 22), "Press E to pick up " + transform.name + ".");
			}
		}
	}
	
	void Update ()
	{
		if (thePlayer != null)
		{
			//This is where we enable and disable the Players ability to pick up the item based on the distance to the player.
			dist = Vector3.Distance(thePlayer.position, transform.position);
			if (dist <= PickUpDistance)
			{
				canPickUp = true;
			}
			else
			{
				canPickUp = false;
			}
			

		}
	}

	void AttemptPickUp(){
		//This is where we allow the player to press the ButtonToPress to pick up the item.
		if ( canPickUp == true)
		{
			theItem.PickUpItem();
		}
	}
	
	//This is just for drawing the sphere in the scene view for easy testing.
	void OnDrawGizmosSelected () 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, PickUpDistance);
	}
}
