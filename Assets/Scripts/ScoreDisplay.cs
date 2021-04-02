using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] GameSession gameSession;
    int displayGold;
    [SerializeField] bool highScoreDisplay = false;
    // Start is called before the first frame update
    void Start()
    {
        FindGameSession();
    }

    private void FindGameSession()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameSession == null) FindGameSession();
        if (highScoreDisplay) displayGold = gameSession.GetHighScore();
        else displayGold = gameSession.GetGold();
        GetComponent<TextMeshProUGUI>().text = displayGold.ToString();
    }
}
