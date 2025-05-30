using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelRegistro : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TMP_InputField inputCorreo;
    public TMP_InputField inputContraseña;
    public TMP_InputField inputConfirmacion;
    public XRSimpleInteractable registerButton;

    private void Start()
    {
        UserManager.LoadUsers();

        // Configurar el evento del botón de registro
        registerButton.selectEntered.AddListener(_ => AttemptRegister());
    }

    private void AttemptRegister()
    {
        string nombre = inputNombre.text;
        string correo = inputCorreo.text;
        string contraseña = inputContraseña.text;
        string confirmacion = inputConfirmacion.text;

        bool success = UserManager.RegisterUser(nombre, correo, contraseña, confirmacion);

        if (success)
        {
            // Aquí puedes añadir lógica para cambiar de panel o escena
            Debug.Log("Registro exitoso, redirigiendo...");
        }
    }
}
