using UnityEngine;

public class ParticlesScript : MonoBehaviour
{
    private ParticleSystem particles;
    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (!particles.IsAlive())
            ObjectPool.Instance.ReturnObject(gameObject);
    }

    public void Play()
    {
        particles.Play();
    }
}
