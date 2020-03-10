using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Renderer _renderer;
    [SerializeField] Texture[] textures;
    [SerializeField] bool rotateToCam;
    Vector3 playerTransfrom;
    int activeTexture = 0;
    float timeLeft = 1f;

    private void Update()
    {
        
        if (Vector3.Distance(player.position, playerTransfrom) > 0.3f && rotateToCam)
        {
            playerTransfrom = player.position;
            transform.LookAt(transform.position + player.rotation * Vector3.forward,
               player.rotation * Vector3.up);

        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            if (activeTexture == 0)
            {
                _renderer.material.SetTexture("_mTexture", textures[1]);
                activeTexture = 1;
            }
            else
            {
                _renderer.material.SetTexture("_mTexture", textures[0]);
                activeTexture = 0;

            }

            timeLeft = 1f;
        }

    }
}
