using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenu : MonoBehaviour
{
    public XRSimpleInteractable playButton;
    public XRSimpleInteractable registerButton;
    public XRSimpleInteractable loginButton;
    
    public GameObject mainPanel;
    public GameObject registerPanel;
    public GameObject loginPanel;
    
    private void Start()
    {
        // Configurar eventos de los botones
        playButton.selectEntered.AddListener(_ => PlayGame());
        registerButton.selectEntered.AddListener(_ => ShowRegister());
        loginButton.selectEntered.AddListener(_ => ShowLogin());
        
        // Mostrar panel principal al inicio
        ShowMainMenu();
    }
    
    private void PlayGame()
    {
        Debug.Log("Iniciando juego...");
        // Aquí iría la lógica para cargar la escena del juego
    }
    
    private void ShowRegister()
    {
        mainPanel.SetActive(false);
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
    
    private void ShowLogin()
    {
        mainPanel.SetActive(false);
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
    
    public void ShowMainMenu()
    {
        mainPanel.SetActive(true);
        registerPanel.SetActive(false);
        loginPanel.SetActive(false);
    }
}
