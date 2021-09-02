using UnityEngine;

public enum ENpc { None, Npc, }

public class Spawner2 : MonoBehaviour
{
    private Character Prefab;
    public float SpawnInterval = 5;
    private float _time;
    private Pool<Character> Pool;

    private void Start()
    {
        Prefab = ENpc.Npc.GetPrefab();
        Pool = new Pool<Character>(Prefab, null);
        // Resources.Load<GameObject>("Prefabs/" + ENpc.Npc);
    }

    private void Update()
    {
        if (_time > Time.time) return;
        _time = Time.time + SpawnInterval;
        var p = Pool.GetFromPool();
        p.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        //Instantiate(Prefab, transform.position, Quaternion.identity, null);
    }
}

public static class PrefabExt
{
    public static Character GetPrefab(this ENpc npc) =>
        Resources.Load<Character>("Prefabs/" + npc);
}
