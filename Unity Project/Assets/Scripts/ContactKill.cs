using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactKill : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ASTEROID"))
        {
            other.gameObject.SetActive(false);
        }
    }
}