using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MissionZone : MonoBehaviour
{
    [SerializeField] private string objective = "Explore the city";
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private bool playerNearby;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(interactKey))
        {
            Debug.Log("Objective started: " + objective);
            MissionHUD.Show(objective);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            MissionHUD.Show("Press E: " + objective);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            MissionHUD.Show("");
        }
    }
}
