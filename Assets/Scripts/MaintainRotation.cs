using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.identity;
    }
}
