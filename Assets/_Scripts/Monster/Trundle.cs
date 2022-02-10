
using UnityEngine;
using UnityEngine.AI;
public class Trundle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent ghost;
    public float distanceToPlayer;
    private float lerp;
    public float walkRadius;
    public float aboveTimer, belowTimer;
    private Vector3 underGroundPos, aboveGroundPos;
    public bool underGround, aboveGround;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        //  lerp = 1;
        //  lerp = Mathf.PingPong(Time.time, 1) / 1;
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        rb = GetComponent<Rigidbody>();


    }
    
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        underGroundPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 10,
            gameObject.transform.position.z);
        
        aboveGroundPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10,
            gameObject.transform.position.z);
        ghost.destination = Player.transform.position;
        
        gameObject.transform.LookAt(Player.transform);

        
        
        if (distanceToPlayer <= 40)
        { 
            underGround = false;
            aboveGround = true;
            
            
        }
        else
        {
            underGround = true;
            aboveGround = false;

        }

        if (underGround)
        {
            rb.useGravity = false;
            belowTimer += Time.deltaTime;
            ghost.updatePosition = false;
            if(belowTimer >= 10)
            {
               // transform.position = Vector3.Lerp(gameObject.transform.position, aboveGroundPos, lerp * Time.deltaTime);

               gameObject.transform.position = underGroundPos;
                ghost.updatePosition = true;
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
                Vector3 finalPosition = hit.position;

                gameObject.transform.position = finalPosition;

                belowTimer = 0;


            }
            
            


            // gameObject.transform.position.x,
            // gameObject.transform.position.y - 5,
            // gameObject.transform.position.z

        }

        if (aboveGround)
        {
            aboveTimer += Time.deltaTime;
            if (aboveTimer > 2)
            {
                rb.velocity = transform.up * lerp;
                gameObject.transform.position = aboveGroundPos;
              //  transform.position = Vector3.Lerp(gameObject.transform.position, underGroundPos, lerp * Time.deltaTime);
                ghost.updatePosition = true;
                float randomUpTime = Random.Range(10, 50);
                aboveTimer = 0;

                if (aboveTimer > randomUpTime)
                {
                   // aboveGround = false;
                   // underGround = true;
                   // aboveTimer = 0;
                }
            }

        }
        
        

    }
}
