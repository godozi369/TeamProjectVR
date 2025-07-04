using UnityEngine;
using UnityEngine.InputSystem;

public class CastingController : MonoBehaviour
{
    [Header("�Է� �׼�")]
    public InputActionReference castAction;

    [Header("ĳ���� ����")]
    public float castPowerMultiplier = 1f; // ȸ�� ���� ����ϹǷ�, ���� ������ ������ �ٲ�� �մϴ�. 50 ������ ������ ������.

    // ������ �������� ȸ�� ���� ���ӵ��� ������ ����
    private Quaternion lastRotation;
    private float lastAngularSpeed;

    private void OnEnable()
    {
        castAction.action.Enable();
        if (castAction.action.actionMap != null)
        {
            castAction.action.actionMap.Enable();
        }
        castAction.action.started += OnCastStarted;
        castAction.action.canceled += OnCastCanceled;
    }

    private void OnDisable()
    {
        if (castAction.action.actionMap != null)
        {
            castAction.action.actionMap.Disable();
        }
        castAction.action.started -= OnCastStarted;
        castAction.action.canceled -= OnCastCanceled;
    }

    // ��ư�� ������ ���� ȣ��
    private void OnCastStarted(InputAction.CallbackContext context)
    {
        if (FishingManager.instance.fishingRodObject.activeInHierarchy)
        {
            Debug.Log("ĳ���� �غ�! �ո� �������� ��������.");
            // ���� ȸ�� ���� �ʱ� ȸ�� ������ ���
            lastRotation = transform.rotation;
        }
    }

    // ��ư�� ���� ���� ȣ��
    private void OnCastCanceled(InputAction.CallbackContext context)
    {
        if (FishingManager.instance.fishingRodObject.activeInHierarchy)
        {
            // ���˴� ���� ���� ������ ������
            Vector3 castDirection = FishingManager.instance.rodTip.forward;
            // ������ ������ ȸ�� �ӷ¿� ����Ͽ� ���� ���
            Vector3 castVelocity = castDirection * lastAngularSpeed * castPowerMultiplier;

            Debug.Log($"ĳ����! ������ ���� ���ӵ�: {lastAngularSpeed}, ����� ��: {castVelocity}");

            // FishingManager���� ĳ���� ���
            FishingManager.instance.CastBobber(castVelocity);
        }
    }

    // �� �����Ӹ��� ȣ��Ǿ� ������ ������ ȸ�� �ӵ��� ��� �����մϴ�.
    void Update()
    {
        // �� �����Ӹ��� ȣ��Ǿ� ������ ������ ȸ�� �ӵ��� ��� �����մϴ�.
        if (Time.deltaTime > 0)
        {
            Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);
            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

            float angularSpeed = angle / Time.deltaTime;

            // --- �� �κ��� �����մϴ� ---
            // �ִ� �ӵ��� 300���� 30 ������ ���� �ٿ��� ���� �����մϴ�.
            lastAngularSpeed = Mathf.Clamp(angularSpeed, 0, 20);

            lastRotation = transform.rotation;
        }
    }
}