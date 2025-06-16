using UnityEngine;

public class DeliveryArea : MonoBehaviour
{
    public bool IsDelivered()
    {
        return itemDelivered;
    }

    private bool itemDelivered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (itemDelivered) return; // se j� entregou, n�o faz nada

        PlayerInventory player = other.GetComponent<PlayerInventory>();
        if (player != null && player.itemCount > 0)
        {
            if (player.RemoveItems(1))
            {
                itemDelivered = true;
                Debug.Log($"Item entregue na �rea {gameObject.name}!");
                // Aqui voc� pode fazer algo tipo ativar porta, dar recompensa, etc.
            }
        }
        else
        {
            Debug.Log("Voc� n�o tem itens para entregar!");
        }
    }
}
