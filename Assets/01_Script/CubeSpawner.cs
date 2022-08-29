using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;
    [SerializeField] private MoveDirection moveDirection;
    [SerializeField] private GameObject parentCube;

    public void SpawnCube()
    {
        if (GameManager.Instance.GameStage == GameStage.Started)
        {
            var cube = Instantiate(cubePrefab);
            cube.transform.parent = parentCube.transform;

            if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start"))
            {
                cube.transform.position = new Vector3(transform.position.x,
                transform.position.y,
                MovingCube.LastCube.transform.position.z + cubePrefab.transform.localScale.z);
            }
            else
            {
                cube.transform.position = transform.position;
            }
            cube.MoveDirection = moveDirection;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
