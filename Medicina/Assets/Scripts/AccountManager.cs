using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class AccountManager : MonoBehaviour
{
    [Header("Referencias VR")]
    public TMP_InputField nombreInput;
    public TMP_InputField correoInput;
    public TMP_InputField codigoInput;
    public TMP_InputField contrasenaInput;
    public TMP_Text txtFeedbackRegistro;
    public XRUIInputModule vrInputModule; // Módulo de entrada para VR

    [Header("Configuración VR")]
    public float feedbackDuration = 2.5f;
    public float uiDistance = 2f; // Distancia recomendada para UI en VR
    public Vector3 uiOffset = new Vector3(0, -0.3f, 0); // Ajuste de altura

    private string dataPath;
    private AccountDatabase accountDatabase;
    private Coroutine feedbackCoroutine;
    
    public static AccountManager Instance { get; private set; }
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupVRUI();
            LoadAccounts();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void SetupVRUI()
    {
        // Configuración inicial para VR
        GameObject uiCanvas = GetComponentInChildren<Canvas>().gameObject;
        uiCanvas.transform.position = Camera.main.transform.position +
                                    Camera.main.transform.forward * uiDistance +
                                    uiOffset;
        uiCanvas.transform.rotation = Quaternion.LookRotation(
            uiCanvas.transform.position - Camera.main.transform.position);
    }
    public void CreateAccount()
    {
        // Validar campos vacíos
        if (string.IsNullOrEmpty(nombreInput.text) ||
           string.IsNullOrEmpty(correoInput.text) ||
           string.IsNullOrEmpty(codigoInput.text) ||
           string.IsNullOrEmpty(contrasenaInput.text))
        {
            ShowFeedback("Por favor, complete todos los campos", Color.red);
            return;
        }

        // Verificar si el correo ya existe
        foreach (UserAccount account in accountDatabase.accounts)
        {
            if (account.correo == correoInput.text)
            {
                ShowFeedback("Este correo ya está registrado", Color.red);
                return;
            }
        }

        // Verificar si el código estudiantil ya existe
        foreach (UserAccount account in accountDatabase.accounts)
        {
            if (account.codigoEstudiantil == codigoInput.text)
            {
                ShowFeedback("Este código estudiantil ya está registrado", Color.red);
                return;
            }
        }

        // Crear nueva cuenta
        UserAccount newAccount = new UserAccount(
            nombreInput.text,
            correoInput.text,
            codigoInput.text,
            contrasenaInput.text
        );

        accountDatabase.accounts.Add(newAccount);
        SaveAccounts();

        ShowFeedback("¡Cuenta creada exitosamente!", Color.green);
        ClearFieldsAndReturn();
    }
    void ShowFeedback(string message, Color color)
    {
        if (txtFeedbackRegistro != null)
        {
            // Detener la corrutina anterior si existe
            if (feedbackCoroutine != null)
            {
                StopCoroutine(feedbackCoroutine);
            }

            txtFeedbackRegistro.text = message;
            txtFeedbackRegistro.color = color;
            txtFeedbackRegistro.gameObject.SetActive(true);

            // Ocultar después de un tiempo
            feedbackCoroutine = StartCoroutine(HideFeedbackAfterDelay());
        }
    }

    System.Collections.IEnumerator HideFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(feedbackDuration);
        ClearFeedback();
    }

    void ClearFeedback()
    {
        if (txtFeedbackRegistro != null)
        {
            txtFeedbackRegistro.text = "";
            txtFeedbackRegistro.gameObject.SetActive(false);
        }
    }

    void ClearFieldsAndReturn()
    {
        // Limpiar campos
        nombreInput.text = "Inserte Nombre...";
        correoInput.text = "Inserte Correo...";
        codigoInput.text = "Inserte Código...";
        contrasenaInput.text = "Inserte Contraseña...";

        // Regresar al menú principal
        ReturnToMainMenu();


    }

    void ReturnToMainMenu()
    {
        MenuManager menuManager = FindObjectOfType<MenuManager>();
        if (menuManager != null)
        {
            menuManager.ShowMainMenu();
        }
    }

    void LoadAccounts()
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            accountDatabase = JsonUtility.FromJson<AccountDatabase>(jsonData);
        }
        else
        {
            accountDatabase = new AccountDatabase();
        }
    }

    public void SaveAccounts()
    {
        string jsonData = JsonUtility.ToJson(accountDatabase, true);
        File.WriteAllText(dataPath, jsonData);
        Debug.Log("[AccountManager] Cuenta guardada: " + jsonData);
    }

   
}

