using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Disposal : MovingInStakePanel
{
    [SerializeField] private Button _updateButton; // ������ ����������
    [SerializeField] private GameOver _gameOverPanel; // ������ ��������� ����
    [SerializeField] private CreateObjects _createObjects; // ��������� ��� �������� ��������

    public GameObject[] _stakePoints; // ������ ����� ���������� ��������
    private float _timerToLose = 1f; // ������ ���������
    private float _timerToWin = 1f; // ������ ������
    private int _length; // ����� ������� _stakePoints
    private int _counterToWin; // ������� ����������� ���������� ��� ��������

    private void Start()
    {
        // ������������ ����������� ���������� ���������� ��� ������
        _counterToWin = _createObjects._maxNumberOfObjects / 3;

        // ���������� ����� ������� ����� ���������� ���� �������� � ����� "StakePanelPoint"
        _length = GameObject.FindGameObjectsWithTag("StakePanelPoint").Length;

        // ������� ������ �������� ������, ������ ����� ��������� ��������
        _stakePoints = new GameObject[_length];
    }

    private void FixedUpdate()
    {
        // ��������� ������� ���� Rigidbody � ����������
        if (HitInfo.rigidbody != null)
        {
            // ��������� ��������� ��������� ������ � ������� �������
            _stakePoints[IndexPoint - 1] = HitInfo.transform.gameObject;
        }

        // ��������� ��������� ���������� �������� �������
        if (_stakePoints[_stakePoints.Length - 1] != null)
        {
            // �������� ������ ���������
            _timerToLose -= Time.deltaTime;

            // ���� ������ ����� � ������� ��������� � ���������
            if (_timerToLose < 0)
            {
                _gameOverPanel.YouLose();
            }
        }
        else
        {
            // ���� ��������� ������� ���� � ���������� ������
            _timerToLose = 1f;
        }

        // �������� ���������� ����������� ��������
        CheckForMatches();

        // �������� ������� ������
        YouWin();
    }
    /// <summary>
    /// ����� �������� ���������������� ���������� ����� ����� ��� ������ ������ ���������
    /// </summary>
    private void CheckForMatches()
    {
        for (int i = 0; i < _stakePoints.Length - 2; i++) // �������� �� ������� �� �������������� �������� ��������
        {
            OnOffButton();
            // ��������� ������� ���� ���������������� ����������� ���������
            if (_stakePoints[i] != null && _stakePoints[i + 1] != null && _stakePoints[i + 2] != null)
            {
                // �������� ������������ ����� ������� ��������� ������� ������� �������
                if (_stakePoints[i].transform.GetChild(0).tag == _stakePoints[i + 1].transform.GetChild(0).tag &&
                    _stakePoints[i].transform.GetChild(0).tag == _stakePoints[i + 2].transform.GetChild(0).tag)
                {
                    RemoveAndReset(i); // ������� �������� � ������� �� ���������
                }

                // ���������� ��������� ������� ��������� �������
                if (_stakePoints[i].transform.GetChild(1).tag == _stakePoints[i + 1].transform.GetChild(1).tag &&
                    _stakePoints[i].transform.GetChild(1).tag == _stakePoints[i + 2].transform.GetChild(1).tag)
                {
                    RemoveAndReset(i); // ������� �������� � ������� �� ���������
                }

                // � �������, ��������� �������� ��������� �������
                if (_stakePoints[i].transform.GetChild(2).tag == _stakePoints[i + 1].transform.GetChild(2).tag &&
                    _stakePoints[i].transform.GetChild(2).tag == _stakePoints[i + 2].transform.GetChild(2).tag)
                {
                    RemoveAndReset(i); // ������� �������� � ������� �� ���������
                }
            }
        }
    }
    /// <summary>
    /// ����� ������� ����������� ��������� � ������ ��������
    /// </summary>
    /// <param name="index"></param>
    private void RemoveAndReset(int index)
    {
        // ������� ��� ���������������� �������
        Destroy(_stakePoints[index]);
        Destroy(_stakePoints[index + 1]);
        Destroy(_stakePoints[index + 2]);

        // ������� ������ �������
        _stakePoints[index] = null;
        _stakePoints[index + 1] = null;
        _stakePoints[index + 2] = null;

        // ������������ � ��������� ������� � ��������� ������� �����
        IndexPoint = index;
        _counterToWin--;
    }
    /// <summary>
    /// ����� ����������� ������ ������
    /// </summary>
    private void YouWin()
    {
        // ����� ��� ����������� ���������� �������
        if (_counterToWin == 0)
        {
            // �������� ������ ������� �� �������� �� ��������� �����
            _timerToWin -= Time.deltaTime;

            // ������� �� ������ ����� ��� ��������� �������
            if (_timerToWin < 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    /// <summary>
    /// ��������� / ���������� ������ ����������
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