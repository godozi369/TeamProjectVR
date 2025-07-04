using UnityEngine;

public class RodController : MonoBehaviour
{
    [Header("���˴� ������")]
    public GameObject fishingRodPrefab;

    private GameObject currentRod;

    // Start ��� Awake�� ����Ͽ� �� ���� ����ǰ� �մϴ�.
    void Awake()
    {
        // 1. FishingManager �ν��Ͻ��� �ִ��� Ȯ���մϴ�.
        if (FishingManager.instance == null)
        {
            Debug.LogError("FishingManager.instance�� ã�� ���߽��ϴ�!");
            return;
        }

        // 2. ���˴� �������� �Ҵ�Ǿ����� Ȯ���մϴ�.
        if (fishingRodPrefab != null)
        {
            // 3. ���˴븦 �����մϴ�.
            currentRod = Instantiate(fishingRodPrefab, transform);
            currentRod.transform.localPosition = new Vector3(0, -0.05f, 0.5f);
            currentRod.transform.localRotation = Quaternion.Euler(45f, 0, 0);

            // 4. ������ ���˴뿡�� RodInfo ������Ʈ�� �����ɴϴ�.
            RodInfo rodInfo = currentRod.GetComponentInChildren<RodInfo>();

            // 5. RodInfo�� �� ���� rodTip�� ��ȿ�ϴٸ�, FishingManager�� �Ҵ��մϴ�.
            if (rodInfo != null && rodInfo.rodTip != null)
            {
                FishingManager.instance.rodTip = rodInfo.rodTip;
                Debug.Log("����: RodInfo�� ���� RodTip�� FishingManager�� �Ҵ�Ǿ����ϴ�!");
            }
            else
            {
                Debug.LogError("����: ������ ���˴� �����տ��� RodInfo ������Ʈ�� �� ���� RodTip�� ã�� �� �����ϴ�.");
            }
        }
    }
}