using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class ghost : MonoBehaviour
{
    public GameObject Death;
    public GameObject player;
    public GameObject ground;
    public Vector3 newPosition;
    private Vector3 direction;
    public Vector3 player_pos;
    private List<string> States = new List<string>();
    private string State;
    public Text health;
    private Rigidbody rb;
    

    public float dist;
    public float boundx;
    public float timer;
    public int health_point;
    public int current_health;
    // Start is called before the first frame update
    void Start()
    {
        boundx = ground.GetComponent<MeshCollider>().bounds.extents.x;
        rb = GetComponent<Rigidbody>();
        State = "roam";
        health.text = current_health.ToString();
        States.Add("roam");
        States.Add("follow");
        States.Add("attack");
        States.Add("death");
   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(State);
        timer +=  Time.deltaTime;
     //   Debug.Log(timer);
        player_pos = player.transform.position;
        dist = Vector3.Distance(transform.position, player_pos);
        
        if (dist < 20)
        {
            State = "follow";
            if (dist < 3)
            {
                State = "attack";
            }

        }
        else
        {
            State = "roam";
        }

   

        switch (State)
        {

            case "roam":
                roam();
                break;
            case "follow":
                follow();
                break;
            case "attack":
                attack();
                break;
            
            default:

                roam();
                break;
                
        }
    }
     



        void roam()
        {


       


            if (timer >4)
            {
            newPosition = new Vector3(Random.Range(0f, boundx),0, Random.Range(0f, boundx));
            timer = 0;
            }
        rb.MovePosition(transform.position + (newPosition * 3 * Time.deltaTime));
    }
        void follow()
        {

        direction = player_pos - transform.position;

        rb.MovePosition(transform.position + (direction * 5 * Time.deltaTime));
        //transform.Translate(direction);
        
        Debug.Log("hello");
        }




        void attack()
        {
        if (timer >= 1)
        {

            timer = 0;
            current_health -= 1;
            if (current_health <= 0)
            {
                Death.SetActive(true);
            }
        }
        
           
        health.text =current_health.ToString(); 
        }






    void death()
        {
        GetComponent<ParticleSystem>().Emit(4);
        GetComponent<MeshRenderer>().enabled = false;
        Object.Destroy(this, 1);
        }

    private void OnDestroy()
    {
        GetComponent<ParticleSystem>().Emit(4); 

    }

}
