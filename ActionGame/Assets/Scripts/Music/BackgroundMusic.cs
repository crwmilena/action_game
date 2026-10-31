using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public float targetVolume = 0.5f;
    public float fadeDuration = 2f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.Play();

        StartCoroutine(FadeIn());
    }

    public void DuckVolume() //для выстрела
    {
        StopAllCoroutines();
        StartCoroutine(DuckRoutine());
    }

    IEnumerator DuckRoutine()//приглушение
    {
        audioSource.volume = targetVolume * 0.9f; //громкость немного падает
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() //появление
    {
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, currentTime / fadeDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
