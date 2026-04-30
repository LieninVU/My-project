using UnityEngine;

public class SelfDestrouVFX : MonoBehaviour
{
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_ps && !_ps.IsAlive())
        {
            DestroySelf();
        }
    }


    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
