using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : Subject
{
    [SerializeField] string pointName;
    
    void OnDisable()
    {
        if(!GameManager.instance.isGameEnd)
            Notify(pointName);
    }
}
