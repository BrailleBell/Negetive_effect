using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveTimeManager : MonoBehaviour
{
    public float durationMinutes = 5.0f; //How long each hour will be in minutes
    public int startTimeHourly = 0; //Hours
    public int endTimeHourly = 6; //Hours

    public float timeMultiplier = 1.0f; // speeds up the game for testing purposes

    [SerializeField]
    float actualTime = 0.0f; //How much real time has passed
    [SerializeField]
    float gameTime = 0.0f;

    bool isRunning = false;
    bool timerFinished = false;

    public bool TimerFinished
    {
        get
        {
            return timerFinished;
        }

        private set
        {
            timerFinished = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTimeManager();
    }

    void StartTimeManager()
    {
        isRunning = true;
    }

    void EndTimeManager()
    {
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            actualTime += Time.deltaTime * timeMultiplier;
            float normalizeTime = actualTime / (durationMinutes * 60.0f);
            gameTime = normalizeTime * (endTimeHourly - startTimeHourly);

            if (actualTime > durationMinutes * 60.0f)
            {
                isRunning = false;
                gameTime = endTimeHourly;
                TimerFinished = true;
                Debug.Log("Time has completed, you are alive");
            }
        }
    }
}
