using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "SmartShop/Food Item")]
public class FoodItem : ScriptableObject
{
    public string foodName;
    public float price;
    
    [Header("Nutrition Values (0-100)")]
    public int protein;      // 0-100
    public int vitamins;     // 0-100
    public int fiber;        // 0-100
    public int calories;     // calories per unit
    
    [Header("Display")]
    public Color displayColor = Color.white;
    public Sprite icon;
    
    // Calculate nutrition score
    public float GetNutritionScore()
    {
        return protein * 0.4f + vitamins * 0.3f + fiber * 0.2f + Mathf.Clamp(calories, 0, 50) * 0.1f;
    }
}