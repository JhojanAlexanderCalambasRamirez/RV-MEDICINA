using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstrumentoInteractivo : MonoBehaviour
{
    [Header("Datos del instrumento")]
    public InfoInstrumento info;

    [Header("Contorno")]
    public Renderer rendererDelObjeto;
    public Material materialContorno;
    private Material[] materialesOriginales;

    [Header("Referencias UI individuales")]
    public GameObject canvasInfo;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoDescripcion;
    public Button botonAudio;
    public AudioSource audioSource;

    private AudioClip audioActual;

    private void Start()
    {
#if UNITY_EDITOR
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
#endif

        if (rendererDelObjeto == null)
            rendererDelObjeto = GetComponent<Renderer>();

        if (rendererDelObjeto != null)
            materialesOriginales = rendererDelObjeto.materials;

        if (botonAudio != null)
            botonAudio.onClick.AddListener(ReproducirAudio);

        if (canvasInfo != null)
            canvasInfo.SetActive(false);

        audioActual = info.audioClip;
    }

    public void ActivarInfo()
    {
        // Ocultar los demás canvas
        DesactivarOtrosCanvas();

        // Mostrar info en el canvas propio
        if (canvasInfo != null)
        {
            textoTitulo.text = info.nombre;
            textoDescripcion.text = info.descripcion;

            // Orientación hacia la cámara (solo eje Y)
            Vector3 direccionSoloY = new Vector3(Camera.main.transform.position.x, canvasInfo.transform.position.y, Camera.main.transform.position.z);
            canvasInfo.transform.LookAt(direccionSoloY);
            canvasInfo.transform.Rotate(0, 180, 0);

            canvasInfo.SetActive(true);
        }

        MostrarContorno(true);
    }

    private void ReproducirAudio()
    {
        if (audioActual != null && audioSource != null)
        {
            audioSource.clip = audioActual;
            audioSource.Play();
        }
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

    private void DesactivarOtrosCanvas()
    {
        InstrumentoInteractivo[] todos = FindObjectsOfType<InstrumentoInteractivo>();
        foreach (var instrumento in todos)
        {
            if (instrumento != this)
            {
                if (instrumento.canvasInfo != null)
                    instrumento.canvasInfo.SetActive(false);

                instrumento.MostrarContorno(false);
            }
        }
    }

#if UNITY_EDITOR
    private void OnMouseDown()
    {
        ActivarInfo();
    }
#endif
}
