using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        anim.SetBool("idle", true);
    }

    private void NormalShoot()
    {
        anim.SetTrigger("shoot");
    }

    private void MeteorShoot()
    {
        anim.SetTrigger("shoot-meteor");
    }


    private void OnEnable()
    {
        Actions.shootAnim += NormalShoot;
        Actions.meteorShootAnim += MeteorShoot;
    }

    private void OnDisable()
    {
        Actions.shootAnim -= NormalShoot;
        Actions.meteorShootAnim -= MeteorShoot;
    }
}
