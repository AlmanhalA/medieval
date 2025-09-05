using UnityEngine;

public class hitbox : MonoBehaviour
{
    private PlayerAttack playerAttackScript;

    void Start()
    {
        playerAttackScript = GetComponentInParent<PlayerAttack>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerAttackScript.RegisterKill();
            Destroy(other.gameObject);
        }
    }
}
