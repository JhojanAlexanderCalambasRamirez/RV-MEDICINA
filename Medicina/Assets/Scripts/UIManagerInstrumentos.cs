using UnityEngine;
using TMPro;

public class UIManagerInstrumentos : MonoBehaviour
{
    public static UIManagerInstrumentos Instance;

    [Header("Referencias UI")]
    public GameObject canvasInfo;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoDescripcion;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        canvasInfo.SetActive(false);
    }

    public void MostrarInfo(InfoInstrumento info, Vector3 posicion)
    {
        textoTitulo.text = info.nombre;
        textoDescripcion.text = info.descripcion;

        canvasInfo.transform.LookAt(Camera.main.transform);
        canvasInfo.transform.Rotate(0, 180, 0);

        canvasInfo.SetActive(true);
    }
}
