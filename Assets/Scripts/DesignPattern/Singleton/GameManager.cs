using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private EnemySample enemySample;
    [SerializeField] private LocomotionSystem playerLocomotion;
    [SerializeField] private TextMeshProUGUI gameStartText;
    [SerializeField] private GameObject gameEnd;
    [SerializeField] private GameObject gameWin;

    private void Awake()
    {
        if(instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        enemySample.enabled = false;
        playerLocomotion.enabled = false;
        gameEnd.SetActive(false);
        gameWin.SetActive(false);

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        gameStartText.text = "3";
        yield return new WaitForSeconds(1f); 
        gameStartText.text = "2";
        yield return new WaitForSeconds(1f);
        gameStartText.text = "1";
        yield return new WaitForSeconds(1f);
        gameStartText.text = "Game Start!";
        enemySample.enabled = true;
        playerLocomotion.enabled = true;
    }

    public void ShowEnd()
    {
        gameEnd.SetActive(true);
    }

    public void ShowWin()
    {
        gameWin.SetActive(true);
    }

}
