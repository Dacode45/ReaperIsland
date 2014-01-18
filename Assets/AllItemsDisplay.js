
//Variables for dragging:
static var itemBeingDragged : Item; //This refers to the 'Item' script when dragging.
private var draggedItemPosition : Vector2; //Where on the screen we are dragging our Item.
private var draggedItemSize : Vector2;//The size of the item icon we are dragging.

//Variables for the window:
var windowSize:Vector2 = Vector2(375, 162.5); //The size of the Inventory window.
var useCustomPosition = false; //Do we want to use the customPosition variable to define where on the screen the Inventory window will appear?
var customPosition : Vector2 = Vector2 (70, 400); // The custom position of the Inventory window.
var itemIconSize : Vector2 = Vector2(60.0, 60.0); //The size of the item icons.

//Variables for updating the inventory
var updateListDelay = 9999;//This can be used to update the Inventory with a certain delay rather than updating it every time the OnGUI is called.
//This is only useful if you are expanding on the Inventory System cause by default Inventory has a system for only updating when needed (when an item is added or removed).
private var lastUpdate = 0.0; //Last time we updated the display.
private var UpdatedList : Item[]; //The updated inventory array.

//More variables for the window:
static var displayInventory = false; //If inv is opened.
private var windowRect =Rect (200,200,108,130); //Keeping track of the Inventory window.
var invSkin:GUISkin; //This is where you can add a custom GUI skin or use the one included (InventorySkin) under the Resources folder.
var Offset : Vector2 = Vector2 (7, 12); //This will leave so many pixels between the edge of the window (x = horizontal and y = vertical).
var canBeDragged = true; //Can the Inventory window be dragged?

var onOffButton : KeyCode = KeyCode.I; //The button that turns the Inventory window on and off.

//Keeping track of components.
private var associatedInventory : Inventory;
private var cSheetFound = false;
private var cSheet : Character;
private var iSheetFound = false;
private var iSheet:InventoryDisplay; 

@script AddComponentMenu ("Inventory/All Items Display")
@script RequireComponent(Inventory)
@script RequireComponent(InventoryDisplay)

//Store components and adjust the window position.
function Awake()
{
	if (useCustomPosition == false)
	{
		windowRect=Rect(windowSize.x+70,windowSize.y+70,windowSize.x,windowSize.y);
	}
	else
	{
		windowRect = Rect (customPosition.x, customPosition.y, windowSize.x, windowSize.y);
	}
	associatedInventory=GetComponent(Inventory);//keepin track of the inventory script
	if (GetComponent(Character) != null)
	{
		cSheetFound = true;
		cSheet = GetComponent(Character);
	}
	else
	{
		Debug.LogError ("No Character script was found on this object. Attaching one allows for functionality such as equipping items.");
		cSheetFound = false;
	}
	if (GetComponent(InventoryDisplay) != null)
	{
		iSheetFound = true;
		iSheet = GetComponent(InventoryDisplay);
	}
	else
	{
		Debug.LogError ("No Character script was found on this object. Attaching one allows for functionality such as equipping items.");
		iSheetFound = false;
	}
}

function Start(){
	UpdateInventoryList();
}

//Update the inv list
function UpdateInventoryList()
{
	
	UpdatedList = gameObject.Find("GameController").GetComponent("GameObjectList").prefabs;
	Debug.Log("Inventory Updated");
}

function Update()
{
	if(Input.GetKeyDown(KeyCode.Escape)) //Pressed escape
	{
		ClearDraggedItem(); //Get rid of the dragged item.
	}
	if(Input.GetMouseButtonDown(1)) //Pressed right mouse
	{
		ClearDraggedItem(); //Get rid of the dragged item.
	}
	
	//Turn the Inventory on and off and handle audio + pausing the game.
	if(Input.GetKeyDown(onOffButton))
	{
		if (displayInventory)
		{
			displayInventory = false;
			
			gameObject.SendMessage ("ChangedState", false, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("PauseGame", false, SendMessageOptions.DontRequireReceiver); //StopPauseGame/EnableMouse/ShowMouse
		}
		else
		{
			displayInventory = true;
			
			gameObject.SendMessage ("ChangedState", true, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("PauseGame", true, SendMessageOptions.DontRequireReceiver); //PauseGame/DisableMouse/HideMouse
		}
	}
	
	//Making the dragged icon update its position
	if(itemBeingDragged!=null)
	{
		//Give it a 15 pixel space from the mouse pointer to allow the Player to click stuff and not hit the button we are dragging.
		draggedItemPosition.y=Screen.height-Input.mousePosition.y+15;
		draggedItemPosition.x=Input.mousePosition.x+15;
	}
	
	//Updating the list by delay
	if(Time.time>lastUpdate){
		lastUpdate=Time.time+updateListDelay;
		UpdateInventoryList();
	}
}

//Drawing the Inventory window
function OnGUI()
{
	GUI.skin = invSkin; //Use the invSkin
	if(itemBeingDragged != null) //If we are dragging an Item, draw the button on top:
	{
		GUI.depth = 3;
		GUI.Button(Rect(draggedItemPosition.x,draggedItemPosition.y,draggedItemSize.x,draggedItemSize.y),itemBeingDragged.itemIcon);
		GUI.depth = 0;
	}
	
	//If the inventory is opened up we create the Inventory window:
	if(displayInventory)
	{
		windowRect = GUI.Window (2, windowRect, DisplayInventoryWindow, "All Items");
	}
}

//Setting up the Inventory window
function DisplayInventoryWindow(windowID:int)
{

	if (canBeDragged == true)
	{
		GUI.DragWindow (Rect (0,0, 10000, 30));  //the window to be able to be dragged
	}
	
	var currentX = 0 + Offset.x; //Where to put the first items.
	var currentY = 18 + Offset.y; //Im setting the start y position to 18 to give room for the title bar on the window.
	
	Debug.Log("item value" + UpdateInventoryList.ToString());
	for(var i:Item in UpdatedList) //Start a loop for whats in our list.
	
	{
		Debug.Log("This item list has" + i.name);
		var item=i;
			if(GUI.Button(Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),item.name))
			{
				var dragitem=true; //Incase we stop dragging an item we dont want to redrag a new one.
				if(itemBeingDragged == item) //We clicked the item, then clicked it again
				{
					if (iSheetFound)
					{
						Debug.Log("Trying to add " + item.name + " to inventory");
						associatedInventory.AddItem(item.transform); //We use the item.
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
							i.GetComponent(ItemEffect).UseEffect(); //It's not equipment so we just use the effect.
						}
					}
				}
				
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


//If we are dragging an item, we will clear it.
function ClearDraggedItem()
{
	itemBeingDragged=null;
}