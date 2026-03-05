using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    private FovController fovController;

    [SerializeField] private float changeDirTimer = 10f;
    [SerializeField] private float moveSpeed = 2f;
    private List<GameObject> objectsInFov;

    private float currTimer;
    private Vector3 moveDir;

    private void Awake()
    {
        currTimer = 0f;
        moveDir = Vector3.zero;

        objectsInFov = new List<GameObject>();
        fovController = gameObject.GetComponentInChildren<FovController>();
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
        int dirIndex = Random.Range(0, 4);

        switch (dirIndex)
        {
            case 0: return Vector3.up;
            case 1: return Vector3.down;
            case 2: return Vector3.left;
            case 3: return Vector3.right;
            default: return Vector3.zero;
        }
    }

    private void Move(Vector3 moveDir)
    {
        Vector3 move = moveDir.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }


    public void CehckFov()
    {
        objectsInFov = fovController.GetObjectsInFov();

        bool isPlayerInFov = false;
        int cntNepotrebstvo = 0;

        foreach (var obj in objectsInFov)
        {
            if (obj.GetComponent<PlayerController>())
                isPlayerInFov = true;
            else if (obj.GetComponent<Nepotrebstvo>())
                cntNepotrebstvo++;
        }

        if (isPlayerInFov)
        {
            if (cntNepotrebstvo > 0)
                // GameManager -> ChangeRep(-x * cntNepotrbstvo)
                Debug.Log("Player and nepotrebstvo");
            else
                // Follow player
                Debug.Log("Following player");
        }
        else if (cntNepotrebstvo > 0)
            // Move to nepotrebstvo
            Debug.Log("Move to nepotrebstvo");
    }
}
