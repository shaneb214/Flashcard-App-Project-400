using System;

[Serializable]
public class Language
{
    public string ISO;  //Using this as language ID.
    public string Name;

    public Language(string ISO, string _name)
    {
        this.ISO = ISO;
        this.Name = _name;
    }
}