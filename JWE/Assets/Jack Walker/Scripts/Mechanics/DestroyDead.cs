using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDead : MonoBehaviour
{
    [SerializeField] Renderer modelRenderer;
    [SerializeField] Material disolveMat;
    float disolveratio = 0;
    private void DestroyEnemy()
    {
        modelRenderer.material = disolveMat;
        StartCoroutine("DeathTimer");
    }

    private IEnumerator DeathTimer()
    {
        while(disolveratio < 0.99)
        {
            disolveratio += 0.01f;
            
            modelRenderer.material.SetFloat("Vector1_BDBEBF2D", disolveratio);
            yield return new WaitForSeconds(0.05f);
        }
        if(disolveratio > 0.98f)
        {
            Destroy(this.gameObject);
        }
    }
}
