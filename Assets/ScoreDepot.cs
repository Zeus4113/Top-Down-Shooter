using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDepot : MonoBehaviour
{
    [SerializeField] private int m_maxScore;

    private int m_currentScoreDeposited;
    private bool m_isComplete;
    private bool m_playerPresent;

    private void Start()
    {
        m_isComplete = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null) return;

        GameObject myObject = collision.gameObject;

        if (myObject.CompareTag("Player"))
        {
            m_playerPresent = true;
            Debug.Log("Player Present");
            ScoreTracker tracker = myObject.GetComponent<ScoreTracker>();
            if (tracker.GetScore() > 0)
            {
                StartCoroutine(DepositScore(tracker.GetScore()));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        GameObject myObject = collision.gameObject;

        if (myObject.CompareTag("Player"))
        {
            m_playerPresent = false;
            ScoreTracker tracker = myObject.GetComponent<ScoreTracker>();
            StopCoroutine(DepositScore(tracker.GetScore()));
        }
    }

    private IEnumerator DepositScore(int playerScore)
    {

        for (int i = 0; i < playerScore; i++)
        {
            m_currentScoreDeposited++;
            Debug.Log(m_currentScoreDeposited);

            if(m_currentScoreDeposited >= m_maxScore)
            {
                Complete();
                break;
            }

            if (!m_playerPresent)
            {
                break;
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    private void Complete()
    {
        m_isComplete = true;
        Debug.Log("Is Complete");
    }
}
