using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyBoi : MonoBehaviour
{
    public GameObject Player;
    public GameObject Monster;
    public int monstersAroundPlayer;

    private readonly float AppearWaitDuration;
    private Transform SurrounderParentTransform;

    // Start is called before the first frame update
    void Start()
    {
        SurrounderParentTransform = new GameObject(gameObject.name + "Surrounder Parent").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SurroundStepAnimated());
    }

    IEnumerator SurroundStepAnimated()
    {
        float AngleStep = 360.0f / monstersAroundPlayer;
        Player.transform.SetParent(SurrounderParentTransform);

        for (int i = 1; i < monstersAroundPlayer; i++)
        {
            GameObject newSurroundMonster = Instantiate(Monster);
            newSurroundMonster.transform.RotateAround(Player.transform.position,Vector3.up,AngleStep +i);
            newSurroundMonster.transform.SetParent(SurrounderParentTransform);
            yield return new WaitForSeconds(AppearWaitDuration);
            
        }
    }


// Update is called once per frame
    void Update()
    {
        
    }
}
