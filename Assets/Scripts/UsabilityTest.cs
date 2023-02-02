// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Handles the usability test.
// SPECIAL NOTES: Only used for the usability tests.
// ===============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class UsabilityTest : MonoBehaviour
{
    /// <summary>
    /// For verifying if the usability tests option is enabled.
    /// </summary>
    private bool usabilityEnabled = false;

    /// <summary>
    /// Participant identifier.
    /// </summary>
    private int participantId = 0;

    /// <summary>
    /// Prefabs for the transparent objects.
    /// </summary>
    public GameObject qrCodeTransparentPrefab;
    public GameObject cubeTransparentPrefab;
    public GameObject startingPositionTransparentPrefab;
    public GameObject arrowTransparentPrefab;
    public GameObject barrierTransparentPrefab;
    public GameObject goalTransparentPrefab;

    /// <summary>
    /// Transparent objects.
    /// </summary>
    private GameObject startingPositionTransparent;
    private GameObject[] cubes = new GameObject[9];

    /// <summary>
    /// List of game objects.
    /// </summary>
    //private List<GameObject> goList = new List<GameObject>();

    /// <summary>
    /// File path for saving the usability tests results.
    /// </summary>
    private string filePathName;
    //private string filePathName = "Assets/UsabilityTest/testConfDesktop.txt";

    /// <summary>
    /// Usability test date.
    /// </summary>
    private DateTime date;

    /// <summary>
    /// Number of objects placed in the usability test.
    /// </summary>
    private int counter = 0;

    /// <summary>
    /// Connection to the configuration window script.
    /// </summary>
    public ConfigureMenuScript configureMenuScript;

    /// <summary>
    /// Unity OnEnable function.
    /// </summary>
    public void startTest()
    {
        if (usabilityEnabled)
        {
            startingPositionTransparent = Instantiate(qrCodeTransparentPrefab, new Vector3(1f, -0.3f, 1f), Quaternion.Euler(new Vector3(0, 180, 0)));
            changeSortOrder(startingPositionTransparent.transform);
            date = DateTime.Now;
            cubes[0] = startingPositionTransparent;
        }
    }

    /// <summary>
    /// Enable the next object placement.
    /// </summary>
    /// <param name="numObject">Number of the last placed object.</param>
    public void placeNextObject(int numObject)
    {
        if (usabilityEnabled)
        {
            counter += 1;
            configureMenuScript.changeNumObjectsText(numObject);
            //Translação
            if (numObject == 0)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(2.48f, -0.2f, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0)));
                cubes[counter].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                changeSortOrder(cubes[counter].transform);
            }
            else if (numObject == 1)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(3.1f, -0.42f, -1.6f), Quaternion.Euler(new Vector3(0, 0, 0)));
                cubes[counter].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                changeSortOrder(cubes[counter].transform);
            }
            //Rotação
            else if (numObject == 2)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(-1.8f, 0.4f, -0.1f), Quaternion.Euler(new Vector3(0, 45, 0)));
                cubes[counter].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                changeSortOrder(cubes[counter].transform);
            }
            else if (numObject == 3)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(4.95f, 0.14f, -1.3f), Quaternion.Euler(new Vector3(10, 70, 35)));
                changeSortOrder(cubes[counter].transform);
                cubes[counter].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            //Escala
            else if (numObject == 4)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(-1.60f, -0.43f, 2.91f), Quaternion.Euler(new Vector3(0, 0, 0)));
                changeSortOrder(cubes[counter].transform);
                cubes[counter].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }
            else if (numObject == 5)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(-1.60f, -0.17f, 1.81f), Quaternion.Euler(new Vector3(0, 0, 0)));
                changeSortOrder(cubes[counter].transform);
                cubes[counter].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            //TUDO
            else if (numObject == 6)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(0.64f, 0.22f, -2.08f), Quaternion.Euler(new Vector3(0, 20, 50)));
                changeSortOrder(cubes[counter].transform);
                cubes[counter].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (numObject == 7)
            {
                cubes[counter] = Instantiate(cubeTransparentPrefab, new Vector3(1.125f, -0.43f, 0.57f), Quaternion.Euler(new Vector3(80, 0, 0)));
                changeSortOrder(cubes[counter].transform);
                cubes[counter].transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            }
            else {
                configureMenuScript.handleShowText(true);
            }
        }
    }

    /// <summary>
    /// Verify if the usability tests are enabled.
    /// </summary>
    public bool getUsabilityEnabled()
    {
        return usabilityEnabled;
    }

    /// <summary>
    /// Change the transparent object sorting order, so they appear behind the opaque ones.
    /// </summary>
    /// <param name="objectChild">Object piece transform component.</param>
    private void changeSortOrder(Transform objectChild)
    {
        for(int i = 0; i < objectChild.childCount; i++)
        {
            if (objectChild.GetChild(i).GetComponent<Renderer>() != null)
            {
                objectChild.GetChild(i).GetComponent<Renderer>().sortingOrder = 10;
            }
            changeSortOrder(objectChild.GetChild(i));
        }
    }

    /// <summary>
    /// Enable the usability test.
    /// Get the participant identifier.
    /// </summary>
    public void enableTest()
    {
        usabilityEnabled = true;
        filePathName = Application.persistentDataPath + "/testConfDesktop.txt";
        if (File.ReadAllLines(filePathName).Length == 0)
        {
            participantId = 1;
        }
        else
        {
            participantId = Int32.Parse(File.ReadAllLines(filePathName)[File.ReadAllLines(filePathName).Length - 1].Split(',')[0]) + 1;
        }
    }

    /// <summary>
    /// Translate the starting object in the YY axis.
    /// </summary>
    public void translateStartingPosition()
    {
        startingPositionTransparent.transform.position += new Vector3(0f, 1.4f, 0f);
    }

    /// <summary>
    /// Stop the object placement timer.
    /// </summary>
    public void stopTime(GameObject goAux, string piece)
    {
        Transform go = goAux.transform;
        TimeSpan time = DateTime.Now - date;
        Vector3 position = go.transform.position - cubes[counter].transform.position;
        Vector3 rotation = go.transform.rotation.eulerAngles - cubes[counter].transform.rotation.eulerAngles;
        Vector3 scale = go.transform.localScale - cubes[counter].transform.localScale;
        Debug.Log("scale1: " + scale.x.ToString());
        if (goAux.GetComponent<Orientate>() != null)
        {
            rotation = goAux.GetComponent<Orientate>().getInitialRotation() - cubes[counter].transform.rotation.eulerAngles;
            scale = goAux.GetComponent<Orientate>().getInitialScale() - cubes[counter].transform.localScale;
        }
        Debug.Log("scale2: " + scale.x.ToString());
        using (StreamWriter sw = File.AppendText(filePathName))
        {
            sw.WriteLine(participantId.ToString() + "," + counter.ToString() + "," + time.ToString() + "," +
                position.x.ToString() + "," + position.y.ToString() + "," + position.z.ToString() + "," + 
                rotation.x.ToString() + "," + rotation.y.ToString() + "," + rotation.z.ToString() + "," + 
                scale.x.ToString() + "," + piece);
        }
        date = DateTime.Now;
        cubes[counter].SetActive(false);
    }

    /// <summary>
    /// Set the initial time.
    /// </summary>
    public void setTime()
    {
        date = DateTime.Now;
    }
}