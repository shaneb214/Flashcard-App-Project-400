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

    public User(string Name)
    {
        ID = Guid.NewGuid().ToString();
        this.Name = Name;

        UserCreatedEvent?.Invoke(this);
    }
}
