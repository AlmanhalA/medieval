using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitboxObject;
    public CharacterAnimator characterAnimator;

    public int kills = 0;
    public float attackDuration = 0.5f;
    public float attackCD = 1f;

    private bool isAttacking = false;

    void Start()
    {
        attackHitboxObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        characterAnimator.SetAttacking(true);
        attackHitboxObject.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackHitboxObject.SetActive(false);
        characterAnimator.SetAttacking(false);

        yield return new WaitForSeconds(attackCD - attackDuration);
        
        isAttacking = false;
    }

    public void RegisterKill()
    {
        kills++;
        Debug.Log("Total kills: " + kills);
    }
}