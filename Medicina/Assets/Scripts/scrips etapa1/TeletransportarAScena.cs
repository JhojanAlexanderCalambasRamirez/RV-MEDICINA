using UnityEngine;
using UnityEngine.SceneManagement;

public class TeletransportarAScena : MonoBehaviour
{
    [Tooltip("Índice de la escena a cargar (según Build Settings)")]
    public int indiceEscenaDestino;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) // Puedes ajustar el tag según lo que quieras detectar
        {
            Debug.Log($"✅ Entró al portal. Cargando escena con índice {indiceEscenaDestino}...");
            SceneManager.LoadScene(indiceEscenaDestino);
        }
    }
}
