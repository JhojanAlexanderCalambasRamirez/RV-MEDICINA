using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapAlCajon : MonoBehaviour
{
    public Transform snapPoint;
    public string relojTag = "Reloj";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(relojTag)) return;

        Debug.Log("Reloj detectado. Aplicando Snap y desactivando interacci�n.");

        // Soltar autom�ticamente si est� siendo agarrado
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected)
        {
            // Forzar a soltarlo
            grab.interactionManager.SelectExit(grab.firstInteractorSelecting, grab);
        }

        other.transform.SetParent(null);

        // Posicionar en el punto del caj�n
        other.transform.position = snapPoint.position;
        other.transform.rotation = snapPoint.rotation;

        // Congelar f�sica
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Desactivar interacci�n para que no se pueda volver a agarrar
        if (grab != null)
        {
            grab.enabled = false;
        }

        // Opcional: quitar colisionadores si quieres que no interfiera m�s
        // Collider col = other.GetComponent<Collider>();
        // if (col != null) col.enabled = false;
    }
}
