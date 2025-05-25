using UnityEngine;

public class MovingInStakePanel : MonoBehaviour
{
    // Поле для сериализации массива точек размещения панелей
    [SerializeField] private GameObject[] _stakePanelPoints;

    // Статическое публичное свойство для хранения текущего индекса панели
    public static int IndexPoint = 0;

    // Переменная для хранения луча (луч кастинга)
    private Ray _ray;

    // Статическая переменная для хранения результата пересечения луча с объектом
    public static RaycastHit2D HitInfo;

    /// <summary>
    /// Публичный метод перемещения объекта в панель
    /// </summary>
    public void InStakePanel()
    {
        // Проверяем, не достигли ли мы конца массива точек размещения
        if (IndexPoint != _stakePanelPoints.Length)
        {
            // Создаем луч из камеры, проходящий через точку касания экрана
            _ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            // Выполняем луч кастирование и сохраняем результат
            HitInfo = Physics2D.Raycast(_ray.origin, _ray.direction);

            // Если пересекли какой-то физический объект
            if (HitInfo.collider != null)
            {
                // Проверяем, является ли этот объект бочкой ("Barrel")
                if (HitInfo.transform.gameObject.CompareTag("Barrel"))
                {
                    // Переносим бочку в текущую точку панели
                    HitInfo.transform.position = _stakePanelPoints[IndexPoint].transform.position;

                    // Сбрасываем вращение объекта
                    HitInfo.transform.rotation = Quaternion.identity;

                    // Изменяем тег объекта на "StakeBarrel"
                    HitInfo.transform.gameObject.tag = "StakeBarrel";

                    // Если точка была занята ранее другим объектом
                    if (_stakePanelPoints[IndexPoint] != null)
                    {
                        // Переходим к следующей точке панели
                        IndexPoint++;
                    }
                }
            }
        }
    }
}
