using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class InstrumentoInteractivo : MonoBehaviour
{
    [Header("Datos del instrumento")]
    public InfoInstrumento info;

    [Header("Contorno visual")]
    public Renderer rendererDelObjeto;
    public Material materialContorno;
    private Material[] materialesOriginales;

    [Header("UI Propia del Instrumento")]
    public GameObject canvasInfo;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoDescripcion;

    private bool yaFueVisto = false; // ← esto es clave

    private void Start()
    {
        if (rendererDelObjeto == null)
            rendererDelObjeto = GetComponent<Renderer>();

        if (rendererDelObjeto != null)
            materialesOriginales = rendererDelObjeto.materials;

        if (canvasInfo != null)
            canvasInfo.SetActive(false);
    }

    public void OnSeleccionXR(BaseInteractionEventArgs args)
    {
        ActivarInfo();
    }

    public void ActivarInfo()
    {
        OcultarTodosLosCanvas();

        if (canvasInfo != null)
        {
            textoTitulo.text = info.nombre;
            textoDescripcion.text = info.descripcion;

            Vector3 objetivo = Camera.main.transform.position;
            Vector3 direccion = new Vector3(objetivo.x, canvasInfo.transform.position.y, objetivo.z);
            canvasInfo.transform.LookAt(direccion);
            canvasInfo.transform.Rotate(0, 180, 0);

            canvasInfo.SetActive(true);

            if (!yaFueVisto)
            {
                yaFueVisto = true;
                ContadorInstrumentosUI.Instance?.RegistrarInteraccion();
            }
        }

        MostrarContorno(true);
    }

    public void MostrarContorno(bool activo)
    {
        if (rendererDelObjeto == null || materialContorno == null)
            return;

        if (activo)
        {
            Material[] nuevos = new Material[materialesOriginales.Length + 1];
            materialesOriginales.CopyTo(nuevos, 0);
            nuevos[nuevos.Length - 1] = materialContorno;
            rendererDelObjeto.materials = nuevos;
        }
        else
        {
            rendererDelObjeto.materials = materialesOriginales;
        }
    }

    private void OcultarTodosLosCanvas()
    {
        InstrumentoInteractivo[] todos = FindObjectsOfType<InstrumentoInteractivo>();
        foreach (var instrumento in todos)
        {
            if (instrumento != this && instrumento.canvasInfo != null)
            {
                instrumento.canvasInfo.SetActive(false);
                instrumento.MostrarContorno(false);
            }
        }
    }
}
