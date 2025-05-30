using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColocarGuantes : MonoBehaviour
{
    public Renderer manoIzquierdaRenderer;
    public Renderer manoDerechaRenderer;
    public Material materialGuantes;
    public NewBehaviourScript panelMisiones; // Script de misiones
    public int idMision = 4;

    private XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGuantesAgarrados);
    }

    void OnGuantesAgarrados(SelectEnterEventArgs args)
    {
        // Validar si es la misión actual
        if (panelMisiones == null || panelMisiones.misionActual != idMision)
        {
            Debug.Log("⛔ No puedes usar los guantes aún.");
            return;
        }

        // Cambiar material de las manos
        if (manoIzquierdaRenderer != null)
            manoIzquierdaRenderer.material = materialGuantes;

        if (manoDerechaRenderer != null)
            manoDerechaRenderer.material = materialGuantes;

        // Ocultar guantes del mundo
        gameObject.SetActive(false);

        // Marcar la tarea como completada
        panelMisiones.CompletarTarea(idMision);

        Debug.Log("✅ ¡Misión 4 completada: Guantes puestos!");
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGuantesAgarrados);
    }
}
