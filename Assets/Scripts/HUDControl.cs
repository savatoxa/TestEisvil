using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class HUDControl : MonoBehaviour
{
    public Slider progressBar;
    public Text ProgressText1;
    public Text progressText3;
    public Text progressText5;
    private float totalTime = 120f;
    private float remainingTime;
    private int totalDestroyed;
    private int spheresDestroyed;
    public RandomPosition randomPosition;

    void Start()
    {
        remainingTime = totalTime;
        progressBar.maxValue = totalTime;
        progressBar.value = totalTime;
        totalDestroyed = 0;
    }

    void Update()
    {
        totalDestroyed = randomPosition.cubeNums + randomPosition.sphereNums - randomPosition.spheres.Count - randomPosition.cubes.Count;
        spheresDestroyed = randomPosition.sphereNums - randomPosition.spheres.Count;
        progressText3.text = $"Total destroyed: {totalDestroyed}/10";
        progressText5.text = $"Spheres destroyed: {spheresDestroyed}/10";
        if(spheresDestroyed >= 10)
            progressText5.text = "10 spheres destroyed!";
        if (totalDestroyed >= 10)
            progressText3.text = "Job well done!";
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            progressBar.value = remainingTime;
        }
        else
        {
            ProgressText1.text = "You won!";
        }
    }
}

