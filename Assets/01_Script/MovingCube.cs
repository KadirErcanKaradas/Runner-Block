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
    [SerializeField] private bool isMove= true;
    private Tween tween;

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x,transform.localScale.y,LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0,1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }
    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.x : LastCube.transform.localScale.x;
        if (Mathf.Abs(hangover) >= LastCube.transform.localScale.z)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangover > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
            SplitCubeOnZ(hangover, direction);
        else
            SplitCubeOnX(hangover, direction);
        LastCube = this;
    }
    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.x - LastCube.transform.position.x;
        else
            return transform.position.x - LastCube.transform.position.x;
    }
    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition,transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }
    private void SplitCubeOnZ(float hangover,float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
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
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            if (transform.position.x >= 2f) 
            {
                //transform.position += -transform.right * Time.deltaTime * moveSpeed;
                transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * moveSpeed);
            } 
           if (transform.position.x<=-2f)
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed);
            }
        }
        else
        {
     
        }  
    }
    //private void Start()
    //{
    //    StartCoroutine(MovementTween());
    //}
    //private IEnumerator MovementTween()
    //{
        
    //    yield return new WaitForSeconds(0.05f);
    //    while (isMove)
    //    {
    //        tween.Kill();
    //        tween = transform.DOMoveX(-2, 1).OnComplete(() => transform.DOMoveX(2, 1));
    //        yield return new WaitForSeconds(2.05f);
    //    }

    //}
}
