using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledParticleEffect : MonoBehaviour
{
    public GameObject particleEffectPrefab;
    private Queue<GameObject> particlePool = new Queue<GameObject>();

    protected void PlayEffect(Vector3 position)
    {
        GameObject effect = GetPooledEffect();
        effect.transform.position = position;
        effect.SetActive(true);

        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            StartCoroutine(ReturnParticleToPool(effect, ps.main.duration));
        }
    }

    private GameObject GetPooledEffect()
    {
        if (particlePool.Count > 0)
        {
            return particlePool.Dequeue();
        }
        else
        {
            return Instantiate(particleEffectPrefab);
        }
    }

    private IEnumerator ReturnParticleToPool(GameObject effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.SetActive(false);
        particlePool.Enqueue(effect);
    }
}
