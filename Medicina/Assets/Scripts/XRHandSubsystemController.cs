using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Hands;

public class XRHandSubsystemController : MonoBehaviour
{
    XRHandSubsystem handSubsystem;

    void Start()
    {
        InitializeSubsystem();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void InitializeSubsystem()
    {
        var subsystems = new System.Collections.Generic.List<XRHandSubsystem>();
        UnityEngine.SubsystemManager.GetInstances(subsystems);

        if (subsystems.Count > 0)
        {
            handSubsystem = subsystems[0];
            if (!handSubsystem.running)
                handSubsystem.Stop(); // Lo dejamos detenido inicialmente para evitar errores
        }
        else
        {
            handSubsystem = null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (handSubsystem == null)
            InitializeSubsystem();

        if (handSubsystem != null)
        {
            // Aqu� dejamos el tracking detenido para evitar problemas
            // Pero si quieres, puedes agregar l�gica para activarlo en escenas espec�ficas
            if (!handSubsystem.running)
                handSubsystem.Stop();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
