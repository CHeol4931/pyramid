using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public GameObject lobbyPanel;
    public GameObject shopPanel;

    public Button startButton;
    public Button shopButton;
    public Button backButton;

    void Start()
    {
        // 버튼 이벤트 연결
        startButton.onClick.AddListener(StartGame);
        shopButton.onClick.AddListener(OpenShop);
        backButton.onClick.AddListener(CloseShop);

        // 초기 상태 설정
        lobbyPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    void StartGame()
    {
        Debug.Log("게임 시작!");
        SceneManager.LoadScene("GameScene"); // 게임 씬으로 전환 (GameScene은 미리 만들어야 함)
    }

    void OpenShop()
    {
        lobbyPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    void CloseShop()
    {
        shopPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
}
