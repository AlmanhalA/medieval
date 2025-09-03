using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3.5f;
    public GameObject player;

    public float gravity = -9.81f;
    private float velocityY = 0f;

    private CharacterController controller;
    private CharacterAnimator characterAnimator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        characterAnimator = GetComponent<CharacterAnimator>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
        if (target == null)
        {
            characterAnimator.SetWalking(false);
            return;
        }


        if (controller.isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        FaceTarget(direction);

        Vector3 move = direction * moveSpeed;
        move.y = velocityY;

        controller.Move(move * Time.deltaTime);

        characterAnimator.SetWalking(true);
    }

    void FaceTarget(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }
    }
}