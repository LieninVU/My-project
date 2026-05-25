using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentEventSystem : MonoBehaviour
{
    private static EventSystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<EventSystem>();
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликат
        }
    }
}