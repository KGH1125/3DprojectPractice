using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 추가 점프력
    public LayerMask targetLayers; // 대상 레이어들 (플레이어 등)
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        // LayerMask로 감지
        if (((1 << other.gameObject.layer) & targetLayers) != 0)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                // 수직 속도 초기화
                Vector3 velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;

                // 점프 힘 적용
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

                // 애니메이션 트리거 실행
                if (animator != null)
                {
                    animator.SetTrigger("Press");
                    Debug.Log("실행");
                }
            }
        }
    }
}
