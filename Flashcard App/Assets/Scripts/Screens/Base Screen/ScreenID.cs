using System;

namespace BlitzyUI
{
    public abstract partial class Screen
    {
        [Serializable]
        public class ScreenID
        {
            //readonly string name;
            //public readonly string defaultPrefabName;

            public string name;
            public string defaultPrefabName;

            public ScreenID (string name)
            {
                this.name = name;
            }

            public ScreenID (string name, string defaultPrefabName)
            {
                this.name = name;
                this.defaultPrefabName = defaultPrefabName;
            }

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

            public static bool operator == (ScreenID x, ScreenID y) 
            {
                if (ReferenceEquals(x, null))
                    return ReferenceEquals(y, null);

                return x.Equals(y);
            }

            public static bool operator != (ScreenID x, ScreenID y) 
            { 
                return !(x == y);
            }

            public override bool Equals(object obj)
            {
                ScreenID other = obj as ScreenID;
                if (ReferenceEquals(other, null))
                    return false;

                return name == other.name;
            }

            public override string ToString()
            {
                return name;
            }
        }
    }
}