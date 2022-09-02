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
    public int colorCounter=0;
    public ParticleSystem ps;
    public List<Material> materials = new List<Material>();

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
            MovingCube.CurrentCube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB((colorCounter / 100f) % 1f, 1f, 1f));
            colorCounter++;
        }
    }
    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;
    }
    public void ComboCube()
    {
        print(lastCube-currentCube+"lastcurrent");
        float start = GameObject.Find("Start").transform.localScale.x;
        Color _color = materials[comboCounter].color;
        if (lastCube-currentCube<lastCube/40)
        {            
            comboCounter++;
            uIController.comboText.transform.DOScale(Vector3.one * 2f, 0.5f).OnComplete(() => uIController.comboText.transform.DOScale(Vector3.one,0.5f));
            ps.transform.DOMove(MovingCube.LastCube.transform.position,0.01f);
            ps.Play();
        }
        else
        {
            comboCounter = 0;
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
