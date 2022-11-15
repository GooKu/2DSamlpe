using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTrans;
    [SerializeField]
    private Vector3 offset = new Vector3(1, 1, 1);

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.position = playerTrans.position + offset;
    }
}
