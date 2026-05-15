using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Slider budgetSlider;
    public TextMeshProUGUI budgetText;

    void Start()
    {
        budgetSlider.onValueChanged.AddListener(UpdateBudgetText);
        UpdateBudgetText(budgetSlider.value);
    }

    void UpdateBudgetText(float value)
    {
        budgetText.text = "Budget: $" + value.ToString("F0");
    }

    public void StartGame()
    {
        PlayerPrefs.SetFloat("SelectedBudget", budgetSlider.value);
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}