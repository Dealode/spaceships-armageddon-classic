using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtension
    {
        public static string ToJson(this object obj, bool pretty = true) =>
            JsonUtility.ToJson(obj, pretty);
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}