using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWindow : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(OpenMainScene());
    }

    private IEnumerator OpenMainScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }
}