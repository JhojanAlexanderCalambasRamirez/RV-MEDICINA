using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas si quieres
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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
            StartCoroutine(CargarEscenaQuizAsync());
        }
    }

    private void ActualizarTexto()
    {
        textoContador.text = $"{instrumentosVisitados}/{totalInstrumentos}";
    }

    private IEnumerator CargarEscenaQuizAsync()
    {
        yield return new WaitForSeconds(audioFinal.clip.length);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Preguntas-Etapa-1-2");
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            // Puedes mostrar asyncLoad.progress aquí para UI
            yield return null;
        }

        // Puedes esperar un pequeño delay extra para suavizar la transición si quieres
        yield return new WaitForSeconds(0.1f);

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
