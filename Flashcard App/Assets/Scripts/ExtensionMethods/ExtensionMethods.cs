using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods 
{
    public static bool AllCharactersEmptyOrWhiteSpace(this string value) => value.All(char.IsWhiteSpace);
}
