using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float speedMod = 2f;
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public Transform cameraPos;
    public Rigidbody rb;
    public Transform indicator, topLeftHole, bottomRightHole;

    private bool isPlacing = true;
    private bool isMoving = false;
    private bool isHitting = false;
    private Vector3 direction;
    private Vector3 position = new Vector3(0, 3, -3);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position;
    }

    private void OnMouseDown()
    {
        if (!isPlacing)
        {
            isHitting = true;
            GameManager.moves++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Input.mousePosition;

        if (CameraSwitch.isPointA)
        {
            screenPoint.z = -cameraPos.position.z;
        }
        else
        {
            screenPoint.z = cameraPos.position.z;
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

        //clamp player y
        if (transform.position.y < 0.675f)
        {
            transform.position = new Vector3(transform.position.x, 0.675f, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {            
            if (isPlacing)
            {
                if (indicator.position.x > topLeftHole.position.x && indicator.position.x < bottomRightHole.position.x 
                    && indicator.position.z > bottomRightHole.position.z && indicator.position.z < topLeftHole.position.z)
                {
                    indicator.gameObject.SetActive(false);
                    isPlacing = false;
                }
            }
            else
            {
                if (isHitting && lineRenderer.enabled && !EventSystem.current.IsPointerOverGameObject())
                {
                    isHitting = false;
                    direction = lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1);
                    direction.y = 0.0f;
                    isMoving = true;
                }
            }
        }      

        if (isPlacing)
        {
            transform.position = new Vector3(worldPoint.x, 3.0f, worldPoint.z);
            indicator.position = new Vector3(worldPoint.x, transform.position.y - 2.5f, worldPoint.z);
            indicator.gameObject.SetActive(true);
        }

        if (isHitting)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, worldPoint);
        }
        else
        {
            lineRenderer.enabled = false;
        }

        if (isMoving)
        {
            rb.velocity = direction * speedMod;
            isMoving = false;
        }
    }

    private void ResetCueBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isPlacing = true;
        isMoving = false;
        isHitting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Boundary"))
        {
            ResetCueBall();
        }
        else if (other.gameObject.tag.Equals("Hole"))
        {
            GameManager.hasFallen = true;
            ResetCueBall();
        }
    }
}
