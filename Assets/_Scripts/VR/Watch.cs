using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Watch : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Text text;

    private void Update()
    {
        int min = (int)GameManager.getTimer / 60 % 60;
        int hour = (int)GameManager.getTimer / 3600 % 24;

        string _Min;
        if (min < 10)
            _Min = "0" + min;
        else
            _Min = min.ToString();

        string _Hour;
        if(hour < 10)
        {
            _Hour = "0" + hour;
        }
        else
        {
            _Hour = hour.ToString();
        }
        text.text = _Hour + ":" + _Min;
    }
}
