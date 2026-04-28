using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReportManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI spentText;
    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI nutritionText;
    public TextMeshProUGUI detailsText;
    public Button backButton;

    void Start()
{
    if (GameManager.Instance == null)
    {
        Debug.LogError("GameManager not found!");
        return;
    }

    // Check if all fields are assigned
    if (titleText == null || spentText == null || itemsText == null || 
        nutritionText == null || detailsText == null || backButton == null)
    {
        Debug.LogError("ReportManager: One or more UI fields are not assigned in the Inspector!");
        return;
    }

    float spent = GameManager.Instance.GetTotalSpent();
    int itemCount = GameManager.Instance.GetPurchasedItems().Count;
    float nutrition = GameManager.Instance.GetTotalNutrition();
    float remaining = GameManager.Instance.GetRemainingBudget();

    // Populate UI
    titleText.text = "Shopping Complete!";
    spentText.text = $"Total Spent: ${spent:F2}";
    itemsText.text = $"Items Bought: {itemCount}";
    nutritionText.text = $"Nutrition Score: {nutrition:F0}";
    
    // Build item list
    string details = "Items purchased:\n\n";
    foreach (var purchase in GameManager.Instance.GetPurchasedItems())
    {
        details += $"• {purchase.item.foodName} (x{purchase.quantity}) — ${purchase.item.price:F2}\n";
    }
    details += $"\nBudget Remaining: ${remaining:F2}";
    
    detailsText.text = details;
    backButton.onClick.AddListener(() => SceneManager.LoadScene("SampleScene"));
}
}