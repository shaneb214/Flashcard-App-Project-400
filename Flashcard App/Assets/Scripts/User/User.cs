using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User 
{
    public static event Action<User> UserCreatedEvent;

    public string ID;
    public string Username;
    public string Email;

    public User() { }
    public User(string ID,string Name, string Email)
    {
        this.ID = ID;
        this.Username = Name;
        this.Email = Email;

        UserCreatedEvent?.Invoke(this);
    }
    public User(string Name,string Email)
    {
        ID = Guid.NewGuid().ToString();
        this.Username = Name;
        this.Email = Email;

        UserCreatedEvent?.Invoke(this);
    }
}
