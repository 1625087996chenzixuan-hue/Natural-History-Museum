using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public string targetScene;
    public string targetSpawnName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeManager.Instance.Teleport(targetScene, targetSpawnName);
        }
    }
}