using UnityEngine;

public class DestructableElixirVisual : MonoBehaviour
{
    [SerializeField] private Elixir elixir;
    [SerializeField] private GameObject breakElixirVFX;

    private void Start()
    {
        elixir.OnDestructibleElixir += ElixirOnDestructibleElixir;
    }

    private void ElixirOnDestructibleElixir()
    {
        ShowBreakVFX();
    }

    private void ShowBreakVFX()
    {
        Instantiate(breakElixirVFX, elixir.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        elixir.OnDestructibleElixir -= ElixirOnDestructibleElixir;
    }
}
