using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
