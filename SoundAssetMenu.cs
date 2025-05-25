using UnityEngine;

[CreateAssetMenu(fileName = "CollectionOfSounds", menuName = "CollectionOfSounds", order = 51)]
public class SoundAssetMenu : ScriptableObject
{
    [SerializeField]
    public AudioClip[] _currentSound;
}
