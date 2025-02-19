using UnityEngine;
using UnityEngine.UI;
using TMPro; // Для работы с TextMeshPro

public class StarRating : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite filledStar;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField reviewInput;
    [SerializeField] private Button sendButton;

    private int currentRating = 0;

    private void Start()
    {
        sendButton.interactable = false;
    }

    public void SetRating(int rating)
    {
        currentRating = rating;

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = (i < rating) ? filledStar : emptyStar;
        }

        CheckFormCompletion();
    }

    public void OnStarClick(int starIndex)
    {
        SetRating(starIndex + 1);
    }

    public void CheckFormCompletion()
    {
        bool isFormComplete = currentRating > 0 &&
                              !string.IsNullOrWhiteSpace(nameInput.text) &&
                              !string.IsNullOrWhiteSpace(reviewInput.text);

        sendButton.interactable = isFormComplete;
    }

    public void OnInputChanged()
    {
        CheckFormCompletion();
    }
}
