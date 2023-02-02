using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTag : MonoBehaviour
{
    public GameObject head;
    public GameObject leftArrow;
    public GameObject rightArrow;
    private int frameCount;
    // Start is called before the first frame update
    void Start()
    {
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        gameObject.transform.parent.LookAt(head.transform);
        gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);

        if(frameCount % 5 == 0)
		{
            Vector3 directionToTarget = (transform.position - head.transform.position).normalized;
            float angle = Vector3.SignedAngle(head.transform.forward, directionToTarget, Vector3.up);
            
            if(angle > -35 && angle < 35)
			{
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
			}
			else
			{
                if (angle > 0)
                {
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(true);
                }
                else
                {
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(true);
                }
            }
        }
        
    }
}
