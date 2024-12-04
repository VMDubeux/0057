using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSequencePlayer : MonoBehaviour
{
    [System.Serializable]
    public class Frame
    {
        public Sprite sprite;
        public float duration = 0.1f; // Dura��o espec�fica para este frame
    }

    public Frame[] imageSequence; // Sequ�ncia de frames com dura��es espec�ficas
    public float fadeDuration = 0.5f; // Dura��o do fade in/out
    public GameObject background; // Objeto de fundo que cobre a tela
    public GameObject dicas;
    public Color yellowScreenColor = new Color(1, 1, 0, 1); // Cor do fade amarelo (padr�o amarelo)
    private Image uiImage;
    private bool isPlaying = false;

    void Awake()
    {
        uiImage = GetComponent<Image>();
        uiImage.enabled = false;
        uiImage.color = new Color(1, 1, 1, 0); // Transparente no in�cio
        if (background != null)
        {
            background.SetActive(false); // Certifique-se de que o fundo esteja desativado inicialmente
        }
    }

    public void PlaySequence()
    {
        if (!isPlaying && imageSequence.Length > 0)
        {
            StartCoroutine(PlayImages());
        }
    }

    private IEnumerator PlayImages()
    {
        isPlaying = true;
        background.SetActive(true); // Ativar fundo no in�cio da sequ�ncia
        uiImage.enabled = true;

        for (int i = 0; i < imageSequence.Length; i++)
        {
            if (i == 4) // Exibir tela amarela antes da 5� imagem
            {
                yield return StartCoroutine(ShowYellowScreen());
            }

            yield return StartCoroutine(FadeIn(imageSequence[i].sprite));
            yield return new WaitForSeconds(imageSequence[i].duration);
            yield return StartCoroutine(FadeOut());
        }

        uiImage.enabled = false;
        background.SetActive(false); // Desativar fundo no final da sequ�ncia
        dicas.SetActive(true);
        isPlaying = false;
    }

    private IEnumerator FadeIn(Sprite newSprite)
    {
        uiImage.sprite = newSprite;
        uiImage.color = new Color(1, 1, 1, 0); // Inicia totalmente transparente
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            uiImage.color = new Color(1, 1, 1, normalizedTime); // Manipula apenas o alfa
            yield return null;
        }
        uiImage.color = new Color(1, 1, 1, 1); // Totalmente vis�vel
    }

    private IEnumerator FadeOut()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            uiImage.color = new Color(1, 1, 1, 1 - normalizedTime); // Reduz apenas o alfa
            yield return null;
        }
        uiImage.color = new Color(1, 1, 1, 0); // Totalmente transparente
    }

    private IEnumerator ShowYellowScreen()
    {
        uiImage.sprite = null; // Remove qualquer imagem
        uiImage.color = new Color(yellowScreenColor.r, yellowScreenColor.g, yellowScreenColor.b, 1); // Usa a cor configurada
        yield return new WaitForSeconds(fadeDuration);
    }
}
