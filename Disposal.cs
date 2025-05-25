using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Disposal : MovingInStakePanel
{
    [SerializeField] private Button _updateButton; // Кнопка обновления
    [SerializeField] private GameOver _gameOverPanel; // Панель окончания игры
    [SerializeField] private CreateObjects _createObjects; // Компонент для создания объектов

    public GameObject[] _stakePoints; // Массив точек размещения объектов
    private float _timerToLose = 1f; // Таймер проигрыша
    private float _timerToWin = 1f; // Таймер победы
    private int _length; // Длина массива _stakePoints
    private int _counterToWin; // Счётчик необходимых совпадений для выигрыша

    private void Start()
    {
        // Рассчитываем необходимое количество совпадений для победы
        _counterToWin = _createObjects._maxNumberOfObjects / 3;

        // Определяем длину массива путем нахождения всех объектов с тегом "StakePanelPoint"
        _length = GameObject.FindGameObjectsWithTag("StakePanelPoint").Length;

        // Создаем массив объектов длиной, равной длине найденных объектов
        _stakePoints = new GameObject[_length];
    }

    private void FixedUpdate()
    {
        // Проверяем наличие тела Rigidbody в коллайдере
        if (HitInfo.rigidbody != null)
        {
            // Сохраняем последний ударенный объект в массиве позиций
            _stakePoints[IndexPoint - 1] = HitInfo.transform.gameObject;
        }

        // Проверяем состояние последнего элемента массива
        if (_stakePoints[_stakePoints.Length - 1] != null)
        {
            // Отнимаем таймер проигрыша
            _timerToLose -= Time.deltaTime;

            // Если таймер истек — выводим сообщение о поражении
            if (_timerToLose < 0)
            {
                _gameOverPanel.YouLose();
            }
        }
        else
        {
            // Если последний элемент пуст — сбрасываем таймер
            _timerToLose = 1f;
        }

        // Проверка комбинаций совпадающих объектов
        CheckForMatches();

        // Проверка условия победы
        YouWin();
    }
    /// <summary>
    /// Метод проверки последовательных совпадений тегов среди трёх подряд идущих элементов
    /// </summary>
    private void CheckForMatches()
    {
        for (int i = 0; i < _stakePoints.Length - 2; i++) // Проходим по массиву до предпоследнего третьего элемента
        {
            OnOffButton();
            // Проверяем наличие трех последовательных заполненных элементов
            if (_stakePoints[i] != null && _stakePoints[i + 1] != null && _stakePoints[i + 2] != null)
            {
                // Проверка соответствия тегов первого дочернего объекта каждого объекта
                if (_stakePoints[i].transform.GetChild(0).tag == _stakePoints[i + 1].transform.GetChild(0).tag &&
                    _stakePoints[i].transform.GetChild(0).tag == _stakePoints[i + 2].transform.GetChild(0).tag)
                {
                    RemoveAndReset(i); // Удаляем элементы и очищаем их положение
                }

                // Аналогично проверяем второго дочернего объекта
                if (_stakePoints[i].transform.GetChild(1).tag == _stakePoints[i + 1].transform.GetChild(1).tag &&
                    _stakePoints[i].transform.GetChild(1).tag == _stakePoints[i + 2].transform.GetChild(1).tag)
                {
                    RemoveAndReset(i); // Удаляем элементы и очищаем их положение
                }

                // И наконец, проверяем третьего дочернего объекта
                if (_stakePoints[i].transform.GetChild(2).tag == _stakePoints[i + 1].transform.GetChild(2).tag &&
                    _stakePoints[i].transform.GetChild(2).tag == _stakePoints[i + 2].transform.GetChild(2).tag)
                {
                    RemoveAndReset(i); // Удаляем элементы и очищаем их положение
                }
            }
        }
    }
    /// <summary>
    /// Метод очистки совпадающих элементов и сброса индексов
    /// </summary>
    /// <param name="index"></param>
    private void RemoveAndReset(int index)
    {
        // Удаляем три последовательных объекта
        Destroy(_stakePoints[index]);
        Destroy(_stakePoints[index + 1]);
        Destroy(_stakePoints[index + 2]);

        // Очищаем ячейки массива
        _stakePoints[index] = null;
        _stakePoints[index + 1] = null;
        _stakePoints[index + 2] = null;

        // Возвращаемся к начальной позиции и уменьшаем счётчик побед
        IndexPoint = index;
        _counterToWin--;
    }
    /// <summary>
    /// Метод определения победы игрока
    /// </summary>
    private void YouWin()
    {
        // Когда все необходимые комбинации найдены
        if (_counterToWin == 0)
        {
            // Начинаем отсчет времени до перехода на следующую сцену
            _timerToWin -= Time.deltaTime;

            // Переход на вторую сцену при истечении таймера
            if (_timerToWin < 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    /// <summary>
    /// Включение / выключение кнопки обновления
    /// </summary>
    private void OnOffButton()
    {
        if (_stakePoints[0] != null)
        {
            _updateButton.interactable = false;
        }
        else
        {
            _updateButton.interactable = true;
        }
    }
}