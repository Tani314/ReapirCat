using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraController : MonoBehaviour
{

    private Gyroscope gyro;
    private bool gyroEnabled;

    private Quaternion rot;
    [SerializeField] public GameObject cameraContainer;
    [SerializeField] private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            Debug.Log("HELLO GYRO");
            cameraContainer.transform.localRotation = gyro.attitude * rot;
        }
    }

    private bool EnableGyro() {
        if (SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;
            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0,0, 1, 0);
            Debug.Log("Enable gyro");
            return true;
        } else {
            Debug.Log("No gyro");
            return false;
        }
    }
}