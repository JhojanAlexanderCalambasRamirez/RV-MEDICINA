using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuManager : MonoBehaviour
{

    [Header("Botones VR")]
    public XRSimpleInteractable loginButton;  // Cambiado a XRSimpleInteractable para VR
    public XRSimpleInteractable registerButton;
    public XRSimpleInteractable backButtonLogin;
    public XRSimpleInteractable backButtonRegister;

    [Header("Paneles VR")]
    public GameObject mainMenuPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;
    public Transform uiAnchor; // Punto de anclaje para la UI

    [Header("Interactores VR")]
    public XRRayInteractor leftControllerInteractor;
    public XRRayInteractor rightControllerInteractor;

    [Header("Configuración VR")]
    public float uiDistance = 2f;
    public float uiHeightOffset = -0.3f;
    public float panelAngle = 15f; // Ligera inclinación para mejor visualización
    public Transform uiPivot; // Arrastra el UIPivot aquí en el Inspector
    public float defaultDistance = 2.0f;

    public static MenuManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetupVRMenu();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupVRMenu()
    {
        // Configurar posición inicial
        PositionMenuInView();

        // Configurar interactores
        ConfigureInteractors();

        // Estado inicial
        ShowMainMenu();
    }

    void PositionMenuInView()
    {
        if (Camera.main == null) return;

        // Distancia recomendada para VR (en metros)
        float preferredDistance = 2.0f;

        // Posición frente al usuario con offset vertical
        Vector3 uiPosition = Camera.main.transform.position +
                           (Camera.main.transform.forward * preferredDistance) +
                           (Vector3.up * -0.2f); // Ajuste vertical

        transform.position = uiPosition;
        FaceCamera();
    }

    void FaceCamera()
    {
        if (Camera.main == null) return;

        // Hacer que el menú mire al usuario con ligera inclinación
        Vector3 lookDirection = transform.position - Camera.main.transform.position;
        lookDirection.y = 0; // Mantener nivel horizontal

        transform.rotation = Quaternion.LookRotation(lookDirection);
        transform.Rotate(15f, 0, 0); // Inclinación hacia arriba
    }

    void ConfigureInteractors()
    {
        if (leftControllerInteractor != null)
        {
            leftControllerInteractor.uiHoverEntered.AddListener(OnUIHover);
            leftControllerInteractor.uiHoverExited.AddListener(OnUIHoverExit);
        }

        if (rightControllerInteractor != null)
        {
            rightControllerInteractor.uiHoverEntered.AddListener(OnUIHover);
            rightControllerInteractor.uiHoverExited.AddListener(OnUIHoverExit);
        }
    }

    void OnUIHover(UIHoverEventArgs args)
    {
        // Feedback visual al apuntar a botones
        if (args.uiObject.TryGetComponent<Button>(out var button))
        {
            button.transform.localScale = Vector3.one * 1.1f;
        }
    }

    void OnUIHoverExit(UIHoverEventArgs args)
    {
        if (args.uiObject.TryGetComponent<Button>(out var button))
        {
            button.transform.localScale = Vector3.one;
        }
    }
    void Start()
    {
        // Configurar posición inicial con más precisión
        PositionMenuInView();

        // Asegurar que el canvas mira correctamente a la cámara
        FaceCamera();
        // Configurar listeners
        loginButton.selectEntered.AddListener(_ => ShowLoginPanel());
        registerButton.selectEntered.AddListener(_ => ShowRegisterPanel());
        backButtonLogin.selectEntered.AddListener(_ => ShowMainMenu());
        backButtonRegister.selectEntered.AddListener(_ => ShowMainMenu());

        // Estado inicial
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        SetAllPanelsInactive();
        PositionUI();
        mainMenuPanel.SetActive(true);
    }

    public void ShowLoginPanel()
    {
        SetAllPanelsInactive();
        PositionUI(); // Misma posición para todos los paneles
        loginPanel.SetActive(true);
    }

    public void ShowRegisterPanel()
    {
        SetAllPanelsInactive();
        registerPanel.SetActive(true);
        PositionMenuInView();
    }

    private void SetAllPanelsInactive()
    {
        mainMenuPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }
    void PositionUI()
    {
        if (Camera.main == null || uiPivot == null) return;

        // Posiciona el pivot (padre de todos los paneles)
        uiPivot.position = Camera.main.transform.position +
                         (Camera.main.transform.forward * defaultDistance);

        // Rotación para que mire al usuario
        uiPivot.LookAt(Camera.main.transform);
        uiPivot.Rotate(0, 180f, 0); // Ajuste para que no esté al revés
    }
    public void RecenterMenu()
    {
        PositionMenuInView();
    }

    void OnDestroy()
    {
        // Limpiar listeners
        if (leftControllerInteractor != null)
        {
            leftControllerInteractor.uiHoverEntered.RemoveListener(OnUIHover);
            leftControllerInteractor.uiHoverExited.RemoveListener(OnUIHoverExit);
        }

        if (rightControllerInteractor != null)
        {
            rightControllerInteractor.uiHoverEntered.RemoveListener(OnUIHover);
            rightControllerInteractor.uiHoverExited.RemoveListener(OnUIHoverExit);
        }
    }
}
