using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DetectorTapabocas : MonoBehaviour
{
    public string tapabocasTag = "Tapabocas";
    public NewBehaviourScript panelMisiones;  // Script donde marcas las tareas
    public int idMision = 2;

    private void OnTriggerEnter(Collider other)
    {
        // ✅ Validar que sea la misión actual
        if (panelMisiones == null || panelMisiones.misionActual != idMision) return;

        // ✅ Validar que colisiona con el objeto correcto
        if (!other.CompareTag(tapabocasTag)) return;

        // Soltar si está agarrado
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected)
        {
            grab.interactionManager.SelectExit(grab.firstInteractorSelecting, grab);
        }

        // ✅ Completar misión
        panelMisiones.CompletarTarea(idMision);

        // ✅ Destruir el objeto
        Destroy(other.gameObject);
    }
}
