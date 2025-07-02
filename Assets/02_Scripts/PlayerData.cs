using UnityEngine;

// �÷��̾� �����͸� ���� ��ü���� ���� �����ϵ��� �����ϴ� �̱��� Ŭ����
public class PlayerData : MonoBehaviour
{
    // static ������ �ν��Ͻ��� �����Ͽ� ��𼭵� PlayerData.Instance�� ���� ����
    public static PlayerData Instance { get; private set; }

    public string UserID { get; private set; }
    public float MaxFishSize { get; set; }
    public string FishCaughtList { get; set; }
    public int Points { get; set; }

    private void Awake()
    {
        // ���� PlayerData �ν��Ͻ��� ���� ���, ���� �ν��Ͻ��� �Ҵ�
        if (Instance == null)
        {
            Instance = this;
            // ���� ��ȯ�Ǿ �� ���� ������Ʈ�� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� �ν��Ͻ��� �����ϸ�, ���� �ν��Ͻ��� �ߺ��̹Ƿ� �ı�
            Destroy(gameObject);
        }
    }

    // �α��� �� �����κ��� ���� �����ͷ� �ʱ�ȭ�ϴ� �Լ�
    public void SetData(string id, float maxSize, string fishList, int pts)
    {
        UserID = id;
        MaxFishSize = maxSize;
        FishCaughtList = fishList;
        Points = pts;
    }
}