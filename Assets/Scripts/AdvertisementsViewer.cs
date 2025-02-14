using UnityEngine;
using YG;

namespace Scripts
{
    public class AdvertisementsViewer : MonoBehaviour
    {
        private static int _gameCycle;

        private int _frequencyAdShow = 3;

        public void ShowFullSreenAd()
        {
            _gameCycle++;

            if (_gameCycle % _frequencyAdShow == 0)
                YandexGame.FullscreenShow();
        }
    }
}