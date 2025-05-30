using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InstrumentoInteractivo : MonoBehaviour
{
    public InfoInstrumento info;

    private void Start()
    {
        // Para pruebas sin XR: click con mouse
#if UNITY_EDITOR
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
#endif
    }

    public void ActivarInfo()
    {
        UIManagerInstrumentos.Instance.MostrarInfo(info, this.transform.position);
    }

#if UNITY_EDITOR
    private void OnMouseDown()
    {
        ActivarInfo();
    }
#endif
}
