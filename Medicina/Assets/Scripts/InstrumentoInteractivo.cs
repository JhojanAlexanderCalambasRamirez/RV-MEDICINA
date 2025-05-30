using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InstrumentoInteractivo : MonoBehaviour
{
    [Header("Datos del instrumento")]
    public InfoInstrumento info;

    [Header("Contorno")]
    public Renderer rendererDelObjeto; // Renderer del objeto 3D
    public Material materialContorno;  // Material personalizado con shader de contorno

    private Material[] materialesOriginales;

    private void Start()
    {
#if UNITY_EDITOR
        // Añadir BoxCollider en modo editor si no existe
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
#endif

        // Guardar materiales originales
        if (rendererDelObjeto == null)
            rendererDelObjeto = GetComponent<Renderer>();

        if (rendererDelObjeto != null)
            materialesOriginales = rendererDelObjeto.materials;
    }

    public void ActivarInfo()
    {
        UIManagerInstrumentos.Instance.MostrarInfo(info, this.transform.position);
        MostrarContorno(true);
    }

    public void MostrarContorno(bool activo)
    {
        if (rendererDelObjeto == null || materialContorno == null)
            return;

        if (activo)
        {
            // Agrega el material de contorno
            Material[] nuevos = new Material[materialesOriginales.Length + 1];
            materialesOriginales.CopyTo(nuevos, 0);
            nuevos[nuevos.Length - 1] = materialContorno;
            rendererDelObjeto.materials = nuevos;
        }
        else
        {
            // Restaura materiales originales
            rendererDelObjeto.materials = materialesOriginales;
        }
    }

#if UNITY_EDITOR
    // Activar con clic durante pruebas en editor
    private void OnMouseDown()
    {
        ActivarInfo();
    }
#endif
}
