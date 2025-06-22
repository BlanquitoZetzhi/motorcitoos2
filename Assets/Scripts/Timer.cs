using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public int minutes;

    public float limitSeconds;

    public float seconds;

    public Color dangerColor;

    public TextMeshProUGUI TimerText;

    private void Start()
    {
        ActualizarContador();
    }

    private void Update()
    {
        seconds -= Time.deltaTime;

        if(seconds <= 0)
        {
            seconds = 60;
            minutes -= 1;
        }

        ActualizarContador();
        
        if (seconds < limitSeconds && minutes <1)
        {
            TimerText.color = dangerColor;
        }

    }

    public void ActualizarContador()
    {
        if(seconds<9.9f)
        {
            if(minutes <1)
            {
                TimerText.text = minutes.ToString() + ":" + seconds.ToString("0.0");    
            }
            else
            {
                TimerText.text = minutes.ToString() + ":0" + seconds.ToString("f0");
            }
        }
        else
        {
            TimerText.text = minutes.ToString() + ":" + seconds.ToString("f0");
        }
    }

    
}
