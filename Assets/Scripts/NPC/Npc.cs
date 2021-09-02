
using DG.Tweening;
using UnityEngine;

public interface IDo
{
    void Do();
} 

public class Do : IDo
{
    void IDo.Do() => Debug.Log("Do");
}

public class NotDo : IDo
{
    public void Do() { }
}

public class Npc : Character
{
    private enum EState { None, Idle, Move, Battle, BattleDotween }

    //public IDo Do = new NotDo();
    private EState _state = EState.Idle;
    private Character _target;
    public float AttackDistance = 5;
    public float Speed = 5;
    public float BulletSpeed = 15;

    public Transform BulletPrefab;
    private Pool<Transform> BulletPool;
    public float AttackInterval = 1;
    private float _reloading;

    private void Awake()
    {
        BulletPool = new Pool<Transform>(BulletPrefab);
    }

    protected override void Update()
    {
        //Do.Do();
        base.Update();
        switch (_state)
        {
            case EState.Idle:
                _target = AllCharacters.Find(x => !(x is Npc));
                if (_target && IsGrounded) _state = EState.Move;
                //Do = new NotDo();
                break;
            case EState.Move:
                if (_target)
                {
                    var vector = _target.transform.position - transform.position;
                    if (vector.magnitude > AttackDistance)
                    {
                        rigidBody2D.AddForce(vector.normalized * Speed);
                    }
                    else _state = EState.BattleDotween;
                }
                else _state = EState.Idle;
                //Do = new Do();
                break;
            case EState.Battle:
                if (_target)
                {
                    if (Time.time < _reloading) return;
                    _reloading = Time.time + AttackInterval;
                    var vector = _target.transform.position - transform.position;
                    var p = BulletPool.GetFromPool();
                    p.SetPositionAndRotation(transform.position, Quaternion.identity);
                    p.GetComponent<Rigidbody2D>().AddForce(vector.normalized * BulletSpeed);
                }
                else _state = EState.Idle;
                break;
            case EState.BattleDotween:
                if (_target)
                {
                    if (Time.time < _reloading) return;
                    _reloading = Time.time + AttackInterval;
                    var p = BulletPool.GetFromPool();
                    var vector = _target.transform.position - transform.position;
                    p.SetPositionAndRotation(transform.position, Quaternion.identity);
                    p.transform.DOMove(_target.transform.position,
                        vector.magnitude / BulletSpeed * 2);
                    Destroy(p.gameObject, 2);
                }
                else _state = EState.Idle;
                break;
            default:
                Debug.LogError($"{name} in state {_state}");
                break;
        }
    }
}
