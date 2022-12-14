using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow _cameraFollow;
    private Transform target;
    private Vector3 offset;
    public GameObject player;
    public bool notFinishforCamMove;
    public float smoothSpeed = 0.1f;

    private void Start()
    {
        _cameraFollow = this;
        notFinishforCamMove = true;
        target = player.transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        
        if (GameManager.Instance.GameStage== GameStage.Win)
           { 
               transform.RotateAround(target.transform.position, Vector3.up, -50 * Time.deltaTime);
           }

        if (GameManager.Instance.GameStage == GameStage.Started)
        {
            SmoothFollow();
        }
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, targetPos.z);
    }
}