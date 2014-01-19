
using UnityEngine;
using System.Collections;

[AddComponentMenu("Inventory/RI Character Sheet")]
[RequireComponent(typeof(RInventory))]
public class RICharacter : RIWindowDisplay {

	public Transform WeaponSlot ; //This is where the Weapons are going to go (be parented too). In my case it's the "Melee" gameobject.
	
	private Item[] ArmorSlot ; //This is the built in Array that stores the Items equipped. You can change this to static if you want to access it from another script.
	public string[] ArmorSlotName; //This determines how many slots the character has (Head, Legs, Weapon and so on) and the text on each slot.
	public Rect[] buttonPositions; //This list will contain where all buttons, equipped or not will be and SHOULD HAVE THE SAME NUMBER OF cells as the ArmorSlot array.

	private RInventory playersinv; //Refers to the Inventory script.
	private bool equipmentEffectIs = false;
	private InvAudio invAudio;

	
	public override void Awake(){
		windowName = "Character Sheet";
		base.Awake();

		playersinv = GetComponent<RInventory>();

		invAudio = GetComponent<InvAudio>();

	}

	public void Start(){
		Debug.Log("Armor Slot name lenght" + ArmorSlotName.Length);
		ArmorSlot = new Item [ArmorSlotName.Length];
		if (buttonPositions.Length != ArmorSlotName.Length)
		{
			Debug.LogError("The variables on the Character script attached to " + transform.name + " are not set up correctly. There needs to be an equal amount of slots on 'ArmorSlotName' and 'buttonPositions'.");
		}
	}

	//Checking if we already have somthing equipped
	public bool CheckSlot(int tocheck)
	{
		bool toreturn=false;
		if(ArmorSlot[tocheck]!=null){
			toreturn=true;
		}
		return toreturn;
	}

	//Using the item. If we assign a slot, we already know where to equip it.
	public void UseItem(Item i,int slot, bool autoequip)
	{
		if(i.isEquipment){
			//This is in case we dbl click the item, it will auto equip it. REMEMBER TO MAKE THE ITEM TYPE AND THE SLOT YOU WANT IT TO BE EQUIPPED TO HAVE THE SAME NAME.
			if(autoequip)
			{
				int index=0; //Keeping track of where we are in the list.
				int equipto=0; //Keeping track of where we want to be.
				foreach(string a in ArmorSlotName) //Loop through all the named slots on the armorslots list
				{
					if(a==i.itemType) //if the name is the same as the armor type.
					{
						equipto=index; //We aim for that slot.
						break;
					}
					index++; //We move on to the next slot.
				}
				EquipItem(i,equipto);
			}
			else //If we dont auto equip it then it means we must of tried to equip it to a slot so we make sure the item can be equipped to that slot.
			{
				if(i.itemType==ArmorSlotName[slot]) //If types match.
				{
					EquipItem(i,slot); //Equip the item to the slot.
				}
			}
		}
		if (debugMode)
		{
			Debug.Log(i.name + " has been used");
		}
	}

	//Equip an item to a slot.
	public void EquipItem(Item i, int slot)
	{
		if(i.itemType == ArmorSlotName[slot]) //If the item can be equipped there:
		{
			if(CheckSlot(slot)) //If theres an item equipped to that slot we unequip it first:
			{
				UnequipItem(ArmorSlot[slot]);
				ArmorSlot[slot]=null;
			}
			ArmorSlot[slot]=i; //When we find the slot we set it to the item.
			
			SendMessage ("PlayEquipSound", SendMessageOptions.DontRequireReceiver); //Play sound
			
			//We tell the Item to handle EquipmentEffects (if any).
			if (i.GetComponent<EquipmentEffect>() != null)
			{
				equipmentEffectIs = true;
				i.GetComponent<EquipmentEffect>().EquipmentEffectToggle(equipmentEffectIs);
			}
			
			//If the item is also a weapon we call the PlaceWeapon function.
			if (i.isAlsoWeapon == true)
			{
				if (i.equippedWeaponVersion != null)
				{
					PlaceWeapon(i);
				}
				
				else 
				{
					Debug.LogError("Remember to assign the equip weapon variable!");
				}
			}
			if (debugMode)
			{
				Debug.Log(i.name + " has been equipped");
			}
			
			playersinv.RemoveItem(i.transform); //We remove the item from the inventory
		}
	}

	//Unequip an item.
	public void UnequipItem(Item i)
	{
		SendMessage ("PlayPickUpSound", SendMessageOptions.DontRequireReceiver); //Play sound
		
		//We tell the Item to disable EquipmentEffects (if any).
	
		equipmentEffectIs = false;
		i.GetComponent<EquipmentEffect>().EquipmentEffectToggle(equipmentEffectIs);

		
		//If it's a weapon we call the RemoveWeapon function.
		if (i.itemType == "Weapon")
		{
			RemoveWeapon(i);
		}
		if (debugMode)
		{
			Debug.Log(i.name + " has been unequipped");
		}
		playersinv.AddItem(i.transform);
	}

	//Places the weapon in the hand of the Player.
	public void PlaceWeapon(Item item)
	{
		Transform Clone = Instantiate (item.equippedWeaponVersion, WeaponSlot.position, WeaponSlot.rotation) as Transform;
		Clone.name = item.equippedWeaponVersion.name;
		Clone.transform.parent = WeaponSlot;
		if (debugMode)
		{
			Debug.Log(item.name + " has been placed as weapon");
		}
	}

	//Removes the weapon from the hand of the Player.
	public void RemoveWeapon (Item item)
	{	
			Destroy(WeaponSlot.FindChild(""+item.equippedWeaponVersion.name).gameObject);
			if (debugMode)
			{
				Debug.Log(item.name + " has been removed as weapon");
			}

	}

	public override void DisplayWindow(int windowID){
		if (canBeDragged == true)
		{
			GUI.DragWindow (new Rect (0,0, 10000, 30));  //The window is dragable.
		}
		
		int index=0;
		foreach(Item a in ArmorSlot)
		{
			if(a==null)
			{
				if(GUI.Button(buttonPositions[index], ArmorSlotName[index])) //If we click this button (that has no item equipped):
				{
					RIGuiWrapper id=GetComponent<RIGuiWrapper>();
					if(id.itemBeingDragged != null) //If we are dragging an item:
					{
						EquipItem(id.itemBeingDragged,index); //Equip the Item.
						id.ClearDraggedItem();//Stop dragging the item.
					}
				}
			}
			else
			{
				if(GUI.Button(buttonPositions[index],ArmorSlot[index].itemIcon)) //If we click this button (that has an item equipped):
				{

					RIGuiWrapper id2=GetComponent<RIGuiWrapper>();
					if(id2.itemBeingDragged != null) //If we are dragging an item:
					{
						EquipItem(id2.itemBeingDragged,index); //Equip the Item.
						id2.ClearDraggedItem(); //Stop dragging the item.
					}
					else if (playersinv.Contents.Length < playersinv.MaxContent) //If there is room in the inventory:
					{
						UnequipItem(ArmorSlot[index]); //Unequip the Item.
						ArmorSlot[index] = null; //Clear the slot.
						id2.ClearDraggedItem(); //Stop dragging the Item.
					}
					else if (debugMode)
					{
						Debug.Log("Could not unequip " + ArmorSlot[index].name + " since the inventory is full");
					}
				}
			}
			index++;
		}
	}


}
