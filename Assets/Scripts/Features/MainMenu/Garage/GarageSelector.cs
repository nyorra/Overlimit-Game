using System;
using System.Collections.Generic;
using UnityEngine;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Garage
{
    public class GarageSelector : MonoBehaviour
    {
        public List<CarData> allCars;
        private int _currentIndex = 0;

        public static event Action<CarData> OnCarChanged;
        public static CarData SelectedCar { get; private set; }

        public void SwitchCar(int direction)
        {
            if (allCars == null || allCars.Count == 0)
            {
                // Используем готовое сообщение из LogMessages
                OverLogger.LogError(LogMessages.Garage.Empty, this);
                return;
            }

            _currentIndex = (_currentIndex + direction + allCars.Count) % allCars.Count;
            SelectedCar = allCars[_currentIndex];

            // Динамическое сообщение с именем машины
            OverLogger.LogSuccess(LogMessages.Garage.Switched(SelectedCar.CarName), this);

            OnCarChanged?.Invoke(SelectedCar);
        }

        public void Init()
        {
            if (allCars != null && allCars.Count > 0)
            {
                SelectedCar = allCars[0];
                OnCarChanged?.Invoke(SelectedCar);

                // Логируем успешную инициализацию
                OverLogger.LogSuccess(LogMessages.System.Initialized(nameof(GarageSelector)), this);
            }
            else
            {
                OverLogger.LogWarning(LogMessages.Garage.Empty, this);
            }
        }
    }
}
