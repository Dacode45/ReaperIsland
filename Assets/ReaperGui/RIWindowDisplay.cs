using UnityEngine;
using System.Collections;

//This Class is used to set up a window that displays buttons

public class RIWindowDisplay : MonoBehaviour {

		//GUI Locations
		public int guiMargin = 70;
		public int windowMargin = 15;

		//ToolBar Controls
		public string[] toolbarStrings;
		public bool toolBarDisplayed = false;
		public int toolbarInt = 0;
		public Vector2 toolbarSize = new Vector2(100,30);

		//All Gui Displays
		public static RIWindowDisplay[] menus;
		public static int menuCount;
		public static int currentMenu = 0;
		public bool drawGuiArea = false;

		//Variables for dragging:
		public static Item itemBeingDragged; //This refers to the 'Item' script when dragging.
		public Vector2 draggedItemPosition; //Where on the screen we are dragging our Item.
		public  Vector2 draggedItemSize;//The size of the item icon we are dragging.
		public Texture2D defaultIcon; // In case something needs to be dragged, and does not have an icon associated with it.

		//Variables for the window:
		public string windowName = "Master";
		public int windowId;
		public Vector2 windowSize = new Vector2(300, 500); //The size of the Inventory window.
		public bool useCustomPosition = false; //Do we want to use the customPosition variable to define where on the screen the Inventory window will appear?
		public Vector2 customPosition = new Vector2 (70, 400); // The custom position of the Inventory window.
		public static Vector2 itemIconSize = new Vector2(60.0f, 60.0f); //The size of the item icons.

		//Variables for updating the inventory
		public int updateListDelay = 9999;//This can be used to update the Inventory with a certain delay rather than updating it every time the OnGUI is called.
		//This is only useful if you are expanding on the Inventory System cause by default Inventory has a system for only updating when needed (when an item is added or removed).
		public float lastUpdate = 0.0f; //Last time we updated the display.
		public Transform[] UpdatedList; //The updated inventory array.

		//More variables for the window:
		static bool displayInventory = false; //If inv is opened.
		public bool displayThisInventory = false;
		public Rect windowRect = new Rect (0,0,108,130); //Keeping track of the Inventory window.
		public GUISkin invSkin; //This is where you can add a custom GUI skin or use the one included (InventorySkin) under the Resources folder.
		public Vector2 Offset = new Vector2 (7, 12); //This will leave so many pixels between the edge of the window (x = horizontal and y = vertical).
		public bool canBeDragged = true; //Can the Inventory window be dragged?

		public KeyCode onOffButton = KeyCode.K; //The button that turns the Inventory window on and off.

		public bool debugMode= false;

		public virtual void Awake() {
			if (useCustomPosition == false)
			{
				windowRect= new Rect(0,toolbarSize.y + windowMargin,windowSize.x,windowSize.y);
			}
			else
			{
				windowRect = new Rect (customPosition.x, customPosition.y, windowSize.x, windowSize.y);
			}
		if(defaultIcon == null) defaultIcon = Resources.Load("skull.png") as Texture2D;

		}

		public virtual void Start () {
			menus = GetComponents<RIWindowDisplay>();
			menuCount = menus.Length;
		Debug.Log("There are " + menuCount + " windows");
			toolbarStrings = new string[menuCount];
			//Syncs the toolbars and the toolbar ids. 
			for(int i = 0; i<menuCount; i++){
				toolbarStrings[i] = menus[i].name;
				menus[i].windowId = i;
			}
				

		}

		void Update () {
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
					toolBarDisplayed = false;
					drawGuiArea = false;
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

			//Updating the list by delay
			if(Time.time>lastUpdate){
				lastUpdate=Time.time+updateListDelay;
				UpdateInventoryList();
			}
		}

		 void OnGUI()
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


				toolbarInt = GUI.Toolbar (new Rect (0, 0, toolbarSize.x, toolbarSize.y), toolbarInt, toolbarStrings);
					
				
				if(windowId == toolbarInt){
					windowRect = GUI.Window (windowId, windowRect, DisplayInventoryWindow, windowName);
				}

			}
			
			
		}

		public void ClearDraggedItem()
		{
			itemBeingDragged=null;
		}

		//Needs to be overritten
		public virtual void DisplayInventoryWindow(int windowID){

		}

		public virtual void UpdateInventoryList()
		{
			
		}
} //END
