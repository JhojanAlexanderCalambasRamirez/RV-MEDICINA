using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SoltarReloj : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    [Header("Opcional: Resaltado del caj�n")]
    public ResaltarCajon resaltarCajon;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Dejar el reloj cinem�tico al iniciar (sujeto a la mano sin f�sica)
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // Escuchar el evento de agarre
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Desvincular el reloj de la mano
        transform.parent = null;

        // Activar la f�sica
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        // Resaltar el caj�n (si est� asignado)
        if (resaltarCajon != null)
        {
            resaltarCajon.ActivarResaltado();
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }
}
