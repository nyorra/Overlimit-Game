namespace OVERLIMIT.Logging
{
    // Единый центр сообщений логирования
    public static class LogMessages
    {
        public static class System
        {
            public static string Initialized(string name) => $"Система [{name}] успешно запущена.";
            public static string MissingComponent(string owner, string comp) => $"[{owner}]: Не назначен компонент {comp}!";
            public static string InputReceived = "Ввод игрока получен, активация...";
        }

        public static class Scene
        {
            public static string LoadStarted(string name) => $"Начата загрузка сцены: {name}";
            public static string LoadComplete(string name) => $"Сцена {name} загружена в память.";
            public static string Activation(string name) => $"Переход в сцену: {name}";
            public static string NotFound(string name) => $"Критическая ошибка: Сцена {name} не найдена в Build Settings!";
        }

        public static class Garage
        {
            public static string Switched(string carName) => $"Машина изменена на: {carName}";
            public static string Empty = "Список машин пуст или не инициализирован!";
            public static string SelectionReset = "SelectedCar был null, принудительная установка первого элемента.";
        }

        public static class UI
        {
            public static string PanelOpened(string name) => $"Открыта панель: {name}";
            public static string NullPanel = "Попытка открыть пустую (null) панель!";
            public static string UIUpdated(string target) => $"Интерфейс [{target}] успешно обновлен.";
        }
    }
}
