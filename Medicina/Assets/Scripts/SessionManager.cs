using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [Header("Referencias VR")]
    public XRSimpleInteractable playInteractable; // Reemplazo del Button tradicional
    public Material enabledMaterial; // Material cuando est� habilitado
    public Material disabledMaterial; // Material cuando est� deshabilitado
    public string gameSceneName = "VR_GameScene"; // Escena espec�fica para VR

    [Header("Configuraci�n VR")]
    public float hapticAmplitude = 0.5f;
    public float hapticDuration = 0.3f;

    [HideInInspector]
    public UserAccount currentUser;
    [HideInInspector]
    public bool isLoggedIn = false;

    private Renderer buttonRenderer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Aplicar DontDestroyOnLoad al padre ra�z
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void InitializeVRComponents()
    {
        if (playInteractable != null)
        {
            buttonRenderer = playInteractable.GetComponent<Renderer>();
            playInteractable.selectEntered.AddListener(_ => StartGameVR());
            UpdatePlayButtonState();
        }
    }
    public void Login(UserAccount user)
    {
        currentUser = user;
        isLoggedIn = true;
        UpdatePlayButtonState();
        TriggerHapticFeedback(hapticAmplitude, hapticDuration);
    }

    public void Logout()
    {
        currentUser = null;
        isLoggedIn = false;
        UpdatePlayButtonState();
    }

    public void StartGameVR() // Versi�n adaptada para VR
    {
        if (!isLoggedIn)
        {
            Debug.LogWarning("Intento de iniciar juego sin sesi�n activa");
            TriggerHapticFeedback(0.7f, 0.5f); // Feedback de error m�s fuerte
            return;
        }

        Debug.Log($"Cargando escena VR: {gameSceneName}");
        SaveUserData();

        // Transici�n optimizada para VR
        SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Single);
    }
    void UpdatePlayButtonState()
    {
        if (playInteractable != null && buttonRenderer != null)
        {
            playInteractable.enabled = isLoggedIn;

            // Cambio visual basado en materiales
            buttonRenderer.material = isLoggedIn ? enabledMaterial : disabledMaterial;

            // Escalado para feedback visual
            playInteractable.transform.localScale = isLoggedIn ?
                Vector3.one * 1.05f :
                Vector3.one * 0.95f;
        }
    }
    void TriggerHapticFeedback(float amplitude, float duration)
    {
        // Implementaci�n espec�fica seg�n SDK VR
        // Ejemplo para Oculus:
        // OVRInput.SetControllerVibration(1, amplitude, OVRInput.Controller.RTouch);
        // Invoke(nameof(StopHaptics), duration);
    }

    void StopHaptics()
    {
        // OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }

    void SaveUserData()
    {
        if (AccountManager.Instance != null)
        {
            AccountManager.Instance.SaveAccounts();
        }
    }
}
