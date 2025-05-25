using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Сериализованное поле для коллекции звуков меню
    [SerializeField] private SoundAssetMenu _soundCollection;

    // Индекс случайного звука
    private int _soundIndex;

    // Источник воспроизведения аудиофайлов
    private AudioSource _audioSource;

    private void Start()
    {
        // Генерация случайного индекса звука
        _soundIndex = Random.Range(0, _soundCollection._currentSound.Length);

        // Получаем компонент AudioSource, прикрепленный к этому объекту
        _audioSource = GetComponent<AudioSource>();
    }

    // Метод, вызываемый при входе в столкновение другого объекта
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Воспроизведение звука столкновения бочки
        SoundsOfCollisionBarrel();
    }

    /// <summary>
    /// Метода воспроизведения звука столкновения
    /// </summary>
    private void SoundsOfCollisionBarrel()
    {
        if (_audioSource != null) // Проверка наличия компонента AudioSource
        {
            // Генерируем новый случайный индекс звука
            _soundIndex = Random.Range(0, _soundCollection._currentSound.Length);

            // Воспроизводим звук, указанный индексом
            _audioSource.PlayOneShot(_soundCollection._currentSound[_soundIndex]);
        }
    }
}
