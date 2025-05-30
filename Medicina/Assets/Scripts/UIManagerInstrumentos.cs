using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManagerInstrumentos : MonoBehaviour
{
    public static UIManagerInstrumentos Instance;

    [Header("Referencias UI")]
    public GameObject canvasInfo;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoDescripcion;
    public Button botonAudio;

    [Header("Audio")]
    public AudioSource audioSource;

    private AudioClip audioActual;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        canvasInfo.SetActive(false);
        botonAudio.onClick.AddListener(ReproducirAudio);
    }

    public void MostrarInfo(InfoInstrumento info, Vector3 posicion)
    {
        textoTitulo.text = info.nombre;
        textoDescripcion.text = info.descripcion;
        audioActual = info.audioClip;

        // Posiciona el Canvas flotante frente al instrumento
        canvasInfo.transform.position = posicion + Vector3.up * 0.25f;
        canvasInfo.transform.LookAt(Camera.main.transform);
        canvasInfo.transform.Rotate(0, 180, 0);

        canvasInfo.SetActive(true);
    }

    public void ReproducirAudio()
    {
        if (audioActual != null)
        {
            audioSource.clip = audioActual;
            audioSource.Play();
        }
    }
}
