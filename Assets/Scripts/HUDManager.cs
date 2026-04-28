using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI cartText;
    public Button reportButton;

    void Start()
    {
        reportButton.onClick.AddListener(ShowReport);
        InvokeRepeating(nameof(UpdateHUD), 0f, 0.1f);
    }

    void UpdateHUD()
    {
        if (GameManager.Instance == null) return;

        float remaining = GameManager.Instance.GetRemainingBudget();
        int itemCount = GameManager.Instance.GetPurchasedItems().Count;

        budgetText.text = $"Budget: ${remaining:F2}";
        cartText.text = $"Items: {itemCount}";
    }

    void ShowReport()
    {
        SceneManager.LoadScene("ReportScene");
    }
}