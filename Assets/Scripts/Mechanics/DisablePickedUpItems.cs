using UnityEngine;

public class DisablePickedUpItems : MonoBehaviour
{
    public string itemName; // Same unique identifier

    private void Start()
    {
        Debug.Log($"Checking item {itemName} at start...");
        if (PickUpManager.Instance.HasItem(itemName))
        {
            Debug.Log($"{itemName} has already been collected. Destroying object.");
            Destroy(gameObject); // Or gameObject.SetActive(false);
        }
        else
        {
            Debug.Log($"{itemName} has not been collected yet.");
        }
    }
}
