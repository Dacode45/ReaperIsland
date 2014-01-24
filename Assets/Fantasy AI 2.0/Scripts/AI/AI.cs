using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    public class AI : MonoBehaviour {
	
	//SOME OF THE MAIN COMPONENTS ABLE TO BE DISABLED
	public bool DisableCombatBehavor;
	public bool DisableEnemyChoosing;
	public bool DisableObstacleAvoidance;
	public bool DisableRespawn;
	
	
	//CHARACTER MODEL
	public GameObject charactermodel;
	public Transform headbone;
	public Transform neckbone;
	public Transform facefront;
	public Transform headcontroller;
	private Vector3 currentlook;
	private Transform lookfriend;
	private float looktime;
	private float playerdirection;
	

	//LAYERS AND TAGS
	public string charactertag="character";
	public int waypointcollisionlayer=8;
	public int aicollisionlayer=9;
	
	
	//ANIMATIONS
	public List<AnimationClip> Attackanimations;
	public AnimationClip run;
	private int attackran;
	public AnimationClip attackstand;
	public AnimationClip death;
	public AnimationClip walk;
	public AnimationClip stand;
	

	//ANIMATION SPEEDS
	public float deathanimationspeed=1.5f;
	public float runanimationspeed=0.9f;
	public float walkanimationspeed=0.8f;
	public float idleanimationspeed=4;
	
	
	//IDLE SOUNDS
	public List<AudioClip> idlesounds;
	private int idlesoundrandom;
	
	//ALERTED SOUNDS
	public List<AudioClip> alertsounds;
	private int alertsoundrandom;
	private float alerttimer;
	
	
	//SPOT SOUNDS
	public List<AudioClip> spotsounds;
	private int spotsoundrandom;
	private float spottimer;
	
	
	
	//HUNT SOUNDS
	public List<AudioClip> huntsounds;
	private int huntsoundrandom;
	private float huntsoundtimer;
	
	//RETREAT SOUNDS
	public List<AudioClip> retreatsounds;
	private int retreatsoundrandom;
	private bool retreatsound;
	
	//ASK HELP SOUNDS
	public List<AudioClip> askhelpsounds;
	private int askhelpsoundrandom;
	
	//RESPOND SOUNDS
	public List<AudioClip> respondsounds;
	private int respondsoundrandom;
	
	//DEATH SOUNDS
	public List<AudioClip> deathsounds;
	private int deathsoundrandom;
	private bool deathsound=false;
	

	//SOUND FX
	public AudioClip swing;
	public AudioClip hit;
	
	//SOUNDS
	public bool enablesounds;
	public bool enableidlesounds=true;
	public bool enablechargesounds=true;
	public bool enablehuntsounds=true;
	public bool enableretreatsounds=true;
	public bool enabledeathsounds=true;
	
	public float playidlesoundevery=8;
	private float idlesoundtimer;
	private int randomgeneratore;
	
	public bool spotsound;
	public bool alertsound;
	public bool Dialogrespond;

	
	//DIALOG RESPOND
	private bool Dialogaskhelp;
	private bool airespond;
	
	//TARGETS
    public Vector3 currenttarget;
	private Transform targsave;
    public Transform enemy;
	private bool hostile;
	private int ce;
	private int e;
	
	
	//GOTO TARGET MODES
	public bool gototarget;
	private bool gototargetsave;
	
	//GOTO TARGET MODE
	public bool gototargetnostop;
	private bool targetpresent;
	
	//THE TARGET HE GOES TO
	public Transform targetlocateto;
	
	//RUN OR WALK?
	public bool runtotarget;
	public bool walktotarget=true;
	
	
	//COMPANION FEATURE
	public bool companion;
	public Transform companionleader;
	public float companionstartdistance=20;
	public float companionstopdistance=10;
	
	
	public float giveupchargetime=10;
	public float giveuphunttime=10;
	private float givetimer;
	private float hunttimer;
	

	//HEALTH SAVE
	public int health;

	
	//TARGET DISTANCES
	private float targdist;
	private float respondtime;
	
	//UNIT LIST
	public List<Transform> unitlist;
	//ENEMY LIST
	public List<Transform> enemys;
	//FRIENDLY LIST
	public List<Transform> friendly;
	private int listsize;
	//DYNAMIC TEAM SAVE
	private bool dynamicteams;

	
	
	//CURRENT TEAM
	private int team;
	//A CHECK FOR ENEMYS TO POPULATE ENEMY LIST
	private bool enemylistscan;
	public bool enemydead;
	public Transform eyespot;
	
	//POSITION THE RAYCAST WILL CAST ON HIS ENEMY
	public float runspeed = 8;
	public float walkspeed = 4;
	public float turnspeed=8;
	public float avoidturnspeed=8;

	
	//CHARACTER AVOIDANCE
	private bool avoidcharacter;
	private float avoidtimer;
	private Transform charactertoavoid;
	private float avoidtime;
	
	//WAYPOINT DETECT
	private bool waydetect;
	
	//LINE OF SIGHT
	public Transform aieye;
	public float viewdistance=20;
	public int awarnesspercent=10;
	private int awarnesssave;
	public bool viewob;
	public bool showray;
	
	//public Transform playeye;
	//public Transform playeye2;
	
	//SOME BASIC STATS
	public float reacttimeseconds=1f;
	private float reacttimer;
	//MELEE DAMAGE AMOUNT(THIS SHOULD BE SET TO 0 IF THE AI IS RANGED!)
	public int damage=5;
	public float attackrange=8;
	public float attackspeed=1.5f;
	public float attacktimer;
	private bool attacktimestart;
	private float awaretimer;
	private bool awareboost;
	
	//WHEN DAMAGE IS DEALT
	private bool dealdamage;
	private bool damagedealt;
	
	//COWARD MUTIPLY WORKS BY HOW MANY TIMES HIS OPPONENT/OPPONENTS ARE BETTER THAN HIM.(by adding health and damage of apponent)
	
	
	//ODD CALCULATIONS USED FOR RETREAT
	private float odds;
	private float oddsper;
	
	//TALLY UP OF ODDS
	private int oddcounter;
	private int apponamount;
	private int oddsave;
	private float retreattime;
	public bool findhelponretreat;
	public float cowardmultiply=2;
	private float cowardmultsave;
	
	//LENGTH IN TIME FOR RETREAT
	public float retreatlength=4;
	private float suspcioustimer;
	
	//no available friends
	private int friendsearch;
	
	//LOCATION VECTOR OF TARGET TO GOTO
	public Vector3 locationvector;
	
	//THE DISTANCE THE AI STOPS WHEN REACHING TARGET
	public float targetdistancestop=7;
	
	//STANDAR BEHAVROL STATES
	public bool changestate;
	public bool idle;
	public bool charge;
	public bool runn;
	public bool walkk;
	public bool stopwalking;
	public bool stoprunning;
	public bool navigate;
	private bool chargedisable;
	public bool retreat;
	private bool friendsearchcycledone;
	public bool hunt;
	public bool attackstance;
	public bool attack;
	public bool dead;
	private bool deadanim;
	public bool respawn;
	
	
	//VECTOR RESPAWN POSTITION
	public Vector3 respawnposition;
	//TARGET RESPAWN POSITION
	public Transform respawntarget;
	//TIME IT TAKES TO RESPAWN
	
	public float respawntime=10;
	
	//SAVES START POSITION
	public Vector3 startposition;
	
	//RESPAWN TIMER
	private float respawntimer;
	
	//WILL RESPAWN AT HIS START POSITION
	public bool respawnatstartposition;
	
	//timer for death
	private float deadtime;
	private int framsave;
	
	
	
// Use this for initialization
	void Start () {
		if(headbone&headcontroller&neckbone){
			headbone.parent=headcontroller;
	headcontroller.parent=neckbone;
			facefront.parent=transform;
		}
		
		
		if(charactermodel){}
		else charactermodel=gameObject;
		
		
		//enable death animation for when it comes
		deadanim=true;
		
		
		//GET START SAVE POSITION
		startposition=transform.position;
		
		//change location vector to targetlocatto if it exists
			if(targetlocateto) locationvector=targetlocateto.transform.position;
		

		//check for audio
		if(audio){}
		else enablesounds=false;
		//check to see if the EYE has been assigned to the AI
		if(aieye){}
		else aieye=transform;
		
		
		//get team info from team script if attached
		team myteam=(team)GetComponent("team");	
			if(myteam){
				team=myteam.teamnumber;
			if(myteam.dynamicteams)dynamicteams=true;
			else dynamicteams=false;
			if(myteam.hostile) hostile=true;
					else hostile=false;
					}
		

		
		deathsound=true;
		randomgeneratore=1;
		idle=true;
		awarnesssave=awarnesspercent;
		cowardmultsave=cowardmultiply;
		if(hostile) ce=1;
		enemylistscan=true;
		dealdamage=true;

	if(enemy) currenttarget=enemy.transform.position;	
		
		
		
	//LETS CHECK FOR UNITS and add them to the list	
AddAllUnits();
	}
	public void AddAllUnits()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag(charactertag);
		foreach(GameObject unitts in go)
			Addtarget(unitts.transform);
		}
	
	public void Addtarget(Transform unitts)
	{
	unitlist.Add(unitts);

	}

	
	// Update is called once per frame
	void Update () {
		//START RANDOM NUMBER GENERATORS
		
		if(changestate){
		gototarget=false; gototargetnostop=false; targetlocateto=null; runn=false; walkk=false; enemy=null; enemydead=true;  companion=false; companionleader=null;
		idle=false; hunt=false; charge=false; attack=false; retreat=false; attackstance=false; 	
			
			changestate=false;
		}
		else{
		//IDLE SOUNDS GENERATOR
		idlesoundrandom=idlesoundrandom+1; 
		if(idlesoundrandom>=idlesounds.Count)idlesoundrandom=0;
		
		//ALERT SOUND RANDOM GENERATOR
		alertsoundrandom=alertsoundrandom+1;
		if(alertsoundrandom>=alertsounds.Count)alertsoundrandom=0;
		
		//SPOTTED SOUNDS GENERATOR
		spotsoundrandom=spotsoundrandom+1;
		if(spotsoundrandom>=spotsounds.Count)spotsoundrandom=0;
		
		//HUNT SOUNDS GENERATOR
		huntsoundrandom=huntsoundrandom+1; 
		if(huntsoundrandom>=huntsounds.Count)huntsoundrandom=0;
		
		//RETREAT SOUNDS GENERATOR
		retreatsoundrandom=retreatsoundrandom+1; 
		if(retreatsoundrandom>=retreatsounds.Count)retreatsoundrandom=0;
		
		//ASK HELP SOUNDS GENERATOR
		askhelpsoundrandom=askhelpsoundrandom+1; 
		if(askhelpsoundrandom>=askhelpsounds.Count)askhelpsoundrandom=0;
		
		//RESPOND SOUNDS GENERATOR
		respondsoundrandom=respondsoundrandom+1; 
		if(respondsoundrandom>=respondsounds.Count)respondsoundrandom=0;
		
		//DEATH SOUNDS GENERATOR
		deathsoundrandom=deathsoundrandom+1; 
		if(deathsoundrandom>=deathsounds.Count)deathsoundrandom=0;
		
			if(randomgeneratore>=3)randomgeneratore=1;
			else randomgeneratore=randomgeneratore+1;	
		
		//RANDOM ATTACK ANIMATIONS GENERATOR
		if(Attackanimations.Count>0){
		attackran=attackran+1;
		if(attackran>=Attackanimations.Count)attackran=0;
		}
		
		
		//if there is a target assigned, the location vector=targetlocateto
		if(targetlocateto) locationvector=targetlocateto.transform.position;

		//RESPAWN 
	if(DisableRespawn){}
			else{
			//check if respawn at start position is enabled
			if(respawnatstartposition){
			respawnposition=startposition;
			}
			else{
				if(respawntarget)respawnposition=respawntarget.position;
				else respawnposition=transform.position;
			}
			
			if(DisableRespawn){}
			else{
				//IF RESPAWN IS ENABLED AND THE RESPAWN TIMER IS UP RESPAWN!
		if(respawn){
					
				respawntimer+=Time.deltaTime;
			if(respawntimer>respawntime){
				health hpres=(health)GetComponent("health");
				if(hpres){
							deadtime=0;
							deadanim=true;
					collider.enabled = true;
						collider.isTrigger=false;
				     rigidbody.isKinematic=false;
					if(targetlocateto)gototarget=true;
					transform.position=respawnposition;
					hpres.currenthealth=hpres.maxhealth;
					hpres.dead=false;
					dead=false;
						
						respawntimer=0;
						respawn=false;
				}
			}
		}
		}
		}
	
		//DEATH
		if(dead){
			charactermodel.animation.wrapMode = WrapMode.Once;

		
				attack=false; charge=false; hunt=false; attackstance=false; runn=false;
			if(DisableRespawn){
				
			}
			else{
			respawn=true;
			
			}
			
			//disable collision on death
		
			if(rigidbody){
			collider.enabled=false;
			rigidbody.isKinematic=true;
			}
			
			if(enablesounds){
			if(deathsound){

					if(deathsounds.Count>0){
				audio.clip=deathsounds[deathsoundrandom];
				
				audio.Play();
					}
					
					deathsound=false;
				}
			}
			
			deadtime+=Time.deltaTime;
		if(death){
				if(deadanim){
		charactermodel.animation[death.name].speed = charactermodel.animation[death.name].length / deathanimationspeed;
			charactermodel.animation.CrossFade( death.name);	
				deadanim=false;
				
				}
				
			}	
		}
		else{
			
	
			
	//HEAD TURNING
		if(headbone&headcontroller&neckbone){
				
				
					
				
				
					if(enemy){	
					
					Vector3 pdir = (enemy.transform.position - transform.position).normalized;
		playerdirection = Vector3.Dot(pdir, transform.forward);
				
					if(playerdirection>0.5f){	
						if(viewob){
						currentlook=facefront.transform.position;	
						}
					else{
						if(eyespot)	currentlook=eyespot.transform.position;
						}
				}
					else currentlook=facefront.transform.position;
					
					}
				
					if(enemydead){
					if(lookfriend){
						Vector3 pdir = (lookfriend.transform.position - transform.position).normalized;
		playerdirection = Vector3.Dot(pdir, transform.forward);
						if(playerdirection>0.5f){
						currentlook=lookfriend.transform.position;
					}
						else currentlook=facefront.transform.position;
					}
					else currentlook=facefront.transform.position;
				}
				
	
	headcontroller.transform.rotation = Quaternion.Slerp(headcontroller.transform.rotation, Quaternion.LookRotation(headcontroller.transform.position - currentlook), 12 * Time.deltaTime);	
			}		
	
			
			
		//WHO ARE HIS ENEMYS?
			//lets figure that out!
		listsize=unitlist.Count;

		if(enemylistscan){
		for (int i = 0; i < listsize; i++)
{		
					//get the team scripts from every unit if they exist
		team ais=(team)unitlist[i].GetComponent("team");
					if(ais){
						if(ais.transform==transform){}
					else{
		if(hostile){
			enemys.Add(ais.transform);				
						}
							
						
					if(ais.hostile){
						enemys.Add(ais.transform);	
							
						} 
						else if(ais.teamnumber==team){
								AI ais2=(AI)unitlist[i].GetComponent("AI");		
						if(ais2){
								ais2.friendly.Add(transform);
								}	
									friendly.Add(ais.transform);
								}

			//add enemys to enemy list			
			if(ais.teamnumber>team|ais.teamnumber<team){
						enemys.Add(ais.transform);	
						AI ais2=(AI)unitlist[i].GetComponent("AI");		
						if(ais2){
								ais2.enemys.Add(transform);
								}	
						}
						}
}
					

			if(hostile){
				}
			}
				//end the enemy scan
							enemylistscan=false;	
}
				
		
		//DYNAMIC TEAMS(the dynamic teams feature is active when "dynamic teams" is checked off.  it enables the ability to switch teams during gameplay
			if(dynamicteams){
				team myteam=(team)GetComponent("team");
				
				if(myteam.teamnumber>team|myteam.teamnumber<team){
					ce=0;
					
					enemys.Clear();
					friendly.Clear();

					for (int i = 0; i < listsize; i++){
					AI ais=(AI)unitlist[i].GetComponent("AI");
						if(ais){
						//change if hes on the same team
							if(myteam.teamnumber==ais.team){
								ce=0;
							ais.enemys.Remove(transform);	
								ais.friendly.Add(transform);
								if(ais.enemy=transform) ais.enemy=null;
								ais.charge=false; ais.attack=false; ais.hunt=true;
							}
							
					if(myteam.teamnumber>ais.team|myteam.teamnumber<ais.team|myteam.hostile){
						if(ais.enemy=transform) ais.enemy=null;
								ais.charge=false; ais.attack=false; ais.hunt=true;
						team=myteam.teamnumber;
					ais.enemys.Add(transform);
					ais.friendly.Remove(transform);
						}
						enemys.Clear();
					friendly.Clear();
					enemylistscan=true;
					team=myteam.teamnumber;
				}
				}
				if(hostile){
					
				}
				else{
			if(myteam.hostile){
					enemys.Clear();
					friendly.Clear();
					for (int i = 0; i < listsize; i++){
					AI ais=(AI)unitlist[i].GetComponent("AI");
						if(ais.enemy=transform) ais.enemy=null;
								ais.charge=false; ais.attack=false; ais.hunt=true;
						team=myteam.teamnumber;
					ais.enemys.Add(transform);
					ais.friendly.Remove(transform);
						}
						
						enemys.Clear();
					friendly.Clear();
					enemylistscan=true;
					hostile=true;
				}
				}
			}
			}

			//TIME TO CHOOSE THE CLOSEST ENEMY SEEN
			if(enemylistscan){}
			else{
			if(DisableEnemyChoosing){}
			else{

			
			RaycastHit hitt = new RaycastHit();

		LayerMask layyy=aicollisionlayer;
			if(enemys.Count>0){
				if(hostile){
					if(ce>=enemys.Count)ce=1;
				}
				else if(ce>=enemys.Count)ce=0;
							
			health choose=(health)enemys[ce].GetComponent("health");
			

			if(choose){
								//raycast to check if he can see him
		if(choose.dead|Physics.Linecast(aieye.position, choose.transform.position, out hitt, layyy)){
				
				ce=ce+1;
				if(hostile){
					if(ce>=enemys.Count)ce=1;
				}
				else if(ce>=enemys.Count)ce=0;
					
				}
			else{
               
			 e=ce;
				enemydead=false;
				enemy=enemys[e];
				if(hostile)ce=1;
				else{ 
				 ce=0;
								}
			}
			}
			}
			}
			}
			
			//organize enemys closest to farthest if framesave counter is less than 1
			if(DisableEnemyChoosing){}
			else{
				framsave=framsave+1;
				if(framsave<=1){
				
		enemys.Sort(delegate(Transform p2, Transform p1) { 
return Vector3.Distance(p2.position, transform.position).CompareTo(Vector3.Distance(p1.position, transform.position));	
	
			});

				}
				//HOW OFTEN ENEMY LIST IS ORGANIZED FOR DIFFERENT STATES
				//during idle state
				if(idle){
				if(framsave>100) framsave=0;
				}
				//during attack state
				if(attack){
				if(framsave>50){
						
						framsave=0;
					}
				}
				//during hunt state
				if(hunt){
				if(framsave>21) framsave=0;
				}
				//during charge state
				if(charge){
					attackran=0;
				if(framsave>15) framsave=0;
				}
				
				
			}
			
			//IF THERE IS NO ENEMY TARGET ENEMY DEAD IS TRUE
			
				if(enemy){}
			else enemydead=true;
				
			
			//check if enemy becomes friendly
			if(enemy){
			team enteam=(team)enemy.GetComponent("team");
			if(enteam.teamnumber==team){}
				else{
				}
			}
			
				//COMBAT BEHAVOR STARTS
			if(DisableCombatBehavor){}
			else{
					
				//GIVE UP TIMERS FOR HUNT AND CHARGE
					
					//GIVE UP CHARGE AFTER LOST SIGHT
				if(gototargetnostop){}
				else{
		if(charge){
							if(viewob){
							hunttimer+=Time.deltaTime;	
							if(hunttimer>=giveupchargetime){	
								charge=false;
									hunt=true;
								hunttimer=0;
								}
							}
						else hunttimer=0;
						}
						
					//GIVE UP HUNT AFTER LOST SIGHT
		if(hunt){
						
							if(viewob){
								givetimer+=Time.deltaTime;	
							if(givetimer>=giveuphunttime){	
								hunt=false;
									enemy=null;
								runn=false;
								if(targetlocateto)gototarget=true;
								else idle=true;
								
								givetimer=0;
								}
							}
						else givetimer=0;
						}
						
						
						//IDLE WILL BE FALSE IF ANYTHING ELSE IS HAPPENING
				if(charge|attack|attackstance|retreat|hunt|gototargetnostop|gototarget)idle=false;
				else{
								if(targetlocateto)gototarget=true;
								else idle=true;
							}
					
					if(enemy){
						
			//GET ENEMY COMPONENTS		
			if(enemy.gameObject.tag == charactertag){
			AI targ=(AI)enemy.GetComponent("AI");
					
							if(targ){
							if(targ.dead)enemydead=true;
							else enemydead=false;
							}
								
							//check if the enemy has an 'eye' object assigned
				if(targ){
						eyespot=targ.aieye;
						
					}
					else{ eyespot=enemy.transform;
								
							}
			}
				else{ eyespot=enemy.transform;
				
						}
				
				//distance and direction of enemy
		targdist = Vector3.Distance(transform.position, enemy.transform.position);
		Vector3 pdir = (enemy.transform.position - transform.position).normalized;
		float playerdirection = Vector3.Dot(pdir, transform.forward);
				

	
				//HUNT if enemy is nearby
				if(charge|attack|attackstance|retreat){
					hunt=false;
				}
				else{
							if(enemydead){}
							else{
		if(targdist<(viewdistance*awarnesspercent/100*0.9f)){
					if(attack){}
									else hunt=true;
					
									gototarget=false;
					walkk=false;
					idle=false;
								}
				}
				}
		//if sees enemy or he is alerted
			if(targdist<viewdistance&&playerdirection > 0|targdist<(viewdistance*awarnesspercent/100*0.2f)){}
						else{
								if(attackstance){
					if(targetlocateto)gototarget=true;
					else idle=true;
					attackstance=false;
										}
							viewob=true;
							attack=false;
					reacttimer=0;
						}
			
				//TARGET IN RANGE AND IN FRONT
		if(targdist<viewdistance&&playerdirection > 0|targdist<(viewdistance*awarnesspercent/100*0.2f)){
		RaycastHit hit = new RaycastHit();	
		LayerMask lay=aicollisionlayer;
				

					//calculate odds for retreat
					
					oddsper=odds/(damage+health);
					if(oddsper>=cowardmultiply){
					retreat=true;
					gototarget=false;
								walkk=false;
					attackstance=false;
					attack=false;
					idle=false;
					}
						else retreat=false;
					if(retreat){
						
					}
					else{
								
		//NOW WE CHECK IF THE VIEW IS OBSTRUCTED WITH RAYCASTING	
		if(Physics.Linecast(aieye.position, eyespot.position, out hit, lay))
								
    {
        viewob=true;	
					if(attackstance){
					if(targetlocateto)gototarget=true;
					else idle=true;
					attackstance=false;
										}
					attack=false;
					reacttimer=0;
		}
		else{
						
		//get enemys health componenet 
		health enemyy=(health)enemy.GetComponent("health");
									
		//check if he is dead						
		if(enemyy){
		if(enemyy.dead){
											retreattime=retreatlength;

				//IF THE ENEMY IS DEAD AND THE AI HAS A TARGET, CONTINUE TO TARGET						
            if(targetlocateto)gototarget=true;							
			attackstance=false;
			attack=false;
			charge=false;
				}			
				else{			
						
				//ai can now see the enemy							
			viewob=false;
											
					//checkreacttime
					reacttimer+=Time.deltaTime;
		if(showray)Debug.DrawLine(aieye.position, eyespot.transform.position, Color.red);
					float targdis = Vector3.Distance(transform.position, enemy.position);
					                       //MAKE SURE UNIT DOESNT TRY TO ATTACK ONCE OUT OF RANGE
											if(targdis>=attackrange*(attackrange)){
												//chargedisable=false;
												//attacktimer=0;
												//attack=false;
											}
											
					//if target is in attack range, attack. ATTACK RANGE IS MULTIPLIED BY .99 TO MAKE IT MORE CHALLENGING FOR PLAYERS AND APPONENTS TO AVOID ATTACKS
					if(targdis<attackrange*0.99){
					if(reacttimer>reacttimeseconds){
						attack=true;
						gototarget=false;
													walkk=false;
				         attackstance=false;
						charge=false;
						}
					}
					else{
											
					if(reacttimer<reacttimeseconds){
							if(charge){}
							else{
											gototarget=false;
											//hunt=true;
												if(viewob){}
											else attackstance=true;
											idle=false;
										}
						}
						else{
			attackstance=false;
				if(chargedisable){}
										else{
			attack=false;
			gototarget=false;
			walkk=false;
			charge=true;
										}
					}
												
					}
											
		}
		}
		}
			}
			}
			}
			}
			}
				//THE BEHAVE TREE ENDS	
		

			//NOW LETS DEFINE EXACLTY WHAT HAPPENS IN THESE BEHAVORS
			
			
			//COMPANION feature
				if(companion){
				if(companionleader){}
					else Debug.Log("You have enabled companion without assigning a leader..");
				}
				
			if(companion&companionleader){
			//	lookfriend=companionleader;
				
				AI eye=(AI)companionleader.GetComponent("AI");
									if(eye){
									if(eye.aieye)lookfriend=eye.aieye;
							else lookfriend=companionleader.transform;
									}
				
				float comdistance=Vector3.Distance(transform.position, companionleader.transform.position);
				if(enemy){
					float endist=Vector3.Distance(enemy.transform.position, companionleader.transform.position);
					if(endist>=companionstartdistance){
						charge=false;
						
						
							
						
					}
					else{
							
						gototarget=false;
						runn=false;
					}
				}
				
				if(comdistance>=companionstartdistance){
					
					attack=false;
					walktotarget=false;
					runtotarget=true;
					
					idle=false;
					gototargetnostop=true;
					targetlocateto=companionleader;
					
				}
				else {
					if(comdistance>=companionstopdistance&comdistance<=companionstartdistance){
					
							
						gototargetnostop=false;
						if(hunt|attack|charge|attackstance){
							if(enemydead){}
							else{
							runn=false;
							targetlocateto=null;
							gototarget=false;
							walkk=false;
							}
						}
						else{
						
								
						//gototarget=false;
						targetlocateto=companionleader;	
							if(comdistance>=companionstopdistance*1.65){
								walktotarget=false;
								runtotarget=true;
							}
							else{
								runtotarget=false;
								walktotarget=true;
							}
							
							
						gototarget=true;
						}
						//runtotarget=false;
					
				}
					else if(comdistance<=companionstopdistance) {
					walktotarget=false;
						runtotarget=true;
					gototarget=false;
						
						gototargetnostop=false;
					targetlocateto=null;
						if(enemy){}else idle=true;
					}
				}
				}

			
			//gototarget state
			if(gototargetnostop|gototarget){
					
				if(stoprunning){}
							else{
					if(walktotarget)walkk=true;
					if(runtotarget)runn=true;
				hunt=false;
				}

					idle=false;
					if(rigidbody)rigidbody.isKinematic=false;
				if(targetlocateto){
				float comdistance=Vector3.Distance(transform.position, targetlocateto.transform.position);
				
				if(companion){}
				else{
					if(comdistance<=targetdistancestop){
							odds=0;
				oddsave=0;
							
						gototarget=false;
						gototargetnostop=false;
						runn=false;
						walkk=false;
						if(findhelponretreat&retreat){
						AI fsearch=(AI)friendly[friendsearch].GetComponent("AI");
							Dialogaskhelp=true;
							charge=true;
							targetlocateto=enemy;
							if(fsearch){
								fsearch.Dialogrespond=true;
								fsearch.gototarget=true;
							    fsearch.runtotarget=true;
								fsearch.walktotarget=false;
								fsearch.targetlocateto=enemy;
							}
								retreat=false;
								findhelponretreat=false;
						}
						else{
								
							if(enemydead){
								idle=true;
							}
							else{ 
									if(attack){}
									else hunt=true;
								}
						}
						
						}
					}
				}
				
				if(targetlocateto){
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currenttarget - transform.position), turnspeed * Time.deltaTime);
					
				}
			}
			
			
			//GENERIC STATES

			//IDLE
			if(idle){
				
if(stand){
					
		charactermodel.animation[stand.name].speed = charactermodel.animation[stand.name].length / idleanimationspeed;
			
			charactermodel.animation.CrossFade( stand.name, 0.3f);	
			}
			}
			
			
		
			//THE RUNNING STATE BECOMES ACTIVE DURING THESE STATES
			if(charge|retreat|runtotarget&gototarget|gototargetnostop)if(stoprunning){} else runn=true;
			else runn=false;
			
			
			if(charge|retreat|runtotarget)walkk=false;
			
			
			//RUN STATE!!
			if(stoprunning){}
			else{
			if(runn){
				if(rigidbody)rigidbody.isKinematic=false;
				transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);	
	transform.position += transform.forward * runspeed * Time.deltaTime;
				if(run){
						
		charactermodel.animation[run.name].speed = charactermodel.animation[run.name].length / runanimationspeed;
			
							charactermodel.animation.CrossFade( run.name, 0.1f);	
			}
				}		
			}
			
			
	//WALK STATE!!
			if(stoprunning){}
			else{
			if(walkk){
				if(rigidbody)rigidbody.isKinematic=false;
				transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);	
	transform.position += transform.forward * walkspeed * Time.deltaTime;
				if(walkk){
					
		charactermodel.animation[walk.name].speed = charactermodel.animation[walk.name].length / walkanimationspeed;
			charactermodel.animation.CrossFade( walk.name, 0.15f);	
			}
				}		
			}		
			
	
	//CHARGE AT ENEMY 	
		if(enemy){
		
	if(charge){
					
					idle=false;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currenttarget - transform.position), turnspeed * Time.deltaTime);
					
				}
			}
			
			

			//HUNT
			
			//MOSTLY ALOT OF SMART LOOKING MOVEMENT AND ANIMATIONS FOR THIS STATE
			if(hunt){
				
				idle=false;
				if(rigidbody)rigidbody.isKinematic=false;
				suspcioustimer+=Time.deltaTime;
				if(suspcioustimer<1&suspcioustimer>0.87f){
				
				if(run){
						
		charactermodel.animation[run.name].speed = charactermodel.animation[run.name].length / 0.9f;
			charactermodel.animation.CrossFade( run.name);	
			}
				}
				if(suspcioustimer<1.66&suspcioustimer>1.35f){
			
				
				if(run){
						
		charactermodel.animation[run.name].speed = charactermodel.animation[run.name].length / 0.9f;
			charactermodel.animation.CrossFade( run.name);	
			}
				}
				
				if(suspcioustimer>3){
					if(suspcioustimer<3.3f){
						if(enemy){
							
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currenttarget - transform.position), turnspeed * Time.deltaTime);
						
						}
					}
					
			transform.position += transform.forward * runspeed * Time.deltaTime;	
					if(suspcioustimer>3.8f)suspcioustimer=0;
					if(run){
						
		charactermodel.animation[run.name].speed = charactermodel.animation[run.name].length / 0.9f;
			charactermodel.animation.CrossFade( run.name);	
			}

				}

				else{
				
				if(attackstand){
						
		charactermodel.animation[attackstand.name].speed = charactermodel.animation[attackstand.name].length / 1f;
			charactermodel.animation.CrossFade( attackstand.name);	
						}
					
			}
			}
			

		//reset odds if gototarget nonstop		
			if(gototargetnostop){
				if(walktotarget)walkk=true;
				if(runtotarget)runn=true;
				
				odds=0;
				oddsave=0;
			}	
			
			//RETREAT
			if(retreat){
				
				if(findhelponretreat){
					friendly.Sort(delegate(Transform p2, Transform p1) { 
return Vector3.Distance(p2.position, transform.position).CompareTo(Vector3.Distance(p1.position, transform.position));	
	
			});
					if(friendly.Count>0){
						
						AI fsearch=(AI)friendly[friendsearch].GetComponent("AI");
						if(fsearch.idle|fsearch.hunt){
							//IF THE SELECTED FRIEND ISNT BUSY, FIND HIM!
					targetlocateto=friendly[friendsearch];
					gototargetnostop=true;
							
						}
						else{
							//IF FRIEND IS BUSY, CHECK THE NEXT FRIEND, GIVE UP CHECKING IF ALL FRIENDS ARE BUSY
							if(friendsearchcycledone){
								gototargetnostop=false;
								targetlocateto=null;
								//odds=0;
					//oddsave=0;
					//retreattime=0;
					//retreat=false;
								//findhelponretreat=false;
								friendsearchcycledone=false;
								friendsearch=0;
							}
							
							friendsearch=friendsearch+1;
							
							if(friendsearch>=friendly.Count){

								friendsearch=0;
								friendsearchcycledone=true;
							}
							
						}
					}
					else findhelponretreat=false;
					
				}
				else{
					if(findhelponretreat){}
					
				retreattime+=Time.deltaTime;
				if(retreattime>retreatlength){
					odds=0;
					oddsave=0;
					retreattime=0;
					retreat=false;
					
				}
				if(enemy){
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currenttarget - transform.position), turnspeed * Time.deltaTime);		
					}
					}
			}
				
			
			
			//if attack bring awareness up
			if(awareboost){
				awaretimer+=Time.deltaTime;
				awarnesspercent=100;
				if(awaretimer>10){
					awarnesspercent=awarnesssave;
				}
			}
			
			
		//ATTACK!
		if(enemy){
				if(attackstance){
				
				//play spot sound
					spotsound=false;

				awaretimer=0;
				awareboost=true;
					//disable rigid body so he doesnt shift around and to speed framerate
				if(rigidbody)rigidbody.isKinematic=false;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemy.transform.position - transform.position), (turnspeed*0.3f) * Time.deltaTime);
			//attack stance
			if(attackstand){
						
		charactermodel.animation[attackstand.name].speed = charactermodel.animation[attackstand.name].length / 1f;
			charactermodel.animation.CrossFade( attackstand.name);	
			}
		}
			}
			
			
			//RESET ATTACK TIMER IF LEAVES ATTACKSTATE
			//if(runn)attacktimer=0;
			
		//THE ATTACK
		if(enemy){
		if(attack){
						currenttarget=enemy.transform.position;
					//start attack timer
			
					attacktimestart=true;
					
					//play attack sound
					if(enablesounds){
					if(attacktimer<(attackspeed*0.1f)){	
						//attacktimestart=true;
								damagedealt=false;
						audio.clip=swing;
						audio.Play();
					}
					}
					
					awaretimer=0;
					awareboost=true;
					if(attacktimer<(attackspeed*0.9))chargedisable=true;
					
					else{
						chargedisable=false;
					}
					
					retreattime+=Time.deltaTime;
					if(retreattime>retreatlength*2){
						
						cowardmultiply=cowardmultsave;
						retreattime=0;
					}
					//if(rigidbody)rigidbody.isKinematic=true;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemy.transform.position - transform.position), turnspeed * Time.deltaTime);
			;
					
			//LETS DEAL SOME DAMAGE
		
					if(attacktimer>attackspeed*0.3f){
							if(damagedealt){}
							else{
				dealdamage=true;
							}
				
			}	
						
						
				if(dealdamage){
								
				health enm=(health)enemy.GetComponent("health");
							if(enm){
								
				float targdist = Vector3.Distance(transform.position, enemy.transform.position);
				if(targdist<(attackrange*1.04f)){
								if(enablesounds){
				audio.clip=hit;
				audio.Play();
								}
				enm.currenthealth=enm.currenthealth-damage;
							}
						AI enm2=(AI)enemy.GetComponent("AI");
					
						if(enm2){		
					enm2.oddsave=enm2.oddsave+health+damage;
							framsave=0;	
					odds=oddsave;
					oddsave=0;
								}
					dealdamage=false;
						damagedealt=true;
								}
							
				}
						
			//PLAY ATTACK ANIMATION
			if(attacktimer<=0.2f&attacktimer>=0.1){
			if(Attackanimations.Count>0){
						if(Attackanimations[attackran]){
							
		charactermodel.animation[Attackanimations[attackran].name].speed = charactermodel.animation[Attackanimations[attackran].name].length / (attackspeed*0.75f);
			charactermodel.animation.CrossFade( Attackanimations[attackran].name);	
			}
			}
					}
		if(attacktimer>(attackspeed*0.85f)){
				if(attackstand){
							
		charactermodel.animation[attackstand.name].speed = charactermodel.animation[attackstand.name].length / 1f;
			charactermodel.animation.CrossFade( attackstand.name);	
			}
				
			}
				}
				}
			
		
				
			
			
			if(attacktimestart){
					attacktimer+=Time.deltaTime;
					if(attacktimer>attackspeed){
						attacktimer=0;
						dealdamage=false;
						damagedealt=false;
						chargedisable=false;
						attacktimestart=false;
					}
				}
			
			
			//SOUNDS!!
			
			//DIALOG
			//ask for help
			if(Dialogaskhelp){
				if(askhelpsounds.Count>0){
				audio.clip=askhelpsounds[askhelpsoundrandom];
				audio.Play();
				}
				Dialogaskhelp=false;
			}
			
			//respond
			if(Dialogrespond){
				
				respondtime+=Time.deltaTime;
				float retimemax;
				
				if(airespond){
					retimemax=1.7f;
					
				}
				else retimemax=0;
				
				if(respondtime>=retimemax){
					if(respondsounds.Count>0){
				audio.clip=respondsounds[respondsoundrandom];
				audio.Play();
					}
					airespond=false;
				Dialogrespond=false;
					
				}
			}
			
			
			//SPOTTED!
		if(enablesounds){
				if(enablechargesounds){
				if(charge|attackstance){

					spottimer+=Time.deltaTime;
					if(spottimer<0.3){
							if(spotsounds.Count>0){
				audio.clip=spotsounds[spotsoundrandom];
				audio.Play();
							}
				}
				if(spottimer>5) spottimer=0;
							}	
				}

	
			
			//IDLE SOUNDS!
			
			if(idle){
					if(enableidlesounds){
				spottimer=0;
				idlesoundtimer+=Time.deltaTime;
				if(idlesoundtimer>=playidlesoundevery){
				if(idlesounds.Count>0){
				audio.clip=idlesounds[idlesoundrandom];
				audio.Play();
							}
					idlesoundtimer=0;
				}
			}
				}
			
			//HUNTING AND ALERTED SOUNDS
				if(enablehuntsounds){
					
		if(awarnesspercent<100){
			if(hunt){
				
				spottimer=0;
				//play alert when first alerted
					alerttimer+=Time.deltaTime;
				if(alerttimer>0.8){
				if(alertsound){
					audio.Stop();
					if(alertsounds.Count>0){
				audio.clip=alertsounds[alertsoundrandom];
				audio.Play();
									}
					alertsound=false;
					//alerttimer=0;
				}
					}
				//HUNT sounds
				huntsoundtimer+=Time.deltaTime;
			
				if(huntsoundtimer>=6){
				if(huntsounds.Count>0){
				audio.clip=huntsounds[huntsoundrandom];
				audio.Play();
								}
					huntsoundtimer=0;
				}
		
			}
			else alertsound=true;
			}
				}
			
				//COWARD SOUNDS!
				if(enableretreatsounds){
			if(retreat){

				if(retreatsound){
					audio.Stop();
				if(retreatsounds.Count>0){
				audio.clip=retreatsounds[retreatsoundrandom];
				audio.Play();
							}
							retreatsound=false;	
							}	
		}
			else retreatsound=true;
			}
			}
			
			


	//CHARACTER AVOIDANCE
			
			if(attack){}
			else{
				if(idle|attack|attackstance){}
				else{
					if(charactertoavoid){
		if(avoidcharacter){
				avoidtime+=Time.deltaTime;
if(walkk)transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - charactertoavoid.transform.position), (turnspeed*0.6f) * Time.deltaTime);			
if(runn)transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - charactertoavoid.transform.position), turnspeed * Time.deltaTime);		
			
							
				if(avoidtime>0.4f){
					avoidtime=0;
					avoidcharacter=false;
					//if(lookfriend)lookfriend=null;
				}
			}
			}
			}
			}
			
			
			//MAKE SURE AI DOESNT TURN UP OR DOWN
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
			
			
			

				
		}
		}
}	
	
	void OnTriggerStay(Collider other){
		if(other){
			Waypoint wayray=(Waypoint)other.GetComponent("Waypoint");
			if(wayray|other.transform==enemy){}
			else{
				
				
				
				if(run&other.transform.tag=="obstacle"|other.transform.tag==charactertag){
			if(avoidcharacter){}
				else{
			if(other.transform.tag==charactertag){
							team friend=(team)other.GetComponent("team");
							if(friend){
								if(friend.teamnumber==team){
							AI eye=(AI)other.GetComponent("AI");
									if(eye){
									if(eye.aieye)lookfriend=eye.aieye;
							else lookfriend=other.transform;
									}
							}
								else lookfriend=null;
						}
						}
	charactertoavoid=other.transform;
	avoidcharacter=true;	
				}
		}
		}
		}
	}

	
}
