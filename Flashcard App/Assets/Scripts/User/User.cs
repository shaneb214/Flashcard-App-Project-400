using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User 
{
    public static event Action<User> UserCreatedEvent;

    public string ID;
    public string Name;
    public string Email;

    public User() { }
    public User(string Name,string Email)
    {
        ID = Guid.NewGuid().ToString();
        this.Name = Name;
        this.Email = Email;

        UserCreatedEvent?.Invoke(this);
    }
}
