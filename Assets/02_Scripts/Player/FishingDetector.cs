using UnityEngine;

public class FishingDetector : MonoBehaviour
{
    [Header("���� ���� UI")]
    public GameObject fishingPromptUI; // �ν����Ϳ��� Canvas�� �����մϴ�.

    void Start()
    {
        // ������ ���� UI�� ��Ȱ��ȭ�մϴ�.
        if (fishingPromptUI != null)
        {
            fishingPromptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� Ʈ������ ���̾ FishingZone�̸�
        if (other.gameObject.layer == LayerMask.NameToLayer("FishingZone"))
        {
            Debug.Log("���� ���� ����");
            if (fishingPromptUI != null)
            {
                fishingPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���� Ʈ������ ���̾ FishingZone�̸�
        if (other.gameObject.layer == LayerMask.NameToLayer("FishingZone"))
        {
            Debug.Log("���� ���� ��Ż");
            if (fishingPromptUI != null)
            {
                fishingPromptUI.SetActive(false);
            }
        }
    }
}