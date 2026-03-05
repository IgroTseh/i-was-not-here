using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovController : MonoBehaviour
{
    private List<GameObject> objectsInFov;

    private void Awake()
    {
        objectsInFov = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInFov.Add(collision.gameObject);
        Debug.Log("Plaer Entered!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsInFov.Remove(collision.gameObject);
        Debug.Log("Plaer Exited!");
    }

    public List<GameObject> GetObjectsInFov()
    {
        return new List<GameObject>(objectsInFov);
    }
}
