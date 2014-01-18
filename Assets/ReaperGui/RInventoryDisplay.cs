using UnityEngine;
using System.Collections;

[AddComponentMenu("Inventory/RInventory Display")]
[RequireComponent (typeof (Inventory))]

public class RInventoryDisplay : RIWindowDisplay {

	//Keeping track of components.
	public Inventory associatedInventory;
	private bool cSheetFound = false;
	private Character cSheet;

	public override void Awake(){

		windowSize = new Vector2(200, 200);
		windowName = "Inventory";
		base.Awake();
	}

	// Use this for initialization
	public override void Start() {
		
		base.Start();
		associatedInventory = GetComponent<Inventory>();//keepin track of the inventory script
		if (GetComponent<Character>() != null)
		{
			cSheetFound = true;
			cSheet = GetComponent<Character>();
		}
		else
		{
			Debug.LogError ("No Character script was found on this object. Attaching one allows for functionality such as equipping items.");
			cSheetFound = false;
		}
	}
	
	public override void  DisplayInventoryWindow(int windowID)
	{
		if (canBeDragged == true)
		{
			GUI.DragWindow (new Rect (0,0, 10000, 30));  //the window to be able to be dragged
		}
		
		float currentX = 0 + Offset.x; //Where to put the first items.
		float currentY = 18 + Offset.y; //Im setting the start y position to 18 to give room for the title bar on the window.
		
		foreach(Transform i in UpdatedList) //Start a loop for whats in our list.
		{
			Item item=i.GetComponent<Item>();
			Texture2D ic = (item.itemIcon == null)? defaultIcon: item.itemIcon;

			if (cSheetFound) //CSheet was found (recommended)
			{
				if(GUI.Button(new Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),ic))
				{
					bool dragitem=true; //Incase we stop dragging an item we dont want to redrag a new one.
					if(itemBeingDragged == item) //We clicked the item, then clicked it again
					{
						if (cSheetFound)
						{
							GetComponent<Character>().UseItem(item,0,true); //We use the item.
						}
						ClearDraggedItem(); //Stop dragging
						dragitem = false; //Dont redrag
					}
					if (Event.current.button == 0) //Check to see if it was a left click
					{
						if(dragitem)
						{
							if (item.isEquipment == true) //If it's equipment
							{
								itemBeingDragged = item; //Set the item being dragged.
								draggedItemSize=itemIconSize; //We set the dragged icon size to our item button size.
								//We set the position:
								draggedItemPosition.y=Screen.height-Input.mousePosition.y-15;
								draggedItemPosition.x=Input.mousePosition.x+15;
							}
							else
							{
								i.GetComponent<ItemEffect>().UseEffect(); //It's not equipment so we just use the effect.
							}
						}
					}
					else if (Event.current.button == 1) //If it was a right click we want to drop the item.
					{
						associatedInventory.DropItem(item);
					}
				}
			}
			else //No CSheet was found (not recommended)
			{
				if(GUI.Button(new Rect(currentX,currentY,itemIconSize.x,itemIconSize.y), ic))
				{
					if (Event.current.button == 0 && item.isEquipment != true) //Check to see if it was a left click.
					{
						i.GetComponent<ItemEffect>().UseEffect(); //Use the effect of the item.
					}
					else if (Event.current.button == 1) //If it was a right click we want to drop the item.
					{
						associatedInventory.DropItem(item);
					}
				}
			}
			
			if(item.stackable) //If the item can be stacked:
			{
				GUI.Label(new Rect(currentX, currentY, itemIconSize.x, itemIconSize.y), "" + item.stack, "Stacks"); //Showing the number (if stacked).
			}
			
			currentX += itemIconSize.x;
			if(currentX + itemIconSize.x + Offset.x > windowSize.x) //Make new row
			{
				currentX=Offset.x; //Move it back to its startpoint wich is 0 + offsetX.
				currentY+=itemIconSize.y; //Move it down a row.
				if(currentY + itemIconSize.y + Offset.y > windowSize.y) //If there are no more room for rows we exit the loop.
				{
					return;
				}
			}
		}
	}
	
	//Update the inv list Needs to be Overwridden
	public override void UpdateInventoryList()
	{
		UpdatedList = associatedInventory.Contents;
		//Debug.Log("Inventory Updated");
	}
}
