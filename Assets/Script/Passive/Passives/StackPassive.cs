using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPassive : Passive
{
    public int Cnt = 1;
    protected override void OnStart()
    {
        base.OnStart();
        if (role == GameManager.CenterRole)
        {
            Debug.Log(this.GetType().Name);
            var obj = UIManager.instance.ClickB;
            obj.GetComponent<MiJuanShower>().AddMijuan(this.GetType().Name);
        }
    }
    public override void OnConflictPassive(Passive passive)
    {
        base.OnConflictPassive(passive);
        if (passive.GetType() == this.GetType() && passive is StackPassive)
        {
            var pas = passive as StackPassive;
            pas.Cnt += Cnt;
            Discard();
        }
        return;
    }
    
}
