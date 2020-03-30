using UnityEngine;


public sealed class GameController : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private Transform _pointSpawnCharacter;
    [SerializeField] private LevelPartSettings _levelPartSettings;
    #pragma warning restore CS0649

    private void Start()
    {
        _levelPartSettings.Generate();

        Instantiate(_characterPrefab, _pointSpawnCharacter.position, Quaternion.identity);
    }
}
