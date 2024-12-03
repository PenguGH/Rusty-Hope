using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager Instance { get; private set; }

    private HashSet<string> collectedItems = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("Cannot add a null or empty item to the inventory.");
            return;
        }
        if (!collectedItems.Contains(itemName)) // Ensure the item doesn't already exist
        {
            collectedItems.Add(itemName);
            Debug.Log($"Item {itemName} added to inventory.");
        }
        else
        {
            Debug.LogWarning($"Item {itemName} already exists in inventory.");
        }
    }

    public bool HasItem(string itemName)
    {
        bool exists = collectedItems.Contains(itemName);
        Debug.Log($"Checking if {itemName} is in inventory: {exists}");
        return exists;
    }
}
