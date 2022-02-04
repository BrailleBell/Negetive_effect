using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GhostTest : MonoBehaviour
{
    
    private GameObject Player;
    private Vector3 targetPos;
    private int dir;
    private bool seen, seenafterTeleport, attacking, ghostDying;
    public float timer, killTimer;
    private GameObject[] cover;
    private Material hidingMat;
    public Material orgMat;
    private NavMeshAgent ghost;
    public GameObject cabin;
    public AudioClip[] hidingSound;
    public AudioClip killSound;
    private AudioSource sound;
    private Vector3 ghostPos;
    public float attackDist = 15;
    public int GoToSceneWhenKilled;






    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
     //   orgMat = gameObject.GetComponent<Renderer>().material;
        ghost = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(ghost.transform.position, Player.transform.position));
            ghostPos = ghost.transform.position;
            targetPos = Player.transform.position; // finds the player
            gameObject.transform.LookAt(targetPos);


            if (GetComponent<Renderer>().isVisible) // checks if visible or not
            {
                seen = true;
                Debug.Log("SYNLIG");
                ghost.destination = targetPos;
                gameObject.transform.LookAt(Player.transform.position);

            }
            else
            {
                seen = false;
                Debug.Log("IKKE SYNLIG");
                timer = 0;
                ghost.destination = targetPos;
                gameObject.transform.LookAt(Player.transform.position);
                ghost.speed = 8;
                float lerp = Mathf.PingPong(Time.time, 1) / 100f;
                GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, orgMat, lerp);
            }

            if (seen & !attacking)
            {
                timer += Time.deltaTime;
                if (timer > 0.8f)
                {
                    teleportaway();
                    //  ghost.destination = targetPos;
                    //gameObject.transform.LookAt(Player.transform.position);
                    //ghost.speed = 5;
                }
                seen = false;
            }


            // close to the player
            if (Vector3.Distance(transform.position, Player.transform.position) < attackDist)
            {
                attacking = true;
                ghost.speed = 50;

                if (Vector3.Distance(transform.position, Player.transform.position) < 3)
                {
                    SceneManager.LoadScene(GoToSceneWhenKilled);
                }
               

               


            }
            else
            {
                ghost.speed = 5;
                attacking = false;
            }
            
            if (ghostDying)
            {
                killTimer += Time.deltaTime;
                if (killTimer > 0.5f)
                {
                    gameObject.SetActive(false);
                    Debug.Log(Vector3.Distance(gameObject.transform.position, Player.transform.position) + " Hit Ditscance");
                    killTimer = 0;
                    
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
        }
        sound = null;
        Debug.Log("Teleport");
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraShoot")
        {
            ghostDying = true;


        }
    }
}

