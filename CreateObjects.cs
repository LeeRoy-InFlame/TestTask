using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private Transform _canvasPosition; // Позиция холста (канваса), относительно которой будут создаваться объекты
    [SerializeField] private Transform _spawnPoint; // Точка спавна новых объектов
    [SerializeField] private GameObject[] _arrayOfPrefabs; // Массив префабов, из которого случайным образом выбираются объекты для создания
    [SerializeField] private int _cycles; // Количество циклов создания объектов

    public int _maxNumberOfObjects; // Максимальное количество объектов, которое может быть создано

    private int _allowedNumberOfObjects; // Допустимое количество каждого типа объекта
    private int _randomIndexObject; // Индекс случайного объекта из массива префабов

    private bool _isSpawning; // Флаг, определяющий возможность создания новых объектов

    private GameObject _newObject; // Созданный объект

    private void Start()
    {
        // Вычисляем количество циклов исходя из максимального количества объектов и длины массива префабов
        _cycles = _maxNumberOfObjects / _arrayOfPrefabs.Length;
        _allowedNumberOfObjects = _cycles; // Устанавливаем допустимое количество каждого объекта равным количеству циклов
        _randomIndexObject = Random.Range(0, _arrayOfPrefabs.Length); // Случайная выборка индекса объекта из массива
        _isSpawning = true; // Включаем режим создания объектов
    }

    private void FixedUpdate()
    {
        Create(); // Запуск метода создания объектов
    }

    /// <summary>
    /// Метод, создающий новые объекты
    /// </summary>
    private void Create()
    {
        if (_isSpawning == true) // Проверяем условие возможности создания объектов
        {
            // Обновляем позицию точки спавна на основе позиции канваса
            _spawnPoint.position = new Vector2(_canvasPosition.position.x, transform.position.y);

            if (_allowedNumberOfObjects > 0) // Если ещё можно создавать объекты текущего типа
            {
                // Инстанцируем новый объект в точке спавна
                _newObject = Instantiate(_arrayOfPrefabs[_randomIndexObject], _spawnPoint.position, Quaternion.identity);

                // Присваиваем новому объекту родителя текущего компонента (этого скрипта)
                _newObject.transform.SetParent(transform, false);

                // Уменьшаем счётчик оставшихся возможных объектов текущего типа
                _allowedNumberOfObjects--;

                // Уменьшаем общее максимальное число объектов
                _maxNumberOfObjects--;
            }
            else if (_allowedNumberOfObjects == 0) // Если лимит достигнут
            {
                // Меняем индекс случайно выбранного объекта
                _randomIndexObject = Random.Range(0, _arrayOfPrefabs.Length);

                // Восстанавливаем допустимое количество объектов
                _allowedNumberOfObjects = _cycles;
            }

            if (_maxNumberOfObjects == 0) // Если достигнуто максимальное число созданных объектов
            {
                _isSpawning = false; // Остановка процесса создания объектов
            }
        }
    }

    // Метод, вызываемый при нажатии кнопки обновления
    public void OnUpdateButtonDown()
    {
        // Получаем массив всех игровых объектов с тегом "Barrel"
        GameObject[] newMaxNumberObjects = GameObject.FindGameObjectsWithTag("Barrel");

        // Обновляем максимальное количество объектов согласно найденному числу баррелей
        _maxNumberOfObjects = newMaxNumberObjects.Length;

        // Удаляем все существующие объекты с тегом "Barrel"
        for (int i = 0; i < newMaxNumberObjects.Length; i++)
        {
            Destroy(newMaxNumberObjects[i]); // Удаление объекта
        }

        // Обновляем индекс случайного объекта
        _randomIndexObject = Random.Range(0, _arrayOfPrefabs.Length);

        // Восстанавливаем количество разрешённых объектов
        _allowedNumberOfObjects = _cycles;


        if (newMaxNumberObjects.Length % 3 != 0)
        {
            _isSpawning = false;
        }
        else
        {
            // Включаем создание новых объектов
            _isSpawning = true;
        }
    }
}
