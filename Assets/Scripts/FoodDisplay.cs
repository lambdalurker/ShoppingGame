using UnityEngine;
using System.Collections;

public class FoodDisplay : MonoBehaviour
{
    public FoodItem foodData;
    private bool isHighlighted = false;

    void Start()
    {
        if (foodData == null)
        {
            Debug.LogError("FoodDisplay on " + gameObject.name + " has no FoodItem assigned!");
            return;
        }

        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    void OnMouseEnter() { isHighlighted = true; }
    void OnMouseExit() { isHighlighted = false; }

    void OnMouseDown()
    {
        if (GameManager.Instance.TryPurchase(foodData))
        {
            StartCoroutine(PurchaseFeedback());
        }
        else
        {
            StartCoroutine(ErrorFeedback());
        }
    }

    IEnumerator PurchaseFeedback()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale *= 0.8f;
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }

    IEnumerator ErrorFeedback()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale *= 1.2f;
        yield return new WaitForSeconds(0.25f);
        transform.localScale = originalScale;
    }
}