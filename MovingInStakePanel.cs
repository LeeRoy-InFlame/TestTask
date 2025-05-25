using UnityEngine;

public class MovingInStakePanel : MonoBehaviour
{
    // ���� ��� ������������ ������� ����� ���������� �������
    [SerializeField] private GameObject[] _stakePanelPoints;

    // ����������� ��������� �������� ��� �������� �������� ������� ������
    public static int IndexPoint = 0;

    // ���������� ��� �������� ���� (��� ��������)
    private Ray _ray;

    // ����������� ���������� ��� �������� ���������� ����������� ���� � ��������
    public static RaycastHit2D HitInfo;

    /// <summary>
    /// ��������� ����� ����������� ������� � ������
    /// </summary>
    public void InStakePanel()
    {
        // ���������, �� �������� �� �� ����� ������� ����� ����������
        if (IndexPoint != _stakePanelPoints.Length)
        {
            // ������� ��� �� ������, ���������� ����� ����� ������� ������
            _ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            // ��������� ��� ������������ � ��������� ���������
            HitInfo = Physics2D.Raycast(_ray.origin, _ray.direction);

            // ���� ��������� �����-�� ���������� ������
            if (HitInfo.collider != null)
            {
                // ���������, �������� �� ���� ������ ������ ("Barrel")
                if (HitInfo.transform.gameObject.CompareTag("Barrel"))
                {
                    // ��������� ����� � ������� ����� ������
                    HitInfo.transform.position = _stakePanelPoints[IndexPoint].transform.position;

                    // ���������� �������� �������
                    HitInfo.transform.rotation = Quaternion.identity;

                    // �������� ��� ������� �� "StakeBarrel"
                    HitInfo.transform.gameObject.tag = "StakeBarrel";

                    // ���� ����� ���� ������ ����� ������ ��������
                    if (_stakePanelPoints[IndexPoint] != null)
                    {
                        // ��������� � ��������� ����� ������
                        IndexPoint++;
                    }
                }
            }
        }
    }
}
