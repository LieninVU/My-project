using UnityEngine;

public class DescriptablePlantsVisual : MonoBehaviour
{
    [SerializeField] private DescriptablePlants descriptablePlants;
    [SerializeField] private GameObject bushDeathVFXPrefab;

    private void Start()
    {
        descriptablePlants.OnDestructibleTakeDamage += DestructiblePlantOnDestructibleTakeDamage;
    }

    private void DestructiblePlantOnDestructibleTakeDamage(object sender, System.EventArgs e)
    {
        ShowDeathVFX();
    }

    private void ShowDeathVFX()
    {
        Instantiate(bushDeathVFXPrefab, descriptablePlants.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        descriptablePlants.OnDestructibleTakeDamage -= DestructiblePlantOnDestructibleTakeDamage;
    }
}
