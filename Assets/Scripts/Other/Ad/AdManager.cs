using UnityEngine;
using YG;

public class AdManager : MonoBehaviour
{
    public static AdManager inst;

    // true for success
    public SimpleEvent<bool> RewardAd = new SimpleEvent<bool>();

    private void Awake()
    {
        inst = this;

        YandexGame.ErrorVideoEvent += OnRewardError;
        YandexGame.RewardVideoEvent += OnRewardGranted;
    }

    public void InvokeRewardAd()
    {
        YandexGame.RewVideoShow(0);
    }

    public void InvokeRegularAd()
    {
        YandexGame.FullscreenShow();
    }

    private void OnRewardGranted(int id)
    {
        RewardAd.Invoke(true);
    }

    private void OnRewardError()
    {
        RewardAd.Invoke(false);
    }
}
