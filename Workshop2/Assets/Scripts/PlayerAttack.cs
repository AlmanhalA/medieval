using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider attackHitbox;
    public CharacterAnimator characterAnimator;
    public int kills = 0;
    public float attackDuration = 1f;

    void Start()
    {
        attackHitbox.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        characterAnimator.SetAttacking(true); //error here

        attackHitbox.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.enabled = false;
        characterAnimator.SetAttacking(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            kills++;
            Debug.Log("Total kills: " + kills);
            Destroy(other.gameObject);
        }
    }
}