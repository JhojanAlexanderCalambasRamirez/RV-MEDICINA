using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapAlCajon : MonoBehaviour
{
    public Transform snapPoint;
    public string relojTag = "Reloj";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(relojTag)) return;

        Debug.Log("Reloj detectado. Aplicando Snap y desactivando interacción.");

        // Soltar automáticamente si está siendo agarrado
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected)
        {
            // Forzar a soltarlo
            grab.interactionManager.SelectExit(grab.firstInteractorSelecting, grab);
        }

        other.transform.SetParent(null);

        // Posicionar en el punto del cajón
        other.transform.position = snapPoint.position;
        other.transform.rotation = snapPoint.rotation;

        // Congelar física
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Desactivar interacción para que no se pueda volver a agarrar
        if (grab != null)
        {
            grab.enabled = false;
        }

        // Opcional: quitar colisionadores si quieres que no interfiera más
        // Collider col = other.GetComponent<Collider>();
        // if (col != null) col.enabled = false;
    }
}
