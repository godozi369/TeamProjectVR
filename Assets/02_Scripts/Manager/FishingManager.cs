using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    [Header("���� ���� ������Ʈ")]
    public GameObject fishingRodObject; // ���� ��ġ�� ���˴븦 ���⿡ ����
    public Transform rodTip;
    public GameObject bobberPrefab;
    public LayerMask fishingZoneLayer;

    private GameObject currentBobber;
    private LineRenderer fishingLine;
    private bool isFishing = false;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        // ���� ���� �� ���˴븦 ��Ȱ��ȭ
        if (fishingRodObject != null)
        {
            fishingRodObject.SetActive(false);
        }
    }

    // '�����ϱ�' ��ư�� ȣ���� ���ο� �Լ�
    public void EnterFishingMode()
    {
        // ���˴븦 Ȱ��ȭ�ϰ� UI�� ����
        if (fishingRodObject != null)
        {
            fishingRodObject.SetActive(true);
        }
        FindObjectOfType<FishingDetector>().fishingPromptUI.SetActive(false);
    }

    // ���� ĳ������ �����ϴ� �Լ� (CastingController�� ȣ��)
    public void CastBobber(Vector3 castVelocity)
    {
        currentBobber = Instantiate(bobberPrefab, rodTip.position, Quaternion.identity);
        Rigidbody bobberRb = currentBobber.GetComponent<Rigidbody>();

        // ���� ������ � �߻�!
        bobberRb.AddForce(castVelocity, ForceMode.Impulse);

        fishingLine = rodTip.GetComponentInParent<LineRenderer>();
        if (fishingLine != null)
        {
            fishingLine.enabled = true;
            isFishing = true;
        }
    }

    void Update()
    {
        if (isFishing && fishingLine != null && currentBobber != null && rodTip != null)
        {
            fishingLine.SetPosition(0, rodTip.position);
            fishingLine.SetPosition(1, currentBobber.transform.position);
        }
    }

    public void CancelFishing()
    {
        if (fishingRodObject != null) fishingRodObject.SetActive(false);
        if (currentBobber != null) Destroy(currentBobber);
        if (fishingLine != null) fishingLine.enabled = false;
        isFishing = false;
    }
}