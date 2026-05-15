using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReportManager : MonoBehaviour
{
    [Header("UI References")]

    public TextMeshProUGUI TitleText;

    // Spent Card
    public TextMeshProUGUI SpentCardText;

    // Nutrition Card
    public TextMeshProUGUI NutritionCardText;

    // Items Card
    public TextMeshProUGUI ItemsCardText;

    // Optional details section
    public TextMeshProUGUI DetailsText;

    // Button
    public Button MainMenuButton;

    void Start()
    {
        // Check GameManager
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        // Get data
        float spent = GameManager.Instance.GetTotalSpent();
        int itemCount = GameManager.Instance.GetPurchasedItems().Count;
        float nutrition = GameManager.Instance.GetTotalNutrition();
        float remaining = GameManager.Instance.GetRemainingBudget();

        // ---------- TITLE ----------
        TitleText.text = "SHOPPING COMPLETE!";

        // ---------- CARD VALUES ----------
        SpentCardText.text = "$" + spent.ToString("F2");

        ItemsCardText.text = itemCount.ToString();

        NutritionCardText.text = nutrition.ToString("F0");

        // Nutrition color
        if (nutrition >= 80)
        {
            NutritionCardText.color = Color.green;
        }
        else if (nutrition >= 50)
        {
            NutritionCardText.color = Color.yellow;
        }
        else
        {
            NutritionCardText.color = Color.red;
        }

        // ---------- DETAILS ----------
        if (DetailsText != null)
        {
            string details = "";

            if (itemCount == 0)
            {
                details = "You didn't buy any items.";
            }
            else
            {
                details += "ITEMS PURCHASED\n\n";

                foreach (var purchase in GameManager.Instance.GetPurchasedItems())
                {
                    details +=
                        $"• {purchase.item.foodName} " +
                        $"x{purchase.quantity}  " +
                        $"- ${purchase.item.price * purchase.quantity:F2}\n";
                }

                details += $"\nRemaining Budget: ${remaining:F2}";
            }

            DetailsText.text = details;
        }

        // ---------- BUTTON ----------
        MainMenuButton.onClick.RemoveAllListeners();

        MainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }
}