using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ObjectRange : MonoBehaviour
{
    bool isTarget = false;
    public ParticleSystem particle;

    void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        if (isTarget) return;

        Collider[] cols = Physics.OverlapSphere(transform.position, 2f);

        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Player"))
                {
                    particle.Play();
                    Destroy(gameObject, particle.main.duration);
                    isTarget = true;
                }
            }
        }


    }
}
