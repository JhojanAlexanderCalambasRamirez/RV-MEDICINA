using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelLogin : MonoBehaviour
{
    public TMP_InputField inputCorreo;
    public TMP_InputField inputContraseña;
    public XRSimpleInteractable loginButton;

    private void Start()
    {
        UserManager.LoadUsers();

        // Configurar el evento del botón de login
        loginButton.selectEntered.AddListener(_ => AttemptLogin());
    }

    private void AttemptLogin()
    {
        string correo = inputCorreo.text;
        string contraseña = inputContraseña.text;

        bool success = UserManager.LoginUser(correo, contraseña);

        if (success)
        {
            // Aquí puedes añadir lógica para cambiar de panel o escena
            Debug.Log("Login exitoso, redirigiendo...");
        }
    }
}
