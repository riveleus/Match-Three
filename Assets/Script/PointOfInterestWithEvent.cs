using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestWithEvent : MonoBehaviour
{
    public static event Action<PointOfInterestWithEvent> OnPointOfInterestEntered;
    [SerializeField] string _pointName;
    public string pointName { get { return _pointName; } }

    void OnDisable()
    {
        if (OnPointOfInterestEntered != null)
            if (!GameManager.instance.isGameEnd)
                OnPointOfInterestEntered(this);
    }
}
