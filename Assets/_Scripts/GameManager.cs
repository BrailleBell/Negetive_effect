using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    
    public static  int film = 0;
    public GameObject VRheadset;
    public static Text filmStr;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        //     DontDestroyOnLoad(VRheadset);
        filmStr = GameObject.FindGameObjectWithTag("filmClips").GetComponent<Text>();   
    }

    // Update is called once per frame

   public void SnapPic()
    {
        film--;
        filmStr.text = film.ToString();
    }

    public static void  GetFilm()
    {
        film++ ;
        filmStr.text = film.ToString();
    }

}
