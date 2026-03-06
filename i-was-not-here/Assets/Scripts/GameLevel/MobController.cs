using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MobController : MonoBehaviour
{
    private FovController fovController;

    [SerializeField] private float changeDirTimer = 2f;
    [SerializeField] private float moveSpeed = 1.5f;
    private List<GameObject> objectsInFov;

    private float currTimer;
    private Vector3 moveDir;

    private void Awake()
    {
        currTimer = changeDirTimer;
        moveDir = Vector3.zero;

        objectsInFov = new List<GameObject>();
        fovController = gameObject.GetComponentInChildren<FovController>();
    }


    private void Update()
    {
        bool areThereDetections = isFovDetection();

        if (areThereDetections)
        {;
            // Objects detected
            bool isPlayerInFov = false;
            int cntNepotrebstvo = 0;

            foreach (var obj in objectsInFov)
            {
                if (obj.GetComponent<PlayerController>())
                {
                    isPlayerInFov = true;
                    Move(GetDirToObject(obj));
                }
                else if (obj.GetComponent<Nepotrebstvo>())
                {
                    cntNepotrebstvo++;
                    if (isPlayerInFov)
                        Move(GetDirToObject(obj));
                }

            }

            if (isPlayerInFov)
            {
                if (cntNepotrebstvo > 0)
                    GameManager.Instance.ChangeRep((cntNepotrebstvo) * -0.2f);
                else
                    GameManager.Instance.ChangeRep(-0.05f);
            }

            currTimer = changeDirTimer;
        }
        else
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

    private Vector3 GetDirToObject(GameObject obj)
    {
        return (obj.transform.position - transform.position).normalized;
    }

    private bool isFovDetection()
    {
        objectsInFov = fovController.GetObjectsInFov();

        foreach (var obj in objectsInFov)
            if (obj.gameObject.GetComponent<PlayerController>() ||
                obj.gameObject.GetComponent<MobController>())
            {
                return true; 
            }
                
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
            GameManager.Instance.ChangeRep(-5f);
    }
}
