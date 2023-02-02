// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Dinamically sets the lean component properties and configuration properties.
// SPECIAL NOTES: Camera and slider properties.
// ===============================

using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanCameraHandler : MonoBehaviour
{
    /// <summary>
    /// Device camera.
    /// </summary>
    private GameObject camera;

    /// <summary>
    /// Configuration menu script.
    /// </summary>
    private AuxConfigurationMenu configureMenuScript;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    void Start()
    {
        try
        {
            camera = GameObject.Find("Main Camera");
            configureMenuScript = GameObject.Find("ConfigureMenu").GetComponent<AuxConfigurationMenu>();
        }
        catch { }
        try
        {
            this.GetComponent<LeanTranslate>().Camera = camera.GetComponent<Camera>();
            this.GetComponent<LeanTranslate>().enabled = this.GetComponent<LeanTranslate>().enabled && configureMenuScript.mode.value == 0;
            this.GetComponent<moveY>().enabled = this.GetComponent<moveY>().enabled && configureMenuScript.mode.value == 0;
        }
        catch { }
        try
        {
            this.GetComponent<LeanRotateCustomAxis>().enabled = this.GetComponent<LeanRotateCustomAxis>().enabled && 
                ((configureMenuScript.mode.value == 0) && (configureMenuScript.rotScaleSlider.value == 0));
            this.GetComponent<LeanRotateCustomAxis>().Axis = new Vector3(
                -Convert.ToSingle(configureMenuScript.rotationAxisSlider.value == 0), 
                -Convert.ToSingle(configureMenuScript.rotationAxisSlider.value == 1), -Convert.ToSingle(configureMenuScript.rotationAxisSlider.value == 2));
        }
        catch { }
        try
        {
            this.GetComponent<LeanScale>().Camera = camera.GetComponent<Camera>();
            this.GetComponent<LeanScale>().enabled = this.GetComponent<LeanScale>().enabled && ((configureMenuScript.mode.value == 0) && (configureMenuScript.rotScaleSlider.value == 1));
        }
        catch { }
        try
        {
            //Gizmo translation
            transform.GetChild(1).GetComponent<GizmoTranslateScript>().translateTarget = this.gameObject;
            transform.GetChild(1).GetChild(0).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(1).GetChild(1).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(1).GetChild(2).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(1).gameObject.SetActive((configureMenuScript.mode.value == 1) && (configureMenuScript.transformationsSlider.value == 0));

            //Gizmo rotation
            transform.GetChild(2).GetComponent<GizmoRotateScript>().rotateTarget = this.gameObject;
            transform.GetChild(2).GetChild(1).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(2).GetChild(2).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(2).GetChild(3).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(2).gameObject.SetActive((configureMenuScript.mode.value == 1) && (configureMenuScript.transformationsSlider.value == 1));

            //Gizmo scale
            transform.GetChild(3).GetComponent<GizmoScaleScript>().scaleTarget = this.gameObject;
            transform.GetChild(3).GetChild(0).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(3).GetChild(1).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(3).GetChild(2).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(3).GetChild(3).GetComponent<GizmoClickDetection>().gizmoCamera = camera.GetComponent<Camera>();
            transform.GetChild(3).gameObject.SetActive((configureMenuScript.mode.value == 1) && (configureMenuScript.transformationsSlider.value == 2));
        }
        catch { }
    }
}