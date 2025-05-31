using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ContadorInstrumentosUI : MonoBehaviour
{
    public static ContadorInstrumentosUI Instance;

    [Header("UI")]
    public TextMeshProUGUI textoContador;
    public AudioSource audioFinal;

    [Header("Cantidad total")]
    [SerializeField] private int totalInstrumentos = 16;
    private int instrumentosVisitados = 0;
    private bool audioYaIniciado = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        ActualizarTexto();
    }

    public void RegistrarInteraccion()
    {
        instrumentosVisitados++;
        ActualizarTexto();

        if (instrumentosVisitados >= totalInstrumentos && !audioYaIniciado)
        {
            audioYaIniciado = true;
            audioFinal.Play();
            Invoke(nameof(CargarEscenaQuiz), audioFinal.clip.length);
        }
    }

    private void ActualizarTexto()
    {
        textoContador.text = $"{instrumentosVisitados}/{totalInstrumentos}";
    }

    private void CargarEscenaQuiz()
    {
        SceneManager.LoadScene("Preguntas-Etapa-1-2");
    }
}
