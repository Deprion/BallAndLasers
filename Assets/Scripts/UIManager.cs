using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] private GameObject menu;
    [SerializeField] private Button playBtn;
    [SerializeField] private TMP_Text highscore;
    [SerializeField] private ParticleSystem playFX;
    [SerializeField] private Image background, playCircle;

    private readonly Vector3 playScale = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 targetScale;

    private void Awake()
    {
        Events.Lost.AddListener(Lost);

        playBtn.onClick.AddListener(Play);

        highscore.text = YandexGame.savesData.HighScore.ToString();
    }

    public void SetScore(int val)
    {
        highscore.text = val.ToString();
    }

    private void Lost()
    { 
        menu.SetActive(true);
        playFX.Play();

        background.color = Color.black;
        playCircle.color = Color.white;
        highscore.gameObject.SetActive(true);
        playBtn.interactable = true;
    }

    private void Play()
    {
        playBtn.interactable = false;

        StartCoroutine(Cor());
    }

    private float leftTime = 1;
    private Color clrWhite = Color.white;
    private Color clrBlack = Color.black;
    private WaitForSeconds waitFor = new WaitForSeconds(0.03f);

    private IEnumerator Cor()
    {
        highscore.gameObject.SetActive(false);
        playFX.Stop();

        clrWhite = Color.white;
        clrBlack = Color.black;

        leftTime = 1;

        while (leftTime > 0)
        {
            leftTime -= 0.03f;
            clrWhite.a = Mathf.Lerp(1, 0, 1 - leftTime);
            clrBlack.a = Mathf.Lerp(1, 0, 1 - leftTime);

            background.color = clrBlack;
            playCircle.color = clrWhite;

            yield return waitFor;
        }

        menu.SetActive(false);
        game.Play();
    }

    private void Update()
    {
        if (playBtn.transform.localScale.x > 0.99f)
            targetScale = playScale;
        else if (playBtn.transform.localScale.x < 0.82f)
            targetScale = Vector3.one;

        playBtn.transform.localScale = Vector3.Lerp(playBtn.transform.localScale, targetScale, Time.deltaTime);
    }
}
