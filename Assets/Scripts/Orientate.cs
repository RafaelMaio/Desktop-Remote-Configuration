using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientate : MonoBehaviour
{
    private GameObject cam;

    private Vector3 initialRotation;
    private Vector3 initialScale;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        initialRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 90, 0);
    }

    public Vector3 getInitialRotation()
    {
        return initialRotation;
    }

    public void setInitialScale(Vector3 scale)
    {
        initialScale = scale;
    }

    public Vector3 getInitialScale()
    {
        return initialScale;
    }
}
