using System;
using UnityEngine;

public class DescriptablePlants : MonoBehaviour
{
    public event EventHandler OnDestructibleTakeDamage;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Sword>())
        {
            OnDestructibleTakeDamage?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);

            NavMeshSurfaceManager.Instance.RebakeNavMeshSurface();
            
        }
    }
}
