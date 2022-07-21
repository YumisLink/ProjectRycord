using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public string Realeasing;
    private Vector2 target;
    private Vector2 NearMoveTarget;
    [InspectorShow("是否正在移动")]
    public bool IsMoving = false;
    public Vector2 Position;
    private float MoveSpeed => role.Property.MoveSpeed * 1f;
    Role role;
    public Rigidbody2D rigidbodys;
    public Vector2 nextMove;
    public bool change = false;
    void Start()
    {
        if (TryGetComponent<PlayerShoot>(out _))
            change = true;
        Position = transform.position;
        target = transform.position;
        role = GetComponent<Role>();
        if (!gameObject.TryGetComponent<Rigidbody2D>(out _))
        {
            rigidbodys = gameObject.AddComponent<Rigidbody2D>();
            rigidbodys.freezeRotation = true;
        }
        else
        {
            rigidbodys = gameObject.GetComponent<Rigidbody2D>();
        }
    }
    public void Move(Vector2 v2)
    {
        if(v2.x != float.NaN)
            nextMove.x += v2.x;
        if (v2.y != float.NaN)
            nextMove.y += v2.y;
    }
    public Vector2 hitbackPower = new Vector2();
    void FixedUpdate()
    {
        if (role.State != Role.NormalState)
            IsMoving = false;
        rigidbodys.velocity = Vector2.zero;
        hitbackPower -= 1 * hitbackPower * Time.fixedDeltaTime;
        if (IsMoving)
        {
            if (WindChimeEngnie.Lib.GetDistance(transform.position, NearMoveTarget) <= Time.deltaTime * MoveSpeed)
            {
                IsMoving = false;
                role.Animator.SetBool("Move", false);
            }
            else
            {
                var angle = WindChimeEngnie.Lib.GetAngle(transform.position, NearMoveTarget);
                var x = Mathf.Cos(Mathf.Deg2Rad * angle);
                var y = Mathf.Sin(Mathf.Deg2Rad * angle);
                Move(new Vector2(x * MoveSpeed, y * MoveSpeed));
                if (NearMoveTarget.x > transform.position.x && !change)
                    role.SetFaceToRight();
                if (NearMoveTarget.x < transform.position.x && !change)
                    role.SetFaceToLeft();
                Position = rigidbodys.velocity;
            }
        }
        rigidbodys.mass = 1;
        var v = rigidbodys.velocity;
        v.x += nextMove.x + hitbackPower.x * Time.fixedDeltaTime;
        v.y += nextMove.y + hitbackPower.y * Time.fixedDeltaTime;
        nextMove = new Vector2(0.000001f,0.000001f);
        if (role.CanAction > 0)
            return;
        if (v.x != float.NaN || v.y != float.NaN)
            rigidbodys.velocity = v;
    }
    public void NearGo(Vector2 v2)
    {
        NearMoveTarget = v2;
    }
    public void MoveToTarget(Vector2 Target)
    {
        role.SetAttackTarget(null);
        target = Target;
        IsMoving = true;
        role.Animator.SetBool("Move", true);
        Realeasing = OnlyMove;
        NearMoveTarget = Target;
    }
    public void MoveAttack(Vector2 Target)
    {
        role.SetAttackTarget(null);
        target = Target;
        IsMoving = true;
        if (role.Animator)
            role.Animator.SetBool("Move", true);
        Realeasing = MoveAndAttack;
        NearMoveTarget = Target;
        var e = role.GetNearestEnemy();
        if (e != null)
        {
            var dis = WindChimeEngnie.Lib.GetDistance(e.gameObject, gameObject);
            if (dis <= role.AlertRange)
            {
                role.SetAttackTarget(e);
                IsMoving = false;
                if (role.Animator)
                    role.Animator.SetBool("Move", false);
            }
        }
    }
    public void MoveDirection(Vector2 direction)
    {
        target = direction;
        IsMoving = true;
        role.Animator.SetBool("Move", true);
        if (direction.magnitude == 0)
        {
            role.Animator.SetBool("Move", false);
            IsMoving = false;
        }
        NearMoveTarget = transform.position + new Vector3(direction.x * 5, direction.y * 5);
    }
    public void Attack(GameObject Target)
    {

    }
    public static string MoveAndAttack = "MoveAttack";
    public static string OnlyMove = "OnlyMove";
    public static string RealeaseSkill = "RealeaseSkill";
    public static string OnlyAttack = "OnlyAttack";
}
