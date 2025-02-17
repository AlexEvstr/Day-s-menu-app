using UnityEngine;
using UnityEngine.UI;
using TMPro; // Для работы с TextMeshPro

public class StarRating : MonoBehaviour
{
    [SerializeField] private Image[] stars; // Массив звезд
    [SerializeField] private Sprite filledStar; // Заполненная звезда
    [SerializeField] private Sprite emptyStar; // Пустая звезда
    [SerializeField] private TMP_InputField nameInput; // Поле ввода имени
    [SerializeField] private TMP_InputField reviewInput; // Поле ввода отзыва
    [SerializeField] private Button sendButton; // Кнопка "Send"

    private int currentRating = 0;

    private void Start()
    {
        sendButton.interactable = false; // Отключаем кнопку при старте
    }

    public void SetRating(int rating)
    {
        currentRating = rating; // Запоминаем рейтинг

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = (i < rating) ? filledStar : emptyStar;
        }

        CheckFormCompletion(); // Проверяем, можно ли включить кнопку
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
        CheckFormCompletion(); // Проверка при изменении текста
    }
}
