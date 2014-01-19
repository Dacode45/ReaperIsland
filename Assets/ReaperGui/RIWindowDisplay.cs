using UnityEngine;
using System.Collections;

//This Class is used to set up a window that displays buttons
[RequireComponent(typeof(RIGuiWrapper))]
public class RIWindowDisplay : MonoBehaviour {
		
		//GUI Wrapper
		public RIGuiWrapper guiWrapper;

		//GUI Locations
		public int guiMargin = 70;
		public int windowMargin = 15;

		//Variables for dragging:
		public static Item itemBeingDragged; //This refers to the 'Item' script when dragging.
		public static Vector2 draggedItemPosition; //Where on the screen we are dragging our Item.
		public static Vector2 draggedItemSize;//The size of the item icon we are dragging.
		public static Texture2D defaultIcon; // In case something needs to be dragged, and does not have an icon associated with it.

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
		public Rect windowRect = new Rect (0,0,108,130); //Keeping track of the Inventory window.
		public GUISkin invSkin; //This is where you can add a custom GUI skin or use the one included (InventorySkin) under the Resources folder.
		public Vector2 Offset = new Vector2 (7, 12); //This will leave so many pixels between the edge of the window (x = horizontal and y = vertical).
		public bool canBeDragged = true; //Can the Inventory window be dragged?

		
		public bool debugMode= false;

		public virtual void Awake() {
			if (useCustomPosition == false)
			{
				windowRect= new Rect(0, windowMargin,windowSize.x,windowSize.y);
			}
			else
			{
				windowRect = new Rect (customPosition.x, customPosition.y, windowSize.x, windowSize.y);
			}
		if(defaultIcon == null) defaultIcon = Resources.Load("skull.png") as Texture2D;

		guiWrapper = GetComponent<RIGuiWrapper>();

		}

		

		void Update () {

			//Updating the list by delay
			if(Time.time>lastUpdate){
				lastUpdate=Time.time+updateListDelay;
				UpdateInventoryList();
			}
		}

		

		public void ClearDraggedItem()
		{
			guiWrapper.itemBeingDragged=null;
		}

		//Needs to be overritten
		public virtual void DisplayWindow(int windowID){
			
		}

		public virtual void UpdateInventoryList()
		{
			
		}


} //END
