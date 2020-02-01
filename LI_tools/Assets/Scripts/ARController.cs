using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using System;

public class ARController : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseValid = false;
    public GameObject placementIndicator;
    public bool placed = false;
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

	private void UpdatePlacementIndicator()
	{
        if (placed) return;
        placementIndicator.SetActive(placementPoseValid);
        if (placementPoseValid)
        {
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            placed = true;
        }
	}

	private void UpdatePlacementPose()
	{
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseValid = hits.Count > 0;
        if (placementPoseValid) {
            placementPose = hits[0].pose;
        }
	}
}
