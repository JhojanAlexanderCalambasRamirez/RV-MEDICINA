using UnityEngine;
using UnityEngine.UI;

public class AbrirPuertas : MonoBehaviour
{
    [Header("Toggles de misión")]
    public Toggle tarea1;
    public Toggle tarea2;
    public Toggle tarea3;
    public Toggle tarea4;

    [Header("Puertas")]
    public Transform puertaIzquierda;
    public Transform puertaDerecha;

    [Header("Rotaciones")]
    public Vector3 rotacionIzquierdaFinal = new Vector3(0, -90, 0);
    public Vector3 rotacionDerechaFinal = new Vector3(0, 90, 0);
    public float velocidad = 2f;

    [Header("Sonido de apertura")]
    public AudioSource sonidoPuertas;

    private bool puertasAbiertas = false;
    private bool sonidoReproducido = false;

    void Update()
    {
        if (!puertasAbiertas && tarea1.isOn && tarea2.isOn && tarea3.isOn && tarea4.isOn)
        {
            puertasAbiertas = true;
            Debug.Log("✅ Todas las misiones completadas. ¡Abrir puertas!");
        }

        if (puertasAbiertas)
        {
            // Reproducir el sonido solo una vez
            if (!sonidoReproducido && sonidoPuertas != null)
            {
                sonidoPuertas.Play();
                sonidoReproducido = true;
            }

            // Rotar suavemente las puertas hacia su posición final
            puertaIzquierda.localRotation = Quaternion.Slerp(
                puertaIzquierda.localRotation,
                Quaternion.Euler(rotacionIzquierdaFinal),
                Time.deltaTime * velocidad
            );

            puertaDerecha.localRotation = Quaternion.Slerp(
                puertaDerecha.localRotation,
                Quaternion.Euler(rotacionDerechaFinal),
                Time.deltaTime * velocidad
            );
        }
    }
}
