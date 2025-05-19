using UnityEngine;
using YG;

namespace Scripts
{
    public class AdvertisementsViewer : MonoBehaviour
    {
        private static int s_gameCycle;

        private int _frequencyAdShow = 3;

        public void ShowFullSreenAd()
        {
            s_gameCycle++;

            if (s_gameCycle % _frequencyAdShow == 0)
                YandexGame.FullscreenShow();
        }
    }
}