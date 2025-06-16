using UnityEngine;
using TMPro;

public class PlayerDinheiro : MonoBehaviour
{
    public int dinheiroAtual = 0;
    public TMP_Text textoDinheiro;

    void Start()
    {
        AtualizarUI();
    }

    public void AdicionarDinheiro(int valor)
    {
        dinheiroAtual += valor;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (textoDinheiro != null)
        {
            textoDinheiro.text = "R$" + dinheiroAtual;
        }
    }
}
