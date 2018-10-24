using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightColorEffects:MonoBehaviour
{

    Light light;
    List<int> rbg = new List<int>() { 0, 255, 0 };
    bool countingUp = true;
    int colorIndex = 0;
    int value = 0;
    float timer = 0;


    void Start()
    {
        light = GetComponent<Light>();
        light.color = new Color(rbg[0], rbg[1], rbg[2]);
    }

    bool skipUpdate = false;

    void Update()
    {
        //skipUpdate = !skipUpdate;
        //if(skipUpdate) return;

        Color newColor = new Color(rbg[0], rbg[1], rbg[2]);

        light.color = Color.Lerp(light.color, newColor, Time.deltaTime);

        bool temp = countingUp;
        
        if(countingUp)
        {
            rbg[colorIndex] += 5;

            if(rbg[colorIndex] >= 255)
            {
                countingUp = !countingUp;
                if(rbg[colorIndex] > 255) rbg[colorIndex] = 255;
            }
        }
        else
        {
            rbg[colorIndex] -= 5;

            if(rbg[colorIndex] <= 0)
            {
                countingUp = !countingUp;
                if(rbg[colorIndex] > 1) rbg[colorIndex] = 1;
            }

        }

        if(temp != countingUp)
            colorIndex = (colorIndex < 2) ? (colorIndex + 1) : 0; 

    }
}

