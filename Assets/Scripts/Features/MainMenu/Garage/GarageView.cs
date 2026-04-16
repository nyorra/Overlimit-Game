using UnityEngine;
using TMPro;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Garage
{
    public class GarageView : MonoBehaviour
    {
        public TMP_Text SelectedCarText;

        private void OnEnable()
        {
            // Подписываемся на событие при включении объекта
            GarageSelector.OnCarChanged += UpdateUI;
        }

        private void OnDisable()
        {
            // Отписываемся, чтобы избежать утечек памяти
            GarageSelector.OnCarChanged -= UpdateUI;
        }

        public void UpdateUI(CarData car)
        {
            if (car == null) return;

            SelectedCarText.text = car.CarName;
            OverLogger.LogSuccess($"Интерфейс обновлен для: {car.CarName}", this);
        }
    }
}
