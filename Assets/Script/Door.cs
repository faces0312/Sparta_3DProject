using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private BoxCollider boxCollider;
    public GameObject door;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

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
            door.transform.Rotate(Vector3.forward, 1f);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
    }
}
