using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PanelLogin : MonoBehaviour
{
    public TMP_InputField inputCorreo;
    public TMP_InputField inputContrase�a;
    public XRSimpleInteractable loginButton;

    private void Start()
    {
        UserManager.LoadUsers();

        // Configurar el evento del bot�n de login
        loginButton.selectEntered.AddListener(_ => AttemptLogin());
    }

    private void AttemptLogin()
    {
        string correo = inputCorreo.text;
        string contrase�a = inputContrase�a.text;

        bool success = UserManager.LoginUser(correo, contrase�a);

        if (success)
        {
            // Aqu� puedes a�adir l�gica para cambiar de panel o escena
            Debug.Log("Login exitoso, redirigiendo...");
        }
    }
}
