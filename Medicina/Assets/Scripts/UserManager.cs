using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class UserManager 
{
    private static List<UserData> users = new List<UserData>();
    private static string filePath = Application.persistentDataPath + "/users.json";

    public static void LoadUsers()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserList userList = JsonUtility.FromJson<UserList>(json);
            if (userList != null)
            {
                users = userList.users;
            }
        }
    }

    public static void SaveUsers()
    {
        UserList userList = new UserList { users = users };
        string json = JsonUtility.ToJson(userList, true);
        File.WriteAllText(filePath, json);
    }

    public static bool RegisterUser(string nombre, string correo, string contrase�a, string confirmacion)
    {
        // Validaciones
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) ||
            string.IsNullOrEmpty(contrase�a) || string.IsNullOrEmpty(confirmacion))
        {
            Debug.Log("Todos los campos son obligatorios");
            return false;
        }

        if (contrase�a != confirmacion)
        {
            Debug.Log("Las contrase�as no coinciden");
            return false;
        }

        if (users.Exists(u => u.correo == correo))
        {
            Debug.Log("El correo ya est� registrado");
            return false;
        }

        // Crear nuevo usuario
        UserData newUser = new UserData(nombre, correo, contrase�a);
        users.Add(newUser);
        SaveUsers();

        Debug.Log("Registro exitoso");
        return true;
    }

    public static bool LoginUser(string correo, string contrase�a)
    {
        UserData user = users.Find(u => u.correo == correo && u.contrase�a == contrase�a);
        if (user != null)
        {
            Debug.Log("Login exitoso. Bienvenido " + user.nombre);
            return true;
        }

        Debug.Log("Correo o contrase�a incorrectos");
        return false;
    }

    [Serializable]
    private class UserList
    {
        public List<UserData> users;
    }
}
