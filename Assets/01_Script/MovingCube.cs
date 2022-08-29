using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get;  set; }

    [SerializeField] private float moveSpeed=1f;

    private int direction = -1;

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        CurrentCube = this;

        transform.localScale = new Vector3(LastCube.transform.localScale.x,transform.localScale.y,LastCube.transform.localScale.z);
        GameManager.Instance.currentCube = LastCube.transform.localScale.x;
    }
    internal void Stop()
    {
        if (GameManager.Instance.GameStage == GameStage.Started)
        {
            moveSpeed = 0;
            float hangover = transform.position.x - LastCube.transform.position.x;
            if (Mathf.Abs(hangover) >= LastCube.transform.localScale.z)
            {
                LastCube = null;
                CurrentCube = null;
            }

            float direction = hangover > 0 ? 1f : -1f;

            if (MoveDirection == MoveDirection.Z)
                SplitCubeOnZ(hangover, direction);
            else
                SplitCubeOnX(hangover, direction);
            LastCube = this;
        }   
    }
    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        if (newXSize<0)
        {
            GameManager.Instance.SetGameStage(GameStage.Fail);
            gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            float fallingBlockSize = transform.localScale.x - newXSize;
            GameManager.Instance.fallCube = fallingBlockSize;
            float newXPosition = LastCube.transform.position.x + (hangover / 2);
            transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

            float cubeEdge = transform.position.x + (newXSize / 2f * direction);
            float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

            SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
        }
        
    }
    private void SplitCubeOnZ(float hangover,float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        if (newXSize < 0)
        {
            GameManager.Instance.SetGameStage(GameStage.Fail);
            gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            float fallingBlockSize = transform.localScale.x - newXSize;
            float newXPosition = LastCube.transform.position.x + (hangover / 2);
            transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

            float cubeEdge = transform.position.x + (newXSize / 2f * direction);
            float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

            SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
        }
    }

    private void SpawnDropCube(float fallingBlockZPosition,float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize,transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition , transform.position.y, transform.position.z);
        }
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<MeshRenderer>().materials[0].color = LastCube.GetComponent<MeshRenderer>().materials[0].color;
        Destroy(cube.gameObject, 1f);
    }
    public void ComboCounter()
    {
        if (LastCube.transform.position.x < CurrentCube.transform.position.x)
        {
            
        }
        
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z && GameManager.Instance.GameStage == GameStage.Started)
        {
            if (transform.position.x >= 3f)
            {
                direction = 1;

            }
            else if (transform.position.x <= -3f)
            {
                direction = -1;
            }
            transform.Translate(direction * Vector3.left * Time.deltaTime * moveSpeed, Space.World);

        }
        else if (MoveDirection == MoveDirection.X && GameManager.Instance.GameStage == GameStage.Started)
        {
            if (transform.position.x >= 3f)
            {
                direction = -1;

            }
            else if (transform.position.x <= -3f)
            {
                direction = 1;
            }
            transform.Translate(direction * Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
        GameManager.Instance.lastCube = LastCube.transform.localScale.x;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.SetGameStage(GameStage.WinCube);
        }
    }
}
