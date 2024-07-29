using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStairs : MonoBehaviour
{
    [SerializeField] GameObject MessageScreen;
    public GameObject Stairs;

    private bool hasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            MessageScreen.SetActive(true);
            Stairs.SetActive(true);

            GetComponent<Collider>().enabled = false;
            hasActivated = true;

            StartCoroutine(DisableMessageAndStairsAfterDelay(4f));
        }
    }

    IEnumerator DisableMessageAndStairsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MessageScreen.SetActive(false);
    }
}
