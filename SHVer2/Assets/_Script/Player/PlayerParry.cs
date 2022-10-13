using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    [SerializeField] private GameObject blockTop;
    [SerializeField] private GameObject blockFront;
    [SerializeField] private float blockTime;
    private PlayerInput playerInput;
    public bool isDisabled = true;
    public float parryCoolDownTime = 2f;
    private float nextParryTime = 0;

    private void Awake()
    {
        blockTop.SetActive(false);
        blockFront.SetActive(false);
        playerInput = GetComponent<PlayerInput>();
        Physics2D.IgnoreLayerCollision(6, 19);
    }

    public void PlayerParryFeature()
    {
        if (playerInput.ParryButton() && !isDisabled && !ParryOnCooldown())
        {
            StartCoroutine(Parry());
            nextParryTime = Time.time + parryCoolDownTime;
        }
    }

    private bool ParryOnCooldown()
    {
        bool weaponOnCooldown = Time.time > nextParryTime ? false : true;
        return weaponOnCooldown;
    }

    private void AllowParry(bool allow)
    {
        isDisabled = allow;
    }

    private IEnumerator Parry()
    {
        blockTop.SetActive(true);
        blockFront.SetActive(true);
        yield return new WaitForSeconds(blockTime);
        blockTop.SetActive(false);
        blockFront.SetActive(false);
    }

    private void OnEnable()
    {
        Actions.disableParry += AllowParry;
    }

    private void OnDisable()
    {
        Actions.disableParry -= AllowParry;
    }

}
