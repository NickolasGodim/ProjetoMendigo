using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public DeliveryArea[] deliveryAreas;

    void Update()
    {
        int completed = 0;
        foreach (var area in deliveryAreas)
        {
            if (area.IsDelivered())
                completed++;
        }
        Debug.Log($"Áreas entregues: {completed}/{deliveryAreas.Length}");
    }
}
