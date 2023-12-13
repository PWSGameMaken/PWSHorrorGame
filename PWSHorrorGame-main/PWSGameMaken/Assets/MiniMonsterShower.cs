using UnityEngine;
using System.Collections;

public class MiniMonsterShower : MonoBehaviour
{
    [SerializeField] private AudioSource scarySound;
    [SerializeField] private AudioSource footstepSound;
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Vector3 moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyDelay;

    private bool playerInsideTrigger = false;

    private Renderer objectRenderer; // Reference to the object's renderer

    private void Start()
    {
        // Get the renderer component
        objectRenderer = objectToMove.GetComponent<Renderer>();

        // Make the object initially invisible
        objectRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = true;

            // Make the object visible
            objectRenderer.enabled = true;

            StartCoroutine(PlayAudioAndDestroy());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            MoveObject();
            StartCoroutine(DestroyObjectWithDelay(destroyDelay));
        }
    }

    private void Update()
    {
        if (playerInsideTrigger)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        objectToMove.Translate(moveSpeed * Time.deltaTime * moveDirection.normalized);
    }

    private IEnumerator PlayAudioAndDestroy()
    {
        scarySound.Play();
        footstepSound.Play();

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Continue moving the object
        MoveObject();

        // Wait for additional seconds before destroying
        yield return new WaitForSeconds(destroyDelay - 2f);

        // Play scary sound again before destroying
        scarySound.Play();

        // Wait for a short time before destroying
        yield return new WaitForSeconds(0.5f);

        // Destroy the object
        Destroy(gameObject);
    }

    private IEnumerator DestroyObjectWithDelay(float delay)
    {
        // Wait for the specified delay before destroying
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}