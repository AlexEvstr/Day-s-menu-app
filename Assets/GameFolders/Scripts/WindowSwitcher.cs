using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject windowToClose; // Окно, которое нужно закрыть
    [SerializeField] private GameObject windowToOpen;  // Окно, которое нужно открыть

    private CanvasGroup closeCanvasGroup;
    private CanvasGroup openCanvasGroup;

    private void Awake()
    {
        // Получаем или добавляем CanvasGroup для закрываемого окна
        if (windowToClose != null)
        {
            closeCanvasGroup = windowToClose.GetComponent<CanvasGroup>();
            if (closeCanvasGroup == null)
                closeCanvasGroup = windowToClose.AddComponent<CanvasGroup>();
        }

        // Получаем или добавляем CanvasGroup для открываемого окна
        if (windowToOpen != null)
        {
            openCanvasGroup = windowToOpen.GetComponent<CanvasGroup>();
            if (openCanvasGroup == null)
                openCanvasGroup = windowToOpen.AddComponent<CanvasGroup>();

            // Делаем окно невидимым, но включенным (чтобы работала анимация)
            windowToOpen.SetActive(true);
            openCanvasGroup.alpha = 0f;
            openCanvasGroup.interactable = false;
            openCanvasGroup.blocksRaycasts = false;
        }
    }

    public void SwitchWindows()
    {
        StartCoroutine(FadeOutAndIn());
    }

    private IEnumerator FadeOutAndIn()
    {
        if (windowToClose != null)
        {
            // Плавное исчезновение закрываемого окна
            for (float t = 1f; t >= 0f; t -= Time.deltaTime * 5)
            {
                closeCanvasGroup.alpha = t;
                yield return null;
            }
            closeCanvasGroup.alpha = 0f;
        }

        if (windowToOpen != null)
        {
            windowToOpen.SetActive(true); // Включаем новое окно
            openCanvasGroup.interactable = true;
            openCanvasGroup.blocksRaycasts = true;

            ScrollRect scrollView = windowToOpen.GetComponentInChildren<ScrollRect>();

            if (scrollView != null)
            {
                yield return null; // Ждем 1 кадр перед сбросом (чтобы ScrollRect прогрузился)
                scrollView.verticalNormalizedPosition = 1f; // Поднимаем контент вверх
            }

            // Плавное появление нового окна
            for (float t = 0f; t <= 1f; t += Time.deltaTime * 5)
            {
                openCanvasGroup.alpha = t;
                yield return null;
            }
            openCanvasGroup.alpha = 1f;
            windowToClose.SetActive(false);
        }
    }
}
