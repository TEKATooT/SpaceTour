namespace GameControl
{
    using UnityEngine;
    using YG;

    public class AdvertisementsViewer : MonoBehaviour
    {
        private static long s_timeShowAd;
        private static bool s_isFirsStart = true;

        private long _adDelay = 61000;

        private void Start()
        {
            TryShowFullSreenAd();
        }

        public void TryShowFullSreenAd()
        {
            if (s_timeShowAd < YandexGame.ServerTime() && !s_isFirsStart)
            {
                YandexGame.FullscreenShow();

                s_timeShowAd = YandexGame.ServerTime() + _adDelay;
            }

            if (s_isFirsStart)
                s_isFirsStart = false;
        }
    }
}