using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nepotrebstvo : MonoBehaviour
{
    [SerializeField] private float changeDirTimer = 2f;
    [SerializeField] private float moveSpeed = 6f;

    private float currTimer;
    private Vector3 moveDir;

    private void Awake()
    {
        currTimer = changeDirTimer;
        moveDir = Vector3.zero;
    }

    private void Update()
    {
        // Patroling movement
        currTimer += Time.deltaTime;
        if (currTimer >= changeDirTimer)
        {
            moveDir = GetRandomDir();
            currTimer = 0;
        }
        Move(moveDir);
    }

    private Vector3 GetRandomDir()
    {
        var directions = new List<Vector3>
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };
        int randomDirIndex = UnityEngine.Random.Range(0, directions.Count);
        return directions[randomDirIndex];
    }

    private void Move(Vector3 moveDir)
    {
        Vector3 move = moveDir.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }
}
