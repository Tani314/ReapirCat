using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject destroyedVersion;
    void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            //destroyedVersion.SetActive(true);


            //this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
