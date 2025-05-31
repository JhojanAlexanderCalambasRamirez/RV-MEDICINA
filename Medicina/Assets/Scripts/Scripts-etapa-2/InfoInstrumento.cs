using UnityEngine;

[System.Serializable]
public class InfoInstrumento
{
    public string nombre;
    [TextArea]
    public string descripcion;
    public AudioClip audioClip;
}
