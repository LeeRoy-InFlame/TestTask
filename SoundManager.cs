using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ��������������� ���� ��� ��������� ������ ����
    [SerializeField] private SoundAssetMenu _soundCollection;

    // ������ ���������� �����
    private int _soundIndex;

    // �������� ��������������� �����������
    private AudioSource _audioSource;

    private void Start()
    {
        // ��������� ���������� ������� �����
        _soundIndex = Random.Range(0, _soundCollection._currentSound.Length);

        // �������� ��������� AudioSource, ������������� � ����� �������
        _audioSource = GetComponent<AudioSource>();
    }

    // �����, ���������� ��� ����� � ������������ ������� �������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������������� ����� ������������ �����
        SoundsOfCollisionBarrel();
    }

    /// <summary>
    /// ������ ��������������� ����� ������������
    /// </summary>
    private void SoundsOfCollisionBarrel()
    {
        if (_audioSource != null) // �������� ������� ���������� AudioSource
        {
            // ���������� ����� ��������� ������ �����
            _soundIndex = Random.Range(0, _soundCollection._currentSound.Length);

            // ������������� ����, ��������� ��������
            _audioSource.PlayOneShot(_soundCollection._currentSound[_soundIndex]);
        }
    }
}
