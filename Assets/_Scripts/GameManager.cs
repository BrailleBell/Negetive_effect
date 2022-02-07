using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int film;
    public GameObject VRheadset;
    public TextMesh filmStr;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        //     DontDestroyOnLoad(VRheadset);
          filmStr = GameObject.Find("FilmText").GetComponent<TextMesh>();   
    }


    private void Update()
    {
        filmStr.text = film.ToString();
    }

    // films
    public void SnapPic()
    {

    }

    public void  GetFilm()
    {
        film++ ;
      
    }

}
