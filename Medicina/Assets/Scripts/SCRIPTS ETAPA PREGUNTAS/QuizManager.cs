using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;
using UnityEngine.SceneManagement;

[Serializable]
public class Pregunta
{
    public string enunciado;
    public string[] opciones = new string[3]; // A, B, C
    public int indiceCorrecto; // 0 = A, 1 = B, 2 = C
}

public class QuizManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoPregunta;
    public Toggle[] toggles; 
   

    public TextMeshProUGUI feedbackTexto;
    public Button botonValidar;
    public Button botonSiguiente;
    public GameObject panelResultados;
    public TextMeshProUGUI resultadoTexto;

    [Header("Preguntas")]
    public Pregunta[] preguntas;

    private int indiceActual = 0;
    private int respuestasCorrectas = 0;
    private bool respondido = false;

    void Start()
    {
        MostrarPregunta();
        feedbackTexto.text = "";
        panelResultados.SetActive(false);
    }

    public void MostrarPregunta()
    {
        respondido = false;
        Pregunta p = preguntas[indiceActual];
        textoPregunta.text = p.enunciado;
        feedbackTexto.text = "";

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
            toggles[i].interactable = true;
            toggles[i].gameObject.SetActive(true);

            TextMeshProUGUI toggleLabel = toggles[i].GetComponentInChildren<TextMeshProUGUI>();
            if (toggleLabel != null)
                toggleLabel.text = p.opciones[i];
        }

        botonValidar.gameObject.SetActive(true);
        botonSiguiente.gameObject.SetActive(true);
    }

    public void ValidarRespuesta()
    {
        if (respondido) return;
        int seleccion = -1;

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                seleccion = i;
                break;
            }
        }

        if (seleccion == -1)
        {
            feedbackTexto.text = "Selecciona una opción.";
            return;
        }

        if (seleccion == preguntas[indiceActual].indiceCorrecto)
        {
            feedbackTexto.text = " Correcto";
            respuestasCorrectas++;
        }
        else
        {
            feedbackTexto.text = " Incorrecto";
        }

        respondido = true;

        // Desactivar interacción en los toggles después de responder
        foreach (var toggle in toggles)
        {
            toggle.interactable = false;
        }
    }

    public void SiguientePregunta()
    {
        if (!respondido) return;

        indiceActual++;
        if (indiceActual >= preguntas.Length)
        {
            MostrarResultados();
        }
        else
        {
            MostrarPregunta();
        }
    }

    public void MostrarResultados()
    {
        textoPregunta.text = "";
        foreach (var t in toggles) t.gameObject.SetActive(false);
        botonValidar.gameObject.SetActive(false);
        botonSiguiente.gameObject.SetActive(false);

        panelResultados.SetActive(true);
        float porcentaje = (float)respuestasCorrectas / preguntas.Length * 100f;
        resultadoTexto.text = $"Resultado: {porcentaje:F0}%\n" + (porcentaje >= 80f ? " Aprobado" : " No aprobado");
    }

    public void ReintentarQuiz()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirDelQuiz()
    {
        // Opcional: puedes ir a una escena de menú, o salir del juego (si estás en build)
        Debug.Log("Saliendo del quiz...");
        Application.Quit();

        // O si prefieres cargar una escena de menú:
        // SceneManager.LoadScene("NombreDeLaEscenaMenu");
    }
}