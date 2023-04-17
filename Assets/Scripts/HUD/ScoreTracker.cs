using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private GameObject m_currentScoreUI;
	private GameObject m_depotScoreUI;
    private float currentScore;
    private TMPro.TMP_Text m_textMeshPro_1;
	private TMPro.TMP_Text m_textMeshPro_2;

    private void Start()
    {
		Transform objectHolder = gameObject.transform.GetChild(1);
		Transform objectHolder2 = gameObject.transform.GetChild(2);

		m_textMeshPro_1 = objectHolder.GetChild(0).GetComponent<TMPro.TMP_Text>();
		m_textMeshPro_2 = objectHolder2.GetChild(0).GetComponent<TMPro.TMP_Text>();

		ScoreDepot.setupHUD += UpdateDeposit;
        ScoreParticle.OnParticlePickup += ChangeCurrentScore;
		ScoreDepot.depositTick += ChangeCurrentScore;
		ScoreDepot.resetHUD += ResetDepositHUD;
    }

    private void ChangeCurrentScore(float amount)
    {
		currentScore += amount;
		m_textMeshPro_1.text = currentScore.ToString();
    }

	public void UpdateDeposit(float maxScore, float currentScore)
	{
		m_textMeshPro_2.text = currentScore.ToString() + " / " + maxScore.ToString();
	}

	public void ResetDepositHUD()
	{
		m_textMeshPro_2.text = "N/A";
	}
}
