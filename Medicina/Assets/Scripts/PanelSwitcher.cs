using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class PanelSwitcher : MonoBehaviour
{
    public enum PanelAction
    {
        ShowMain,
        ShowLogin,
        ShowRegister,
        HideCurrent
    }

    public PanelAction action;
    public GameObject mainPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    private XRSimpleInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);

        // Validación de referencias
        if (mainPanel == null) Debug.LogError("MainPanel no asignado", this);
        if (loginPanel == null && action == PanelAction.ShowLogin) Debug.LogError("LoginPanel no asignado", this);
        if (registerPanel == null && action == PanelAction.ShowRegister) Debug.LogError("RegisterPanel no asignado", this);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        switch (action)
        {
            case PanelAction.ShowMain:
                SetActivePanel(mainPanel);
                break;
            case PanelAction.ShowLogin:
                SetActivePanel(loginPanel);
                break;
            case PanelAction.ShowRegister:
                SetActivePanel(registerPanel);
                break;
            case PanelAction.HideCurrent:
                // Lógica adicional si necesitas ocultar el panel actual
                break;
        }
    }

    private void SetActivePanel(GameObject panelToShow)
    {
        // Desactivar todos los paneles primero
        mainPanel.SetActive(false);
        if (loginPanel != null) loginPanel.SetActive(false);
        if (registerPanel != null) registerPanel.SetActive(false);

        // Activar solo el panel deseado
        panelToShow.SetActive(true);
        Debug.Log($"Panel activado: {panelToShow.name}");
    }

    private void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnButtonPressed);
    }
}
