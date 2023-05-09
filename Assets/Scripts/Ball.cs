using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Hole"))
        {
            GameManager.ballsIn++;
            Destroy(this.gameObject);
        }
    }
}
