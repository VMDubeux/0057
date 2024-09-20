using UnityEngine;

public class MercadorInteracao : MonoBehaviour
{
    private float tempoAtivo = 3f;
    [SerializeField] private float tempoAtual = 0f;
    [SerializeField] private bool estaAtivo = false;
    private Animator animador;

    private void Start()
    {
        // Cache do componente Animator no início
        animador = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            AtivarInteracao();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            DesativarInteracao();
    }

    private void Update()
    {
        if (estaAtivo)
        {
            tempoAtual += Time.deltaTime * Time.timeScale;

            if (tempoAtual >= tempoAtivo)
            {
                DesativarInteracao();
            }
        }
    }

    private void AtivarInteracao()
    {
        animador.SetBool("Trigger", true);
        estaAtivo = true;
        tempoAtual = 0f; // Reinicia o temporizador
    }

    private void DesativarInteracao()
    {
        animador.SetBool("Trigger", false);
        estaAtivo = false;
        tempoAtual = 0f; // Reinicia o temporizador
    }
}
