using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 추가 점프력
    public LayerMask targetLayers; // 대상 레이어
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayers) != 0)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                Vector3 velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;

                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

                if (animator != null)
                {
                    animator.SetTrigger("Press");
                    Debug.Log("실행");
                }
            }
        }
    }
}
