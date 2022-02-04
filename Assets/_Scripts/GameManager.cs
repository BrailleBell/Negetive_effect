using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int film;
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
        filmStr = GameObject.FindGameObjectWithTag("Film").GetComponent<Text>();   
    }
    
    
    
        // films
    public void SnapPic()
    {
        film--;
        //filmStr.text = film.ToString();
    }

    public void  GetFilm()
    {
        film++ ;
       // filmStr.text = film.ToString();
    }

}
