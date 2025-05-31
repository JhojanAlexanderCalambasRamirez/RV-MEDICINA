using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.XR.Interaction.Toolkit.UI;
public class LoginManager : MonoBehaviour
{
    [Header("Referencias VR")]
    public TMP_InputField usuarioInput;
    public TMP_InputField contrasenaInput;
    public TMP_Text txtFeedbackLogin;
    public XRUIInputModule vrInputModule;
    public Canvas vrCanvas;

    [Header("Configuración VR")]
    public float feedbackDuration = 3f; // Aumentado para mejor legibilidad en VR
    public float uiDistance = 2.5f;
    public Vector3 uiOffset = new Vector3(0, -0.2f, 0);

    private string dataPath;
    private AccountDatabase accountDatabase;
    private Coroutine feedbackCoroutine;

    void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "accounts.json");
        ConfigureVRUI();
        ClearFeedback();
    }

    void ConfigureVRUI()
    {
        if (vrCanvas != null)
        {
            vrCanvas.worldCamera = Camera.main;
            vrCanvas.planeDistance = uiDistance;

            // Posicionamiento relativo al jugador
            vrCanvas.transform.position = Camera.main.transform.position +
                                       Camera.main.transform.forward * uiDistance +
                                       uiOffset;
            vrCanvas.transform.rotation = Quaternion.LookRotation(
                vrCanvas.transform.position - Camera.main.transform.position);
        }
    }
    public void AttemptLogin()
    {
        if (string.IsNullOrEmpty(usuarioInput.text))
        {
            ShowFeedback("<b>Error:</b> Ingrese su correo o código", Color.red);
            TriggerHapticFeedback(0.5f, 0.2f);
            return;
        }

        if (string.IsNullOrEmpty(contrasenaInput.text))
        {
            ShowFeedback("<b>Error:</b> Ingrese su contraseña", Color.red);
            TriggerHapticFeedback(0.5f, 0.2f);
            return;
        }

        UserAccount account = FindAccount(usuarioInput.text, contrasenaInput.text);

        if (account != null)
        {
            ShowFeedback($"<color=green>¡Bienvenido, {account.nombre}!</color>", Color.green);
            TriggerHapticFeedback(0.7f, 0.5f); // Feedback positivo más largo

            SessionManager.Instance.Login(account);

            // Transición optimizada para VR
            StartCoroutine(CompleteLoginProcess(account));
        }
        else
        {
            ShowFeedback("<b>Error:</b> Credenciales incorrectas", Color.red);
            TriggerHapticFeedback(0.5f, 0.3f);
            contrasenaInput.text = "";
        }
    }

    IEnumerator CompleteLoginProcess(UserAccount account)
    {
        yield return new WaitForSeconds(1.5f); // Tiempo para leer el mensaje
        usuarioInput.text = "";
        contrasenaInput.text = "";

        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.ShowMainMenu();
        }
    }
    void TriggerHapticFeedback(float amplitude, float duration)
    {
        // Implementar según el SDK de VR (Oculus, OpenXR, etc.)
        // Ejemplo para Oculus:
        // OVRInput.SetControllerVibration(1, amplitude, OVRInput.Controller.RTouch);
        // Invoke("StopHaptics", duration);
    }

    void StopHaptics()
    {
        // OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }

    void ShowFeedback(string message, Color color)
    {
        if (txtFeedbackLogin != null)
        {
            if (feedbackCoroutine != null) StopCoroutine(feedbackCoroutine);

            txtFeedbackLogin.text = message;
            txtFeedbackLogin.color = color;
            txtFeedbackLogin.fontSize = 20; // Tamaño aumentado para VR
            txtFeedbackLogin.gameObject.SetActive(true);

            feedbackCoroutine = StartCoroutine(HideFeedbackAfterDelay());
        }
    }

    IEnumerator HideFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(feedbackDuration);
        ClearFeedback();
    }

    void ClearFeedback()
    {
        if (txtFeedbackLogin != null)
        {
            txtFeedbackLogin.text = "";
            txtFeedbackLogin.gameObject.SetActive(false);
        }
    }

    private UserAccount FindAccount(string usuario, string contrasena)
    {
        if (!File.Exists(dataPath))
        {
            Debug.LogWarning("[LoginManager] No se encontró archivo de cuentas");
            return null;
        }

        try
        {
            string jsonData = File.ReadAllText(dataPath);
            accountDatabase = JsonUtility.FromJson<AccountDatabase>(jsonData);

            foreach (UserAccount account in accountDatabase.accounts)
            {
                bool usuarioMatch = account.correo == usuario || account.codigoEstudiantil == usuario;
                bool contrasenaMatch = account.contrasena == contrasena;

                if (usuarioMatch && contrasenaMatch)
                {
                    return account;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[LoginManager] Error al leer cuentas: {e.Message}");
        }

        return null;
    }

    void SuccessfulLogin(UserAccount account)
    {
        ShowFeedback($"¡Bienvenido, {account.nombre}!", Color.green);

        // Notificar al SessionManager
        SessionManager.Instance.Login(account);

        // Limpiar campos
        usuarioInput.text = "";
        contrasenaInput.text = "";

        // Volver al menú principal
        FindObjectOfType<MenuManager>().ShowMainMenu();
    }

    void HideLoginPanel()
    {
        MenuManager menuManager = FindObjectOfType<MenuManager>();
        if (menuManager != null)
        {
            menuManager.ShowMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        // Limpiar campos al regresar
        usuarioInput.text = "";
        contrasenaInput.text = "";

        MenuManager menuManager = FindObjectOfType<MenuManager>();
        if (menuManager != null)
        {
            menuManager.ShowMainMenu();
        }
    }

    //void ShowMessage(string message)
    //{
    //    if (messageBox == null || messageText == null) return;

    //    // Cancelar cualquier mensaje previo
    //    CancelInvoke("HideMessage");

    //    messageText.text = message;
    //    messageBox.SetActive(true);
    //    isShowingMessage = true;

    //    Invoke("HideMessage", messageDuration);
    //}

    //void HideMessage()
    //{
    //    if (messageBox != null)
    //    {
    //        messageBox.SetActive(false);
    //    }
    //    isShowingMessage = false;
    //}
}
