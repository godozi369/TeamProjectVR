using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro; // TextMeshPro�� ����ϱ� ���� �ʿ��մϴ�.
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    private string scriptURL = "https://script.google.com/macros/s/AKfycbxsVIFFP0aRSLTljhqnSI0KAso8jv3Hx3UPdIiWsl1UynSyyk1EVABCf2Fpz6WwzcNn/exec";


    [Header("UI Elements")]
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public TextMeshProUGUI statusText;

    void Start()
    {

        if (loginButton != null)
        {
            loginButton.onClick.AddListener(OnLoginButtonClick);
        }
    }


    public void OnLoginButtonClick()
    {
        string userId = idInputField.text;
        string password = passwordInputField.text;

        // ���̵� ��й�ȣ�� ����ִ��� Ȯ���մϴ�.
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        {
            statusText.text = "���̵�� ��й�ȣ�� ��� �Է��ϼ���.";
            return;
        }

        statusText.text = "�α��� ��...";

        // �ڷ�ƾ�� ����Ͽ� �񵿱� �� ��û�� �����մϴ�.
        StartCoroutine(LoginRequest(userId, password));
    }


    /// <param name="userId">����� ���̵�</param>
    /// <param name="password">����� ��й�ȣ</param>
    IEnumerator LoginRequest(string userId, string password)
    {
        // ���� ���� ������ ����� ����ϴ�.
        WWWForm form = new WWWForm();
        form.AddField("action", "login"); // Apps Script�� � ����� ȣ������ ����
        form.AddField("userId", userId);
        form.AddField("password", password);

        // POST ������� �� ��û�� �����ϰ� �����ϴ�.
        using (UnityWebRequest www = UnityWebRequest.Post(scriptURL, form))
        {
            yield return www.SendWebRequest(); // ��û�� ���� ������ ���⼭ ����մϴ�.

            // �� ��û�� �������� ���
            if (www.result == UnityWebRequest.Result.Success)
            {
                // �����κ��� ���� JSON ������ ���� �ؽ�Ʈ�� �Ľ�(�ؼ�)�մϴ�.
                string jsonResponse = www.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                // �������� "����" ������ ������ ���
                if (response.status == "success")
                {
                    statusText.text = response.message; // "Login successful" �Ǵ� "New user registered"
                    Debug.Log("�α��� ����! ������ �ε� �Ϸ�.");

                    // DontDestroyOnLoad�� ������ PlayerData �̱��� �ν��Ͻ��� �÷��̾� ������ �����մϴ�.
                    PlayerData.Instance.SetData(response.data.userId, response.data.maxFishSize, response.data.fishCaughtList, response.data.points);

                    // 1�� �� �κ� ������ �̵��մϴ�.
                    yield return new WaitForSeconds(1);
                    SceneManager.LoadScene("Lobby");
                }
                else // �������� "����" ������ ������ ��� (��: ��й�ȣ ����)
                {
                    statusText.text = "�α��� ����: " + response.message;
                }
            }
            else // ��Ʈ��ũ ���� ��ü�� �������� ���
            {
                statusText.text = "��Ʈ��ũ ����: " + www.error;
                Debug.LogError("Web Request Error: " + www.error);
            }
        }
    }


    private void OnDestroy()
    {

        if (loginButton != null)
        {
            loginButton.onClick.RemoveListener(OnLoginButtonClick);
        }
    }
}



[System.Serializable]
public class LoginResponse
{
    public string status;
    public string message;
    public PlayerDataFields data;
}


[System.Serializable]
public class PlayerDataFields
{
    public string userId;
    public float maxFishSize;
    public string fishCaughtList;
    public int points;
}