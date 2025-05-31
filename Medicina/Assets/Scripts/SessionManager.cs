using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [Header("Referencias")]
    public Button playButton;
    public string gameSceneName = "GameScene";

    [Header("Configuración de Escenas")]
    public string sceneToLoad = "MainGameScene"; // Nombre exacto de tu escena

    [HideInInspector]
    public UserAccount currentUser;
    [HideInInspector]
    public bool isLoggedIn = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persistencia entre escenas

            // Inicialización
            isLoggedIn = false;
            UpdatePlayButtonState();
        }
        else
        {
            // Destruye cualquier copia accidental
            Destroy(gameObject);
            return;  // Importante para evitar inicialización duplicada
        }

        UpdatePlayButtonState();
    }

    public void Login(UserAccount user)
    {
        currentUser = user;
        isLoggedIn = true;
        UpdatePlayButtonState();

        // Debug para verificar
        Debug.Log($"Login realizado - Botón Play habilitado: {playButton.interactable}");
    }

    public void Logout()
    {
        currentUser = null;
        isLoggedIn = false;
        UpdatePlayButtonState();
    }

    public void StartGame()
    {
        if (!isLoggedIn)
        {
            Debug.LogWarning("Intento de iniciar juego sin sesión activa");
            return;
        }

        Debug.Log($"Cargando escena: {sceneToLoad} para usuario: {currentUser.nombre}");

        // Guardar datos antes de cambiar de escena
        SaveUserData();

        // Cargar la escena
        SceneManager.LoadScene(sceneToLoad);
    }

    void UpdatePlayButtonState()
    {
        if (playButton != null)
        {
            playButton.interactable = isLoggedIn;

            // Opcional: Cambiar color visualmente
            var colors = playButton.colors;
            colors.normalColor = isLoggedIn ? Color.green : Color.gray;
            playButton.colors = colors;
        }
        else
        {
            Debug.LogWarning("PlayButton no asignado en SessionManager");
        }
    }

    void SaveUserData()
    {
        // Verificación segura de AccountManager
        if (AccountManager.Instance != null)
        {
            AccountManager.Instance.SaveAccounts();
            Debug.Log("Datos del usuario guardados correctamente");
        }
        else
        {
            Debug.LogError("AccountManager no está disponible");
            // Opcional: Cargar AccountManager manualmente si es necesario
            // AccountManager.Instance = FindObjectOfType<AccountManager>();
        }
    }
}
