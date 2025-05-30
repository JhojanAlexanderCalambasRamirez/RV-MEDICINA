using UnityEngine;

public class ResaltarCajon : MonoBehaviour
{
    public Renderer cajonRenderer;
    public Material defaultMaterial;
    public Material resaltadoMaterial;

    public void ActivarResaltado()
    {
        if (cajonRenderer != null && resaltadoMaterial != null)
            cajonRenderer.material = resaltadoMaterial;
    }

    public void DesactivarResaltado()
    {
        if (cajonRenderer != null && defaultMaterial != null)
            cajonRenderer.material = defaultMaterial;
    }
}
