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
        if (itemDelivered) return; // se já entregou, não faz nada

        PlayerInventory player = other.GetComponent<PlayerInventory>();
        if (player != null && player.itemCount > 0)
        {
            if (player.RemoveItems(1))
            {
                itemDelivered = true;
                Debug.Log($"Item entregue na área {gameObject.name}!");
                // Aqui você pode fazer algo tipo ativar porta, dar recompensa, etc.
            }
        }
        else
        {
            Debug.Log("Você não tem itens para entregar!");
        }
    }
}
