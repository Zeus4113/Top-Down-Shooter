using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDepot : MonoBehaviour
{
    [SerializeField] private int m_maxScore;
    [SerializeField] private GameObject m_scoreUI;
    [SerializeField] private EnemySpawnManager m_spawnManager;


    private int m_currentScoreDeposited;
    private bool m_isComplete;
    private bool m_playerPresent;
    private SpriteRenderer m_spriteRenderer;
    private TMPro.TMP_Text m_textMeshPro;

    public delegate void ScoreDeposited(int amount);
    public static ScoreDeposited depositTick;

    public void Init()
    {
        m_isComplete = false;
        m_playerPresent = false;
        m_currentScoreDeposited = 0;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.size = new Vector2(0.1f, 0.1f);
        m_textMeshPro = m_scoreUI.GetComponent<TMPro.TMP_Text>();
        m_textMeshPro.text = (m_currentScoreDeposited.ToString() + " / " + m_maxScore.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        GameObject myObject = collision.gameObject;

        if (myObject.CompareTag("Player"))
        {
            m_playerPresent = true;
            ScoreTracker scoreTracker = myObject.GetComponent<ScoreTracker>();
            StartCoroutine(DepositScore(scoreTracker.GetScore()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        GameObject myObject = collision.gameObject;

        if (myObject.CompareTag("Player"))
        {
            m_playerPresent = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator DepositScore(int currentScore)
    {
        while (m_playerPresent && !m_isComplete)
        {
            for(int i = 0; i < currentScore; i++)
            {
                m_currentScoreDeposited++;
                m_textMeshPro.text = (m_currentScoreDeposited.ToString() + " / " + m_maxScore.ToString());
                depositTick?.Invoke(-1);
                m_spriteRenderer.size = new Vector2(m_currentScoreDeposited * 0.004f, m_currentScoreDeposited * 0.004f);

                yield return new WaitForSeconds(0.04f);

                if(m_currentScoreDeposited >= m_maxScore)
                {
                    OnComplete();
                    break;
                }
            }

            break;
        }

        yield return null;
    }

    private void OnComplete()
    {
        m_isComplete = true;
        m_spawnManager.SetActive(false);

    }
}
