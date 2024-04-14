using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UIManager ui;

    private int score = 0;

    private void Awake()
    {
        Events.BouncyWallHit.AddListener(BouncyWallHit);
        Events.Lost.AddListener(Lost);
    }

    private void BouncyWallHit()
    {
        score++;
    }

    public void Play()
    {
        Events.Restart.Invoke();

        player.SetActive(true);
    }

    private void Lost()
    {
        if (YandexGame.savesData.HighScore < score)
        {
            YandexGame.savesData.HighScore = score;
            ui.SetScore(score);
            YandexGame.NewLeaderboardScores("main", score);
        }

        score = 0;

        player.SetActive(false);

        player.transform.localPosition = new Vector3(0, 10, 0);
    }
}
