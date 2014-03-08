using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Inventory/RInventory")]
public class RInventory : MonoBehaviour {

	//This is the central piece of the Inventory System.
	
	public Transform[] Contents; //The content of the Inventory
	public int MaxContent = 12; //The maximum number of items the Player can carry.
	
	public bool DebugMode = false; //If this is turned on the Inventory script will output the base of what it's doing to the Console window.
	
	private RInventoryDisplay playersInvDisplay ; //Keep track of the InventoryDisplay script.
	
	public  Transform itemHolderObject; //The object the unactive items are going to be parented to. In most cases this is going to be the Inventory object itself.


		
		//Handle components and assign the itemHolderObject.
	void Awake ()
	{
		itemHolderObject = gameObject.transform;
		
		playersInvDisplay = GetComponent<RInventoryDisplay>();
		if (playersInvDisplay == null)
		{
			Debug.LogError("No Inventory Display script was found on " + transform.name + " but an Inventory script was.");
			Debug.LogError("Unless a Inventory Display script is added the Inventory won't show. Add it to the same gameobject as the Inventory for maximum performance");
		}
	}
	
	//Add an item to the inventory.
	public void AddItem(Transform Item)
	{
		List<Transform> newContents = new List<Transform>(Contents);
		newContents.Add(Item);
		Contents=newContents.ToArray(); //list to array
		
		if (DebugMode)
		{
			Debug.Log(Item.name+" has been added to inventroy");
		}
		
		//Tell the InventoryDisplay to update the list.
		if (playersInvDisplay != null)
		{
			playersInvDisplay.UpdateInventoryList();
		}
	}
	
	//Removed an item from the inventory (IT DOESN'T DROP IT).
	public void RemoveItem(Transform Item)
	{
		List<Transform> newContents = new List<Transform>(Contents);
		int index=0;
		bool shouldend=false;
		foreach(Transform i in newContents) //Loop through the Items in the Inventory:
		{
			if(i == Item) //When a match is found, remove the Item.
			{
				newContents.RemoveAt(index);
				shouldend=true;
				//No need to continue running through the loop since we found our item.
			}
			index++;
			
			if(shouldend) //Exit the loop
			{
				Contents=newContents.ToArray() ;
				if (DebugMode)
				{
					Debug.Log(Item.name+" has been removed from inventroy");
				}
				if (playersInvDisplay != null)
				{
					playersInvDisplay.UpdateInventoryList();
				}
				return;
			}
		}
	}
	
	//Dropping an Item from the Inventory
	public void DropItem(Item item)
	{
		gameObject.SendMessage ("PlayDropItemSound", SendMessageOptions.DontRequireReceiver); //Play sound
		
		bool makeDuplicate = false;
		if (item.stack == 1) //Drop item
		{
			RemoveItem(item.transform);
		}
		else //Drop from stack
		{
			item.stack -= 1;
			makeDuplicate = true;
		}
		
		item.DropMeFromThePlayer(makeDuplicate); //Calling the drop function + telling it if the object is stacked or not.
		
		if (DebugMode)
		{
			Debug.Log(item.name + " has been dropped");
		}
	}
	
	//This will tell you everything that is in the inventory.
	public void DebugInfo()
	{
		Debug.Log("Inventory Debug - Contents");
		int items=0;
		foreach(Transform i in Contents){
			items++;
			Debug.Log(i.name);
		}
		Debug.Log("Inventory contains "+items+" Item(s)");
	}
	
	//Drawing an 'S' in the scene view on top of the object the Inventory is attached to stay organized.
	void OnDrawGizmos ()
	{
		Gizmos.DrawIcon (new Vector3((float)transform.position.x, (float)(transform.position.y + 2.3), (float)transform.position.z), "InventoryGizmo.png", true);
	}
}
