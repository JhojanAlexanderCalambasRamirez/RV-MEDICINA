using UnityEngine;

public class CajonDetector : MonoBehaviour
{
    public NewBehaviourScript panelMisiones;
    public int idTarea = 1;
    private bool tareaCompletada = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si es la misi�n actual y que no est� ya completada
        if (!tareaCompletada && panelMisiones.misionActual == idTarea && other.CompareTag("Reloj"))
        {
            tareaCompletada = true;
            panelMisiones.CompletarTarea(idTarea);
        }
    }
}
