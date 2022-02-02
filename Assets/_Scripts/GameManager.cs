using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int film;
    public GameObject VRheadset;
    public Text filmStr;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(VRheadset);
    }

    // Update is called once per frame

   public void SnapPic()
    {
        film--;
        filmStr.text = film.ToString();
    }

    public void  GetFilm()
    {
        film ++;
        filmStr.text = film.ToString();
    }

}
