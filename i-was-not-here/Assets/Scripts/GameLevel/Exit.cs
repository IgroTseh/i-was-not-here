using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public delegate void ExitReachedHandler();
    public event ExitReachedHandler OnPlayerReachedExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            OnPlayerReachedExit?.Invoke();
        }
    }
}
