using UnityEngine;

public static class PersistentIdentifierManager
{
    public static string GetOrCreateIdentifier(GameObject gameObject)
    {
        string key = $"identifier_{gameObject.GetInstanceID()}";

        if (!PlayerPrefs.HasKey(key))
        {
            string uniqueId = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString(key, uniqueId);
            PlayerPrefs.Save(); // Ensure the data is saved immediately
        }

        return PlayerPrefs.GetString(key);
    }
}