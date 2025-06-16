using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    public int itemsToGive = 18; 

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory player = other.GetComponent<PlayerInventory>();
        if (player != null)
        {
            player.AddItems(itemsToGive);
            Debug.Log("Player ganhou 18 itens!");
            gameObject.SetActive(false); 
        }
    }
}
