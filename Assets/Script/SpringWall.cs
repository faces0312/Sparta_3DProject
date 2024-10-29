using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringWall : MonoBehaviour
{
    public GameObject ground3;
    [SerializeField] private float jumpForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                ground3.SetActive(true);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
