using UnityEngine;

public class DetectorLavadoManos : MonoBehaviour
{
    [Header("Efectos visuales y sonoros")]
    public ParticleSystem aguaEfecto;
    public AudioSource sonidoAgua;

[Header("Control de misiones")]
public NewBehaviourScript panelMisiones;
public int idMision = 3;

private bool izquierdaDentro = false;
private bool derechaDentro = false;
private bool misionCompletada = false;

private void Start()
{
    // Apagar agua y sonido al iniciar
    if (aguaEfecto != null) aguaEfecto.Stop();
    if (sonidoAgua != null) sonidoAgua.Stop();
}

private void OnTriggerEnter(Collider other)
{   
        Debug.Log("Colisión con: " + other.name);
        if (misionCompletada) return;

    if (other.CompareTag("ManoIzquierda"))
        izquierdaDentro = true;

    if (other.CompareTag("ManoDerecha"))
        derechaDentro = true;

    // Activar agua y sonido si al menos una mano entra
    if ((izquierdaDentro || derechaDentro))
    {
        if (aguaEfecto != null && !aguaEfecto.isPlaying)
            aguaEfecto.Play();

        if (sonidoAgua != null && !sonidoAgua.isPlaying)
            sonidoAgua.Play();
    }

    // Verificar si ambas manos están dentro
    if (izquierdaDentro && derechaDentro)
    {
        misionCompletada = true;

        if (panelMisiones != null)
            panelMisiones.CompletarTarea(idMision);

        Debug.Log("✅ ¡Misión 3 completada: Lavado de manos!");
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("ManoIzquierda"))
        izquierdaDentro = false;

    if (other.CompareTag("ManoDerecha"))
        derechaDentro = false;

    // Detener agua y sonido si ya no hay manos dentro
    if (!izquierdaDentro && !derechaDentro)
    {
        if (aguaEfecto != null && aguaEfecto.isPlaying)
            aguaEfecto.Stop();

        if (sonidoAgua != null && sonidoAgua.isPlaying)
            sonidoAgua.Stop();
    }
}
}