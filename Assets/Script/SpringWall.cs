using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringWall : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
