using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_manager : MonoBehaviour
{
    public static scr_player_manager instance; // Singleton instance
    public Vector3 playerTransform; // Reference to the player's transform


    private void Update()
    {
        playerTransform = this.transform.position;
    }
    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple PlayerManager instances found. Destroying this one.");
            Destroy(gameObject);
        }
    }

    // Method to get the player's position
    public Vector3 GetPlayerPosition()
    {
        // Check if playerTransform is assigned
        if (playerTransform != null)
        {
            return playerTransform;
        }
        else
        {
            Debug.LogWarning("Player transform is not assigned!");
            return Vector3.zero; // Return zero vector if playerTransform is not assigned
        }
    }
}
