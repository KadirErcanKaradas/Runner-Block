using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject TapToStart;
    [SerializeField] private GameObject FailPanel;
    [SerializeField] private GameObject WinPanel;

    public TMP_Text comboText;
    void Start()
    {
        comboText.text = "+" + GameManager.Instance.comboCounter;
    }
    void Update()
    {
        if (GameManager.Instance.GameStage == GameStage.Fail || GameManager.Instance.GameStage == GameStage.FailFall)
        {
            StartCoroutine(Fail());
        }
        if (GameManager.Instance.GameStage == GameStage.Win)
        {
            StartCoroutine(WinDance());
        }
        comboText.text = "+" + GameManager.Instance.comboCounter;
    }
    public void TapToStartButton()
    {
        GameManager.Instance.SetGameStage(GameStage.Started);
        TapToStart.SetActive(false);
        GameManager.Instance.StartCube();

    }
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
    public IEnumerator WinDance()
    {
        yield return new WaitForSeconds(1.5f);
        WinPanel.SetActive(true);
    }
    public IEnumerator Fail()
    {
        yield return new WaitForSeconds(1.5f);
        FailPanel.SetActive(true);
    }
}
