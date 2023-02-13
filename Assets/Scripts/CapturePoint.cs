using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private Color completedColour;
    [SerializeField] private GameObject[] ammoTypes; 

    private bool playerPresent;
    private bool isComplete;
    private float currentCount;
    private float maxCount;
    private float delay;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerPresent = false;
        isComplete = false;
        delay = 2f;
        currentCount = 0;
        maxCount = 15;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerPresent = true;
            Debug.Log("Player Detected");
            StartCoroutine(CountUp());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerPresent = false;
            Debug.Log("Player Lost");
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountUp()
    {
        while (playerPresent && !isComplete)
        {
            if(currentCount < maxCount)
            {
                currentCount++;
                Debug.Log(currentCount);
                yield return new WaitForSeconds(1f);
            }
            else if(currentCount >= maxCount)
            {
                currentCount = maxCount;
                Debug.Log(currentCount);
                OnComplete();
                break;
            }

        }

        yield return null;
    }

    IEnumerator CountDown()
    {
        while (!playerPresent && !isComplete)
        {
            if (currentCount > 0)
            {
                currentCount--;
                Debug.Log(currentCount);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                break;
            }
        }

        yield return null;
    }

    private void OnComplete()
    {
        isComplete = true;
        Debug.Log("Completed!");
        spriteRenderer.color = completedColour;
        SpawnAmmo();
    }

    private void SpawnAmmo()
    {
        for(int i = 0; i < Random.Range(3f, 4f); i++)
        {
            GameObject myAmmo = Instantiate(ammoTypes[Random.Range(0, ammoTypes.Length)], this.transform.position, this.transform.rotation);
            myAmmo.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode2D.Impulse);
        }
    }
}
