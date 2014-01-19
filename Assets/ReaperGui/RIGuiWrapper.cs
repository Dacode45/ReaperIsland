using UnityEngine;
using System.Collections;

public class RIGuiWrapper : MonoBehaviour {

	//ToolBar Controls
	public string[] toolbarStrings;
	public bool toolBarDisplayed = false;
	public int toolbarInt = 0;
	public Vector2 toolbarSize = new Vector2(400,30);
	
	//All Gui Displays
	public static RIWindowDisplay[] menus;
	public static int menuCount;
	public static int currentMenu = 0;
	public bool drawGuiArea = false;

	//Variables for dragging:
	public Item itemBeingDragged; //This refers to the 'Item' script when dragging.
	public Vector2 draggedItemPosition; //Where on the screen we are dragging our Item.
	public Vector2 draggedItemSize;//The size of the item icon we are dragging.
	Texture2D defaultIcon; // In case something needs to be dragged, and does not have an icon associated with it.

	//For Displaying Inventory
	static bool displayInventory = false; //If inv is opened.
	public bool displayThisInventory = false;

	public KeyCode onOffButton = KeyCode.K; //The button that turns the Inventory window on and off.


	public void Start(){

		menus = GetComponents<RIWindowDisplay>();
		menuCount = menus.Length;
		Debug.Log("There are " + menuCount + " windows");
		toolbarStrings = new string[menuCount];
		//Syncs the toolbars and the toolbar ids. 
		for(int i = 0; i<menuCount; i++){
			toolbarStrings[i] = menus[i].windowName;
			menus[i].windowId = i;
		}
		Debug.Log("Toolbar Int is " + toolbarInt);
		itemBeingDragged = RIWindowDisplay.itemBeingDragged;
		draggedItemPosition = RIWindowDisplay.draggedItemPosition;
		draggedItemSize = RIWindowDisplay.draggedItemSize;
		defaultIcon = RIWindowDisplay.defaultIcon;
	}

	public void Update(){
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
				SendMessage ("ChangedState", false, SendMessageOptions.DontRequireReceiver);
				SendMessage("PauseGame", false, SendMessageOptions.DontRequireReceiver); //StopPauseGame/EnableMouse/ShowMouse
			}
			else
			{
				displayInventory = true;
				
				SendMessage ("ChangedState", true, SendMessageOptions.DontRequireReceiver);
				SendMessage("PauseGame", true, SendMessageOptions.DontRequireReceiver); //PauseGame/DisableMouse/HideMouse
			}
		}
		
		//Making the dragged icon update its position
		if(itemBeingDragged!=null)
		{
			//Give it a 15 pixel space from the mouse pointer to allow the Player to click stuff and not hit the button we are dragging.
			draggedItemPosition.y=Screen.height-Input.mousePosition.y+15;
			draggedItemPosition.x=Input.mousePosition.x+15;
		}

	}

	public void  OnGUI()
	{
		//GUI.skin = invSkin; //Use the invSkin

		if(itemBeingDragged != null) //If we are dragging an Item, draw the button on top:
		{
			GUI.depth = 3;
			GUI.Button(new Rect(draggedItemPosition.x,draggedItemPosition.y,draggedItemSize.x,draggedItemSize.y),itemBeingDragged.itemIcon);
			GUI.depth = 0;
		}

		//If the inventory is opened up we create the Inventory window:
		if(displayInventory)
		{
			
			toolbarInt = GUILayout.Toolbar ( toolbarInt, toolbarStrings);

			menus[toolbarInt].windowRect = GUI.Window (menus[toolbarInt].windowId, menus[toolbarInt].windowRect, menus[toolbarInt].DisplayWindow, menus[toolbarInt].windowName);

		}
		
		
	}

	public void ClearDraggedItem()
	{
		itemBeingDragged=null;
	}
}
