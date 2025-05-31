using UnityEngine;
using UnityEngine.XR.Hands;

public class XRHandSubsystemManager : MonoBehaviour
{
    XRHandSubsystem handSubsystem;

    void Start()
    {
        // Intenta obtener el subsistema activo de manos
        var subsystems = new System.Collections.Generic.List<XRHandSubsystem>();
        SubsystemManager.GetInstances(subsystems);

        if (subsystems.Count > 0)
            handSubsystem = subsystems[0];
        else
            Debug.LogWarning("No se encontró XRHandSubsystem activo.");
    }

    void Update()
    {
        if (handSubsystem != null && handSubsystem.running)
        {
            // Actualiza las manos con tipo Dynamic (puedes cambiar a BeforeRender si quieres)
            handSubsystem.TryUpdateHands(XRHandSubsystem.UpdateType.Dynamic);
        }
    }

    private void OnDestroy()
    {
        if (handSubsystem != null)
        {
            handSubsystem.Destroy();
            handSubsystem = null;
        }
    }
}
