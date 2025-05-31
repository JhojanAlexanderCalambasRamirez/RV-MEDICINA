using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject mainMenuPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    [Header("Botones")]
    public Button loginButton;
    public Button registerButton;
    public Button backButtonLogin;
    public Button backButtonRegister;

    void Start()
    {
        // Configurar listeners
        loginButton.onClick.AddListener(ShowLoginPanel);
        registerButton.onClick.AddListener(ShowRegisterPanel);
        backButtonLogin.onClick.AddListener(ShowMainMenu);
        backButtonRegister.onClick.AddListener(ShowMainMenu);

        // Estado inicial
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        SetAllPanelsInactive();
        mainMenuPanel.SetActive(true);
        Debug.Log("[MenuManager] Mostrando menú principal");
    }

    public void ShowLoginPanel()
    {
        SetAllPanelsInactive();
        loginPanel.SetActive(true);
        Debug.Log("[MenuManager] Mostrando panel de login");
    }

    public void ShowRegisterPanel()
    {
        SetAllPanelsInactive();
        registerPanel.SetActive(true);
        Debug.Log("[MenuManager] Mostrando panel de registro");
    }

    private void SetAllPanelsInactive()
    {
        mainMenuPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }
}
