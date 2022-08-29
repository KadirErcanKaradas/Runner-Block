using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;
    public float fallCube;
    public float currentCube;
    public float lastCube;
    public UIController uIController;
    public int comboCounter = 0;

    public static GameManager Instance;
    public GameStage GameStage { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        spawners = FindObjectsOfType<CubeSpawner>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && GameStage == GameStage.Started)
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            ComboCube();
        }
    }
    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;
    }
    public void ComboCube()
    {
        float start = GameObject.Find("Start").transform.localScale.x;
        if (lastCube - currentCube < 0.05f)
        {
            print(lastCube - currentCube);
            comboCounter++;
            uIController.comboText.transform.DOScale(Vector3.one * 2f, 0.5f).OnComplete(() => uIController.comboText.transform.DOScale(Vector3.one,0.5f));
            MovingCube.CurrentCube.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }
        else if (lastCube - currentCube < 0.01f)
        {
            comboCounter++;
            uIController.comboText.transform.DOScale(Vector3.one * 2f, 0.5f).OnComplete(() => uIController.comboText.transform.DOScale(Vector3.one, 0.5f));
            MovingCube.CurrentCube.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
        }
        else
        {
            comboCounter = 0;
            MovingCube.CurrentCube.GetComponent<MeshRenderer>().materials[0].color = GameObject.Find("Start").GetComponent<MeshRenderer>().materials[0].color;
        }
    }
    public void StartCube()
    {
        if (MovingCube.CurrentCube != null)
            MovingCube.CurrentCube.Stop();

        spawnerIndex = spawnerIndex == 0 ? 1 : 0;
        currentSpawner = spawners[spawnerIndex];

        currentSpawner.SpawnCube();
    }
}
public enum GameStage
{
    NotLoaded, Loaded, Started, Win,WinCube, Fail,FailFall
}
