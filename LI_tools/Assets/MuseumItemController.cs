using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumItemController : MonoBehaviour
{
    public GameObject completeObject;
    public GameObject brokenObject;
    public bool broken = false;
    public Animator anim;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        anim = brokenObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hello");
            if (null != anim)
            {
                anim.Play("Fix", 0, 0f);
            }
        }

         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
         Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
         RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider != null) {
                    GameObject touchedObject = hit.transform.gameObject;
                    if (touchedObject == this.gameObject)
                    {
                        anim.Play("Fix", 0, 0f);
                    }
                }
            }
         }
    }

    public void BreakIt() {
        completeObject.SetActive(false);
        brokenObject.SetActive(true);
        audioSource.Play();
        //anim.Play("Fix", 0, 0f);
        //Debug.Log("hellooo");
    }
}
