using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelRegistro : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputCorreo;
    public TMP_InputField inputContrase�a;
    public TMP_InputField inputConfirmacion;
    public XRSimpleInteractable registerButton;

    private void Start()
    {
        UserManager.LoadUsers();

        // Configurar el evento del bot�n de registro
        registerButton.selectEntered.AddListener(_ => AttemptRegister());
    }

    private void AttemptRegister()
    {
        string nombre = inputNombre.text;
        string correo = inputCorreo.text;
        string contrase�a = inputContrase�a.text;
        string confirmacion = inputConfirmacion.text;

        bool success = UserManager.RegisterUser(nombre, correo, contrase�a, confirmacion);

        if (success)
        {
            // Aqu� puedes a�adir l�gica para cambiar de panel o escena
            Debug.Log("Registro exitoso, redirigiendo...");
        }
    }
}
