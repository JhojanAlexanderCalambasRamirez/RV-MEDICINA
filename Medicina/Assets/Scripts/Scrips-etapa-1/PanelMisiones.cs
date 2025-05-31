using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Tareas")]
    public Toggle tarea1Accesorios;
    public Toggle tarea2Tapabocas;
    public Toggle tarea3Lavado;
    public Toggle tarea4Guantes;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoCheck;

    [Header("Progreso de misiones")]
    [Tooltip("Solo se permite completar la misión si coincide con este número.")]
    [HideInInspector]
    public int misionActual = 1;

    public void CompletarTarea(int id)
    {
        if (id != misionActual) return; // Evita completar misiones fuera de orden

        switch (id)
        {
            case 1: tarea1Accesorios.isOn = true; break;
            case 2: tarea2Tapabocas.isOn = true; break;
            case 3: tarea3Lavado.isOn = true; break;
            case 4: tarea4Guantes.isOn = true; break;
        }

        // Reproducir sonido de confirmación
        if (audioSource != null && sonidoCheck != null)
        {
            audioSource.PlayOneShot(sonidoCheck);
        }

        misionActual++; // Avanzar a la siguiente misión
    }
}
