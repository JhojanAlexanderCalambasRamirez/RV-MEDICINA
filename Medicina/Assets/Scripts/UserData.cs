using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData 
{
        public string nombre;
        public string correo;
        public string contrase�a;

        public UserData(string nombre, string correo, string contrase�a)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.contrase�a = contrase�a;
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
