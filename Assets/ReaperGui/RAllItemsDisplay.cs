using UnityEngine;
using System.Collections;

[AddComponentMenu("Inventory/RI All Items Display")]
[RequireComponent(typeof(RInventory))]
[RequireComponent(typeof(RInventoryDisplay))]
public class RAllItemsDisplay : RIWindowDisplay {
	
	//Keeping track of components.
	private RInventory associatedInventory ;
	private bool cSheetFound = false;
	private RICharacter cSheet ;
	private bool iSheetFound = false;
	private RInventoryDisplay iSheet; 

	//Store components and adjust the window position.
	
	public override void Awake(){
		
		windowSize = new Vector2(200, 600);
		windowName = "All Inventory";
		associatedInventory=GetComponent<RInventory>();//keepin track of the inventory script
		if (GetComponent<RICharacter>() != null)
		{
			cSheetFound = true;
			cSheet = GetComponent<RICharacter>();
		}
		else
		{
			Debug.LogError ("No Character script was found on this object. Attaching one allows for functionality such as equipping items.");
			cSheetFound = false;
		}
		if (GetComponent<RInventoryDisplay>() != null)
		{
			iSheetFound = true;
			iSheet = GetComponent<RInventoryDisplay>();
		}
		else
		{
			Debug.LogError ("No Character script was found on this object. Attaching one allows for functionality such as equipping items.");
			iSheetFound = false;
		}
		base.Awake();
	}

	
	public  void Start() {
		

		UpdateInventoryList();
		Debug.Log("All Items Window Display is "+ windowId);
	}



	//Update the inv list
	public override void UpdateInventoryList()
	{
		UpdatedList = GameObject.Find("GameController").GetComponent<GameObjectList>().prefabs;

		Debug.Log("Inventory Updated");
	}
	

	//Setting up the Inventory window
	public override void  DisplayWindow(int windowID)
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

				Debug.Log("Item Drag ready");
				if(GUI.Button(new Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),item.itemIcon))
				{
					bool dragitem=true; //Incase we stop dragging an item we dont want to redrag a new one.
					if(guiWrapper.itemBeingDragged == item) //We clicked the item, then clicked it again
					{
						associatedInventory.AddItem(item.transform); //We use the item.
						ClearDraggedItem(); //Stop dragging
						dragitem = false; //Dont redrag
					}
					if (Event.current.button == 0) //Check to see if it was a left click
					{
						if(dragitem)
						{
							
							guiWrapper.itemBeingDragged = item; //Set the item being dragged.
							guiWrapper.draggedItemSize=itemIconSize; //We set the dragged icon size to our item button size.
							//We set the position:
							guiWrapper.draggedItemPosition.y=Screen.height-Input.mousePosition.y-15;
							guiWrapper.draggedItemPosition.x=Input.mousePosition.x+15;

						}
					}
					else if (Event.current.button == 1) //If it was a right click we want to drop the item.
					{
						associatedInventory.DropItem(item);
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
}

