using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatController : MonoBehaviour
{
    public bool walkingTowardsObject = false;
    public GameObject currentThing;
    public GameObject cat;

    public float speed = 0.5f;
    public List<GameObject> thingsToBreak = new List<GameObject>();
    void Start()
    {

    }

    void SelectNextObjectToBreak()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(thingsToBreak.Count);
        GameObject newThing = thingsToBreak[index];
        if (currentThing == newThing && thingsToBreak.Count > 1)
        {
            SelectNextObjectToBreak();
        }
        else
        {
            currentThing = newThing;
        }
    }

    void Update()
    {
        if (currentThing == null) 
        {
            SelectNextObjectToBreak();
        }
        Vector3 dir = (currentThing.transform.position - cat.transform.position).normalized;
        cat.transform.position = cat.transform.position + dir * speed * Time.deltaTime;
        if (Vector3.Distance(cat.transform.position, currentThing.transform.position) < 0.8)
        {
            SelectNextObjectToBreak();
            return;
        }
        cat.transform.LookAt(currentThing.transform);
    }
}
