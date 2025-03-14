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
        // ��ư �̺�Ʈ ����
        startButton.onClick.AddListener(StartGame);
        shopButton.onClick.AddListener(OpenShop);
        backButton.onClick.AddListener(CloseShop);

        // �ʱ� ���� ����
        lobbyPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    void StartGame()
    {
        Debug.Log("���� ����!");
        SceneManager.LoadScene("GameScene"); // ���� ������ ��ȯ (GameScene�� �̸� ������ ��)
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
