using System.Collections.Generic;

namespace BlitzyUI
{
    public abstract partial class Screen
    {
        /// <summary>
        /// Data container that is passed along to Screens that are being pushed. Screens can use these to setup
        /// themselves up with custom data provided at run-time.
        /// </summary>
        public class ScreenData
        {
            private Dictionary<string, object> _data;

            public ScreenData ()
            {
                _data = new Dictionary<string, object>();
            }

            public ScreenData (int capacity)
            {
                _data = new Dictionary<string, object>(capacity);
            }

            public void Add(string key, object data)
            {
                _data.Add(key, data);
            }

            public T Get<T>(string key)
            {
                object datum = Get(key);

                try
                {
                    return (T)datum;
                }
                catch
                {
                    throw new System.Exception(string.Format("[BlitzyUI.Screen.Data] Could not cast data object '{0}' to type '{1}'", key, typeof(T).Name));
                }
            }

            public object Get (string key)
            {
                object datum;

                if (!_data.TryGetValue(key, out datum))
                    throw new System.Exception(string.Format("[BlitzyUI.Screen.Data] No object found for key '{0}'", key));

                return datum;
            }

            public bool TryGet (string key, out object datum)
            {
                return _data.TryGetValue(key, out datum);
            }

            public bool TryGet<T> (string key, out T datum)
            {
                object datumObj;

                if (_data.TryGetValue(key, out datumObj))
                {
                    try
                    {
                        datum = (T)datumObj;
                        return true;
                    }
                    catch
                    {
                        throw new System.Exception(string.Format("[BlitzyUI.Screen.Data] Could not cast data object '{0}' to type '{1}'", key, typeof(T).Name));
                    }
                }

                datum = default(T);
                return false;
            }
        }
    }
}