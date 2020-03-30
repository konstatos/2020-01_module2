using UnityEngine;


public sealed class PlatformBehaviour : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField] private LevelPartSettings _levelPartSettings;
    [SerializeField] private Transform _pointSpawnAdditionalObject;
    #pragma warning restore CS0649
    private bool _isGenerate;

    public float GetBorder()
    {
        Transform child = transform.GetChild(transform.childCount - 1);
        float offsetX = child.GetComponent<Renderer>().bounds.extents.x;
        return child.localPosition.x + offsetX;
    }

    public Vector3 GetPointSpawnAdditionalObject()
    {
        return _pointSpawnAdditionalObject.position;
    }

    public void GenerateNewPart()
    {
        _isGenerate = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isGenerate && other.CompareTag(TagManager.GetTag(TagType.Player)))
        {
            _levelPartSettings.Generate();
            _isGenerate = false;
        }
    }
}
