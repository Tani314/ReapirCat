using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatController : MonoBehaviour
{
    public bool walkingTowardsObject = false;
    public MuseumItemController currentThing;
    public GameObject cat;

    public float speed = 1.5f;
    public List<MuseumItemController> thingsToBreak = new List<MuseumItemController>();
    void Start()
    {

    }

    void SelectNextObjectToBreak()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(thingsToBreak.Count);
        MuseumItemController newThing = thingsToBreak[index];
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
        Camera.main.transform.Rotate (-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, -Input.gyro.rotationRateUnbiased.z);
        if (currentThing == null)
        {
            SelectNextObjectToBreak();
        }
        Vector3 dir = (currentThing.transform.position - cat.transform.position).normalized;
        cat.transform.position = cat.transform.position + dir * speed * Time.deltaTime;
        if (Vector3.Distance(cat.transform.position, currentThing.transform.position) < 0.2)
        {
            currentThing.BreakIt();
            SelectNextObjectToBreak();
            return;
        }
        cat.transform.LookAt(currentThing.transform);
    }
}
