using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
   
   public float timerFloat = 0f;
   public int timer = 0;


   
   void Update ()
    {
        
        timerFloat += Time.deltaTime;
        if (timerFloat >= 1f)
        {
            timerFloat = 0f;
            SecondStep();
        }
        
    }

    void SecondStep ()
    {
        timer += 1;
    }
}
