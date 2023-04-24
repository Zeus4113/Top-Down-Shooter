using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDepot : MonoBehaviour
{
    [SerializeField] private float m_maxScore;
    [SerializeField] private GameObject m_scoreUI;
    [SerializeField] private EnemySpawnManager m_spawnManager;
	[SerializeField] private GameObject[] m_doors;
	[SerializeField] private Transform[] m_wires;


    private float m_currentScoreDeposited;
	private Image m_fillBar;
    private bool m_isComplete;
    private bool m_playerPresent;
    private SpriteRenderer m_spriteRenderer;
	private DepotTracker m_depotTracker;

    public delegate void ScoreDeposited(float amount);
    public static ScoreDeposited depositTick;

	public delegate void UpdateDeposit(float maxAmount, float currentAmount);
	public static UpdateDeposit setupHUD;

	public delegate void ResetDeposit();
	public static ResetDeposit resetHUD;

	public void Init()
	{
		m_isComplete = false;
		m_playerPresent = false;
		m_currentScoreDeposited = 0;
		m_spriteRenderer = GetComponent<SpriteRenderer>();
		//m_spriteRenderer.size = new Vector2(0.1f, 0.1f);

		m_fillBar = transform.GetChild(1).GetChild(1).GetComponent<Image>();
		m_fillBar.fillAmount = 0;

		SetupHUD(true);
	}

	public void SetupHUD(bool isTrue)
	{
		if (isTrue)	setupHUD?.Invoke(m_maxScore, m_currentScoreDeposited);
		else resetHUD?.Invoke();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;

        GameObject myObject = collision.gameObject;

        if (myObject.CompareTag("Player"))
        {
            m_playerPresent = true;
            PlayerController playerRef = myObject.GetComponent<PlayerController>();
            StartCoroutine(DepositScore(playerRef.GetScore()));
			Debug.Log(playerRef.GetScore());
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

    private IEnumerator DepositScore(float currentScore)
    {
        while (m_playerPresent && !m_isComplete)
        {
            for(int i = 0; i < currentScore; i++)
            {
                m_currentScoreDeposited++;
				setupHUD?.Invoke(m_maxScore, m_currentScoreDeposited);
				depositTick?.Invoke(-1);


                //m_spriteRenderer.size = new Vector2(m_currentScoreDeposited * 0.004f, m_currentScoreDeposited * 0.004f);
				m_fillBar.fillAmount = m_currentScoreDeposited / m_maxScore;
				Debug.Log("Current Score = " + m_currentScoreDeposited);
				Debug.Log("Max Score = " + m_maxScore);


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

	public void SetDepotTracker(GameObject myObject)
	{
		m_depotTracker = myObject?.GetComponent<DepotTracker>();
	}

    private void OnComplete()
    {
        m_isComplete = true;
        m_spawnManager.SetActive(false);

		for(int i = 0; i < m_doors.Length; i++)
		{
			m_doors[i]?.GetComponent<Door>().Unlock();
		}

		for(int j = 0; j < m_wires.Length; j++)
		{
			m_wires[j].GetComponent<SpriteRenderer>().color = Color.green;
		}


		m_depotTracker.OnDepotCompleted(gameObject.transform);
    }

	public bool IsComplete() { return m_isComplete; }
}
