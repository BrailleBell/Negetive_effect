
using UnityEngine;
using UnityEngine.AI;
public class Trundle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent ghost;
    public GameObject Larm, Rarm;
    private float attackDist = 15;
    private Transform orgLarmScale, orgRarmScale;

    private float lerp;
    // Start is called before the first frame update
    void Start()
    {
        ghost = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        lerp = Mathf.PingPong(Time.time, 1) / 100;


    }

    // Update is called once per frame
    void Update()
    {
        ghost.destination = Player.transform.position;
        gameObject.transform.LookAt(Player.transform);
        if (Vector3.Distance(transform.position, Player.transform.position) < attackDist)
        {
            ScaleArmsUp();
        }

        else
        {
            Larm.transform.localScale =
                Vector3.Lerp(transform.localScale, transform.localScale / 4, lerp);
            
            Rarm.transform.localScale =
                Vector3.Lerp(transform.localScale, transform.localScale / 4, lerp);   
        }


    }


    public void ScaleArmsUp()
    {
        
        
        Larm.transform.localScale =
            Vector3.Lerp(transform.localScale, transform.localScale * 4, Time.deltaTime * 100/1);
            
        Rarm.transform.localScale =
            Vector3.Lerp(transform.localScale, transform.localScale * 4, Time.deltaTime * 100/1);
        
    }
    
    
}
