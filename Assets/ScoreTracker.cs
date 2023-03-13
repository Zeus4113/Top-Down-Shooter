using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private GameObject m_scoreUI;

    private int currentScore;
    private TMPro.TMP_Text m_textMeshPro;

    private void Start()
    {
        m_textMeshPro = m_scoreUI.GetComponent<TMPro.TMP_Text>();

        ScoreParticle.OnParticlePickup += ChangeScore;
    }

    private void ChangeScore(int amount)
    {
        currentScore += amount;
        m_textMeshPro.text = currentScore.ToString();
    }

    public int GetScore()
    {
        return currentScore;
    }
}
