using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveY : MonoBehaviour
{

    private Camera cam;

    /// <summary>
    /// Camera scaling speed.
    /// </summary>
    [SerializeField] float navigationScaleSpeed = 1f;

    /// <summary>
    /// Shift Multiplier (when click the left shift key).
    /// </summary>
    [SerializeField] float shiftMultiplier = 2f;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<LeanSelectable>() != null)
        {
            if (GetComponent<LeanSelectable>().IsSelected)
            {
                Vector3 move = Vector3.zero;
                float scaleSpeed = navigationScaleSpeed * (Input.GetKey(KeyCode.LeftShift) ? shiftMultiplier : 1f) * Time.deltaTime * 2f;
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                    move += Vector3.up * scaleSpeed;
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                    move -= Vector3.up * scaleSpeed;
                transform.Translate(move);
            }
        }
        else
        {
            Vector3 move = Vector3.zero;
            float scaleSpeed = navigationScaleSpeed * (Input.GetKey(KeyCode.LeftShift) ? shiftMultiplier : 1f) * Time.deltaTime * 2f;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                move += Vector3.up * scaleSpeed;
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                move -= Vector3.up * scaleSpeed;
            transform.Translate(move);
        }
    }
}
