using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistScript : MonoBehaviour
{
    [SerializeField] bool isLooking = false;
    [SerializeField] FlowManager flowManager;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform player;
    Animator anim;
    bool noticed = false;
    public bool turn = false;
    bool dead = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!dead)
        {
            if (flowManager.melted > 0 && !noticed)
            {
                NoticeUs();
            }
            if (anim.GetInteger("State") == 0)
            {
                anim.SetInteger("State", Random.Range(1, 5));
            }
            else if (anim.GetInteger("State") <= 4)
            {
                anim.SetInteger("State", 0);
            }
        }


    }

    void NoticeUs()
    {
        noticed = true;
        if (isLooking)
        {
            anim.SetInteger("State", 5);
            
        }
        else
        {
            anim.SetInteger("State", 6);
        }
    }

    public void Die()
    {
        dead = true;
        Destroy(anim);
        Destroy(this, 2.7f);
    }

}
