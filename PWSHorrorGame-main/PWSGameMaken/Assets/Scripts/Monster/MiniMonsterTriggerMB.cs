using UnityEngine;
using System.Collections;

public class MiniMonsterTriggerMB : MonoBehaviour
{
    [SerializeField] private AudioSource jumpScareSound;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _destroyDelay;

    private bool playerInsideTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            StartCoroutine(PlayAudioAndDestroy());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            MoveObject();
            StartCoroutine(DestroyObjectWithDelay(_destroyDelay));
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
        _objectToMove.Translate(_moveSpeed * Time.deltaTime * _moveDirection.normalized);
    }

    private IEnumerator PlayAudioAndDestroy()
    {
        jumpScareSound.Play();
        walkSound.Play();

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Continue moving the object
        MoveObject();

        // Wait for additional seconds before destroying
        yield return new WaitForSeconds(_destroyDelay - 2f);

        // Play jump scare sound again before destroying
        jumpScareSound.Play();

        // Wait for a short time before destroying
        yield return new WaitForSeconds(0.5f);

        // Destroy the object
        Destroy(this.gameObject);
    }

    private IEnumerator DestroyObjectWithDelay(float delay)
    {
        // Wait for the specified delay before destroying
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
