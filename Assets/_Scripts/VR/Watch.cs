using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Watch : MonoBehaviour
{
    //public TextMeshProUGUI timeText;
    public Text text;

    private void OnEnable()
    {
        GameManager.OnMinuteChanged += UpdateTime;
        GameManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        GameManager.OnMinuteChanged -= UpdateTime;
        GameManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        //timeText.text = $"{GameManager.Hour:00}:{GameManager.Minute:00}";
        text.text = $"{GameManager.Hour:00}:{GameManager.Minute:00}";
    }
}
