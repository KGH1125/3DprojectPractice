using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // �߰� ������
    public LayerMask targetLayers; // ��� ���̾�� (�÷��̾� ��)
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        // LayerMask�� ����
        if (((1 << other.gameObject.layer) & targetLayers) != 0)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                // ���� �ӵ� �ʱ�ȭ
                Vector3 velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;

                // ���� �� ����
                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

                // �ִϸ��̼� Ʈ���� ����
                if (animator != null)
                {
                    animator.SetTrigger("Press");
                    Debug.Log("����");
                }
            }
        }
    }
}
