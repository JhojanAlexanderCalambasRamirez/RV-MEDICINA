using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserAccount 
{
    public string nombre;
    public string correo;
    public string codigoEstudiantil;
    public string contrasena;

    // Constructor
    public UserAccount(string nombre, string correo, string codigoEstudiantil, string contrasena)
    {
        this.nombre = nombre;
        this.correo = correo;
        this.codigoEstudiantil = codigoEstudiantil;
        this.contrasena = contrasena;
    }
}

[System.Serializable]
public class AccountDatabase
{
    public List<UserAccount> accounts = new List<UserAccount>();
}

