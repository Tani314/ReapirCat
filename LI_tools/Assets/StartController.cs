﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            SceneManager.LoadScene("TestCat", LoadSceneMode.Single);
        }
    }
}
