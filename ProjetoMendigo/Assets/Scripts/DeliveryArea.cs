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
        if (itemDelivered) return; 

        PlayerInventory player = other.GetComponent<PlayerInventory>();
        if (player != null && player.itemCount > 0)
        {
            if (player.RemoveItems(1))
            {
                itemDelivered = true;
                Debug.Log($"Item entregue na área {gameObject.name}!");
                
            }
        }
        else
        {
            Debug.Log("Você não tem itens para entregar!");
        }
    }
}
