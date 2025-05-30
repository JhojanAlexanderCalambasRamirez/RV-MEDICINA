using UnityEngine;

public class CajonDetector : MonoBehaviour
{
    public NewBehaviourScript panelMisiones;
    public int idTarea = 1;
    private bool tareaCompletada = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!tareaCompletada && other.CompareTag("Reloj"))
        {
            tareaCompletada = true;
            panelMisiones.CompletarTarea(idTarea);
        }
    }
}
