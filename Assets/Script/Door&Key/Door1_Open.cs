using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1_Open : MonoBehaviour
{
    public GameObject door;
    public BoxCollider boxCollider;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            boxCollider.enabled = false;
            StartCoroutine(RotateObject());
        }
    }

    private IEnumerator RotateObject()
    {
        for (int i = 0; i < 140; i++)
        {
            door.transform.Rotate(Vector3.forward);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
