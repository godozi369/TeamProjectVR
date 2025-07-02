using UnityEngine;
using TMPro; // TextMeshPro�� InputField�� ����ϱ� ���� �ʿ�

// TMP_InputField�� �� ��ũ��Ʈ�� �߰��ϸ� �˴ϴ�.
[RequireComponent(typeof(TMP_InputField))]
public class OculusKeyboardManager : MonoBehaviour
{
    private TMP_InputField inputField;
    private TouchScreenKeyboard virtualKeyboard;

    void Start()
    {
        // �� ��ũ��Ʈ�� �پ��ִ� ���� ������Ʈ�� TMP_InputField ������Ʈ�� �����ɴϴ�.
        inputField = GetComponent<TMP_InputField>();

        // InputField�� ���õǾ��� ��(Ŭ���Ǿ��� ��) Ű���带 ���� �Լ��� �����մϴ�.
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    // Ű���带 ���� �Լ�
    public void OpenKeyboard()
    {
        // Oculus �ý��� Ű���带 ���ϴ�.
        // TouchScreenKeyboard.Open(���� �ؽ�Ʈ, Ű���� Ÿ��, ...);
        virtualKeyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default);
    }

    void Update()
    {
        // Ű���尡 �����ִٸ�, Ű���忡 �Էµ� ������ InputField�� �ǽð����� �ݿ��մϴ�.
        if (virtualKeyboard != null)
        {
            inputField.text = virtualKeyboard.text;

            // Ű���尡 �������� Ȯ���ϰ�, �������� ������ null�� ������ݴϴ�.
            if (virtualKeyboard.status != TouchScreenKeyboard.Status.Visible)
            {
                virtualKeyboard = null;
            }
        }
    }
}