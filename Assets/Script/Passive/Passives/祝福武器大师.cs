using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 祝福武器大师 : Passive
{
    public int stack;
    public Queue<float> deadtimeline = new();
    public override void SwitchWeapon(BulletData btd)
    {
        if (stack < 3)
        {
            stack++;
            deadtimeline.Enqueue(Time.time + 6);
        }
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (deadtimeline.Count != 0)
            while (deadtimeline.Peek() < Time.time)
            {
                deadtimeline.Dequeue();
                stack--;
                if (deadtimeline.Count == 0)
                    break;
            }
        damage.FinalDamage += damage.damage * stack * 0.33f;
    }
}