using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumItemController : MonoBehaviour
{
    public GameObject completeObject;
    public GameObject brokenObject;
    public bool broken = false;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakIt() {
        completeObject.SetActive(false);
        brokenObject.SetActive(true);
        audioSource.Play();
    }
}
