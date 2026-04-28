using UnityEngine;

public class FoodDisplay : MonoBehaviour
{
    public FoodItem foodData;
    private Material displayMaterial;
    private bool isHighlighted = false;

    void Start()
    {
        if (foodData == null)
        {
            Debug.LogError("FoodDisplay on " + gameObject.name + " has no FoodItem assigned!");
            return;
        }

        // Color the cube to match the food
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            displayMaterial = new Material(rend.material);
            displayMaterial.color = foodData.displayColor;
            rend.material = displayMaterial;
        }

        // Add collider if missing
        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    void OnMouseEnter()
    {
        isHighlighted = true;
        // Brighten the material on hover
        if (displayMaterial != null)
            displayMaterial.color = foodData.displayColor * 1.3f;
    }

    void OnMouseExit()
    {
        isHighlighted = false;
        if (displayMaterial != null)
            displayMaterial.color = foodData.displayColor;
    }

    void OnMouseDown()
    {
        // Try to purchase
        if (GameManager.Instance.TryPurchase(foodData))
        {
            // Visual feedback: scale down briefly
            StartCoroutine(PurchaseFeedback());
        }
        else
        {
            // Not enough budget - flash red
            StartCoroutine(ErrorFeedback());
        }
    }

    System.Collections.IEnumerator PurchaseFeedback()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale *= 0.8f;
        yield return new WaitForSeconds(0.15f);
        transform.localScale = originalScale;
    }

    System.Collections.IEnumerator ErrorFeedback()
    {
        Color originalColor = displayMaterial.color;
        displayMaterial.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        displayMaterial.color = originalColor;
    }
}