using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Budget")]
    public float startingBudget = 20f;
    private float remainingBudget;
    private float totalSpent = 0f;
    
    private List<PurchasedItem> purchasedItems = new List<PurchasedItem>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Get budget from main menu slider
        startingBudget = PlayerPrefs.GetFloat("SelectedBudget", 20f);
        remainingBudget = startingBudget;
    }

    public bool TryPurchase(FoodItem item)
    {
        if (item.price > remainingBudget)
        {
            Debug.Log("Not enough budget!");
            return false;
        }

        remainingBudget -= item.price;
        totalSpent += item.price;
        purchasedItems.Add(new PurchasedItem { item = item, quantity = 1 });
        
        Debug.Log($"Bought {item.foodName} for ${item.price}. Remaining: ${remainingBudget:F2}");
        return true;
    }

    public float GetRemainingBudget() => remainingBudget;
    public float GetTotalSpent() => totalSpent;
    public float GetTotalNutrition()
    {
        float total = 0f;
        foreach (var item in purchasedItems)
            total += item.item.GetNutritionScore() * item.quantity;
        return total;
    }

    public List<PurchasedItem> GetPurchasedItems() => purchasedItems;
    public void ResetCart()
    {
        purchasedItems.Clear();
        totalSpent = 0f;
        remainingBudget = startingBudget;
    }
}

[System.Serializable]
public class PurchasedItem
{
    public FoodItem item;
    public int quantity;
}