using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControll>())
        {
            gameObject.GetComponentInParent<Enemy>().Target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControll>())
        {
            gameObject.GetComponentInParent<Enemy>().Target = null;
        }
    }
}
