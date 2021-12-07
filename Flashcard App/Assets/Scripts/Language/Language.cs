using System;

[Serializable]
public class Language
{
    public string ISO;  //Using this as language ID.
    public string _name;

    public Language(string ISO, string _name)
    {
        this.ISO = ISO;
        this._name = _name;
    }
}