using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData 
{
        public string nombre;
        public string correo;
        public string contraseña;

        public UserData(string nombre, string correo, string contraseña)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.contraseña = contraseña;
        }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
