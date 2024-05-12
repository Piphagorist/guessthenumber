using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Architecture.UI;
using GuessTheNumber.Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace GuessTheNumber.Scripts.Architecture.Loading.UI
{
    public class LoadingScreen : UIWindow
    {
        [SerializeField] private TMP_Text progressTxt;

        public void SetProgress(float progress)
        {
            progressTxt.text = progress.GetPercents();
        }
    }
}