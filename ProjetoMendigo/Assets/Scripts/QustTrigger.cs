using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public int recompensa = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDinheiro playerDinheiro = other.GetComponent<PlayerDinheiro>();
            if (playerDinheiro != null)
            {
                playerDinheiro.AdicionarDinheiro(recompensa);
                Destroy(gameObject); 
            }
        }
    }
}
