using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
public class LoginManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_InputField usuarioInput;
    public TMP_InputField contrasenaInput;
    public TMP_Text txtFeedbackLogin;

    [Header("Configuración")]
    public float feedbackDuration = 2.5f;

    private string dataPath;
    private AccountDatabase accountDatabase;
    private Coroutine feedbackCoroutine;

    void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "accounts.json");
        ClearFeedback();
    }

    public void AttemptLogin()
    {
        string usuario = usuarioInput.text.Trim();
        string contrasena = contrasenaInput.text;

        // Validación básica
        if (string.IsNullOrEmpty(usuario))
        {
            ShowFeedback("Ingrese su correo o código", Color.red);
            return;
        }

        if (string.IsNullOrEmpty(contrasena))
        {
            ShowFeedback("Ingrese su contraseña", Color.red);
            return;
        }


        UserAccount account = FindAccount(usuarioInput.text, contrasenaInput.text);

        if (account != null)
        {
            ShowFeedback($"¡Bienvenido, {account.nombre}!", Color.green);

            // Notificar al SessionManager y habilitar el botón
            SessionManager.Instance.Login(account);

            // Limpiar campos y volver al menú
            usuarioInput.text = "";
            contrasenaInput.text = "";
            FindObjectOfType<MenuManager>().ShowMainMenu();
        }
        else
        {
            ShowFeedback("Credenciales incorrectas", Color.red);
            contrasenaInput.text = "";
        }
    }

    void ShowFeedback(string message, Color color)
    {
        if (txtFeedbackLogin != null)
        {
            // Detener la corrutina anterior si existe
            if (feedbackCoroutine != null)
            {
                StopCoroutine(feedbackCoroutine);
            }

            txtFeedbackLogin.text = message;
            txtFeedbackLogin.color = color;
            txtFeedbackLogin.gameObject.SetActive(true);

            // Ocultar después de un tiempo
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
