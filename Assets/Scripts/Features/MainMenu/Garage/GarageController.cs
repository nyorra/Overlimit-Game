using UnityEngine;
using UnityEngine.UI;

namespace OVERLIMIT.Garage
{
    public class GarageController : MonoBehaviour
    {
        public GarageSelector selector;
        public Button nextButton;
        public Button prevButton;

        void Start()
        {
            nextButton.onClick.AddListener(() => selector.SwitchCar(1));
            prevButton.onClick.AddListener(() => selector.SwitchCar(-1));

            // Запускаем начальную установку
            selector.Init();
        }
    }
}
