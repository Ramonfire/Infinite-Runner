using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currentScore=0;
   public int CurrentScore { get { return currentScore; } }
    [SerializeField]TMP_Text Score;
    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {
        if(Score!=null)
            Score.SetText("Score:"+currentScore);
    }
    public void AddScore(int inScore)
    { 
        currentScore += inScore; 
    }

}
