using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public InputActionReference testAction;

    void OnEnable()
    {
        Debug.Log(gameObject.name + " - InputTest.OnEnable() ȣ���!");

        // �� �׼��� ���Ե� '�׼� ��'�� ã�Ƽ� Ȱ��ȭ�մϴ�. (�̰��� �ٽ�!)
        if (testAction.action.actionMap != null)
        {
            Debug.Log("'" + testAction.action.actionMap.name + "' �׼� ���� Ȱ��ȭ�մϴ�.");
            testAction.action.actionMap.Enable();
        }

        testAction.action.performed += OnActionPerformed;
    }

    void OnDisable()
    {
        if (testAction.action.actionMap != null)
        {
            testAction.action.actionMap.Disable();
        }
        testAction.action.performed -= OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        Debug.LogError("!!! �Է� ����: " + context.action.name + " �׼��� �����Ǿ����ϴ�! !!!");
    }
}