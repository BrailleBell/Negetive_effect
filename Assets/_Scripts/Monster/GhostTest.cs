using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GhostTest : MonoBehaviour
{
    
    private GameObject Player;
    private Vector3 targetPos;
    private int dir;
    public bool seen, attacking, ghostDying;
    public float timer, killTimer, attackTimer;
    private GameObject[] cover;
    private Material hidingMat;
    public Material orgMat;
    private NavMeshAgent ghost;
    public GameObject cabin;
    public AudioClip[] hidingSound;
    public AudioClip killSound;
    private AudioSource sound;
    private Vector3 ghostPos;
    public float attackDist = 20;
    public int GoToSceneWhenKilled;
    private Vector3 monsterOrgPos;






    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        orgMat = gameObject.GetComponent<Renderer>().material;
        ghost = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();
        
        
        // for testing 
        monsterOrgPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(Vector3.Distance(ghost.transform.position, Player.transform.position));
            ghostPos = ghost.transform.position;
            targetPos = Player.transform.position; // finds the player
            gameObject.transform.LookAt(targetPos);


            if (GetComponent<Renderer>().isVisible && Vector3.Distance(ghost.transform.position, Player.transform.position) < 100) // checks if visible or not
            {
                seen = true;
            }
            else
            {
                seen = false;
            }

            if (seen & !attacking) // teleports when not attacking and being seen to the closest cover and takes its texture
            {
                timer += Time.deltaTime;
                if (timer > 0.8f)
                {
                    teleportaway();
                    if (timer > 8)
                    {
                        attacking = true;
                        timer = 0;
                    }
                } 
            }
            else if (!seen) // if not seen follows the player and takes his original texture
            {
                seen = false;
                Debug.Log("IKKE SYNLIG");
                timer = 0;
                ghost.destination = targetPos;
                gameObject.transform.LookAt(Player.transform.position);
                ghost.speed = 20;
                float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, orgMat, lerp);
                
            }


            // close to the player
            if (Vector3.Distance(transform.position, Player.transform.position) < attackDist)
            {
                attacking = true;
                ghost.speed = 50;

                if (Vector3.Distance(transform.position, Player.transform.position) < 7)
                {
                    SceneManager.LoadScene(GoToSceneWhenKilled); // kill op hurt player 
                }

            }

            if (ghostDying) // after taking picture of the ghost it dies after 0.5 sec
            {
                ghost.velocity = Vector3.zero;
                ghost.isStopped = true;
                killTimer += Time.deltaTime; // kill time must be over 0.2 secounds! 
                if (killTimer > 0.5f)
                {
                    gameObject.transform.position = monsterOrgPos;  //KILL GHOST INSERT HERE 
                   // gameObject.SetActive(false);
                    Debug.Log(Vector3.Distance(gameObject.transform.position, Player.transform.position) + " Hit Ditscance");
                    killTimer = 0;
                    ghostDying = false;
                }
               

            }

            if (!ghostDying)
            {
                ghost.isStopped = false;
            }

            if (attacking)
            {
                ghost.destination = Player.transform.position;
                ghost.speed = 200;
                attackTimer += Time.deltaTime;
                if (attackTimer > 5)
                {
                    attacking = false;
                    attackTimer = 0;
                    teleportaway();
                    
                }
            }
            
    }

    
    void teleportaway() // teleports the ghost to closest cover
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Cover");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                hidingMat = closest.GetComponent<Renderer>().material;
                float lerptimer = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, hidingMat, lerptimer);
                ghost.destination = closest.transform.position;
                transform.position =  Vector3.MoveTowards(transform.position,closest.transform.position, 20f * Time.deltaTime);
            }
            else
            {
                float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, orgMat, lerp);
            }
        }
        sound = null;
        Debug.Log("Teleport");
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            Debug.Log("monster hit");
            ghostDying = true;


        }
    }
}

