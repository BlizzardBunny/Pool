using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwitch : MonoBehaviour
{
    public Transform pointA, pointB;
    public GameObject boundaryBlockingA, boundaryBlockingB;
    public Transform directionalLight;

    public static bool isPointA = true;

    private Coroutine moving;
    private int maxMoveFrames = 60;
    private int currMoveFrames = 0;

    private void Start()
    {
        transform.position = pointA.position;
        boundaryBlockingA.SetActive(false);
    }

    public void ToggleCamera()
    {
        EventSystem.current.SetSelectedGameObject(null); //unchecks ToggleCamBtn

        isPointA = !isPointA; 
        if (moving == null)
        {
            if (isPointA)
            {
                directionalLight.rotation *= Quaternion.AngleAxis(180f, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(100f, new Vector3(1, 0, 0));
                boundaryBlockingA.SetActive(false);
                boundaryBlockingB.SetActive(true);
                moving = StartCoroutine(Move(pointA));
            }
            else
            {
                directionalLight.rotation *= Quaternion.AngleAxis(180f, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(-100f, new Vector3(-1, 0, 0));
                boundaryBlockingB.SetActive(false);
                boundaryBlockingA.SetActive(true);
                moving = StartCoroutine(Move(pointB));
            }
        }
    }

    IEnumerator Move(Transform target)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.05f);
            transform.forward = Vector3.RotateTowards(transform.forward, target.forward, 0.05f, 0.05f);
            yield return new WaitForEndOfFrame();
            currMoveFrames++;

            if (currMoveFrames >= maxMoveFrames)
            {
                transform.position = target.position;
                currMoveFrames = 0;
                moving = null;
                yield break;
            }
        }
    }
}
