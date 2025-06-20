using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int itemCount = 0;

    // Adiciona itens
    public void AddItems(int amount)
    {
        itemCount += amount;
        Debug.Log("Itens recebidos. Total agora: " + itemCount);
    }

    // Remove itens (quando entregar)
    public bool RemoveItems(int amount)
    {
        if (itemCount >= amount)
        {
            itemCount -= amount;
            Debug.Log("Itens entregues. Restam: " + itemCount);
            return true;
        }
        else
        {
            Debug.Log("N�o tem itens suficientes para entregar!");
            return false;
        }
    }
}
