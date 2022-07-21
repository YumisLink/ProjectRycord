using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public bool IsWave = false;
    public bool IsStop = true;
    public List<Role> AllMonster = new();
    public Wave noWave;
    public float restTime = 5;
    public float NextSpawnTime = 0;
    public int MonsterLimit = 0;
    float LoopTig = 0;
    public GameObject shoper;
    public GameObject box;
    Text txt;
    private void Start()
    {
        txt = UIManager.GetText("ttt");
        var wb = new Wave()
        {
            LimitTime = 120,
            MaxLimit = 15,
        };
        wb.AddSlime(50);
        waves.Add(wb);

        wb = new Wave()
        {
            LimitTime = 120,
            MaxLimit = 20,
        };
        wb.AddSlime(100);
        wb.Add(GameManager.Roles["Float"], 15);
        waves.Add(wb);

        wb = new Wave()
        {
            LimitTime = 120,
            MaxLimit = 25,
        };
        wb.AddSlime(150);
        wb.Add(GameManager.Roles["Shooter"], 35);
        wb.Add(GameManager.Roles["Float"], 20);
        wb.Add(GameManager.Roles["Cat"], 1);
        wb.Add(GameManager.Roles["sojo"], 1);
        waves.Add(wb);
    }
    private void StartWave(Wave w)
    {
        IsWave = true;
        noWave = w;
        foreach (var a in dels)
            Destroy(a);
    }
    public GameObject mijuan;
    /// <summary>
    /// ≈–∂œ «∑Ò…±À¿¡À–°π÷°£
    /// </summary>
    private void DotFive()
    {
        for (int i = 0; i < AllMonster.Count; i++)
        {
            if (AllMonster[i] == null)
            {
                AllMonster.RemoveAt(i);
                i--;
            }
        }

        if (noWave.b.Count <= 0 && AllMonster.Count <= 0 && IsWave)
        {
            EndWave();
        }
    }
    List<GameObject> dels = new();
    private void EndWave()
    {
        txt.gameObject.SetActive(true);
        IsWave = false;
        IsStop = true;
        var a =Instantiate(mijuan);
        dels.Add(a);
        WindChimeEngnie.Lib.MoveTo(a, new Vector3(-1.07f, 3.3f));
        if (UnityEngine.Random.Range(0, 1f) < 1f)
        {
            a = Instantiate(shoper);
            dels.Add(a);
            WindChimeEngnie.Lib.MoveTo(a, new Vector3(0, 6));
        }
        if (UnityEngine.Random.Range(0, 1f) < 1f)
        {
            a = Instantiate(box);
            dels.Add(a);
            WindChimeEngnie.Lib.MoveTo(a, new Vector3(-9,0));
        }
        if (UnityEngine.Random.Range(0, 1f) < 0.5f)
        {
            a = Instantiate(box);
            dels.Add(a);
            WindChimeEngnie.Lib.MoveTo(a, new Vector3(9, 0));
        }
    }
    IEnumerator dd()
    {
        GameManager.MainBase.FireHpR = 10;
        GameManager.MainBase.IceHpR = 10;
        GameManager.MainBase.ThunderHpR = 10;
        GameManager.MainBase.WaterHpR = 10;
        Debug.Log("d");
        isd = true;
        UIManager.instance.virtualCamera.Follow = this.transform;
        var k = Effect.Create(GameManager.Effects["suc"], transform.position);
        k.transform.localScale = new Vector3(5, 5);
        yield return new WaitForSeconds(3);
        UIManager.instance.CloseWindow();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Succes");
    }
    bool isd = false;
    public void Success()
    {
        if (!isd)
        StartCoroutine("dd");
    }
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.H))
        {
            txt.gameObject.SetActive(false);
            IsStop = false;
        }
        if (!IsWave && waves.Count <= 0 )
            Success();
        if (IsStop) return;
        restTime -= Time.deltaTime;
        if (!IsWave && restTime < 0)
        {
            if (waves.Count > 0)
            {
                StartWave(waves[0]);
                waves.RemoveAt(0);
            }
        }
        LoopTig += Time.deltaTime;
        if (LoopTig > 0.5f)
        {
            DotFive();
            LoopTig = 0;
        }
        if (!IsWave)
            return;
        if (NextSpawnTime < Time.time)
        {
            if (AllMonster.Count < noWave.MaxLimit && noWave.b.Count > 0)
            {
                int k = UnityEngine.Random.Range(0, noWave.b.Count);
                Spawn(noWave.b[k]);
                noWave.b.RemoveAt(k);
                NextSpawnTime = Time.time + UnityEngine.Random.Range(0, 0.2f);
            }
        }
    }
    private void Spawn(GameObject go)
    {
        var k = UnityEngine.Random.Range(0, GameManager.MonsterSpawn.Count);
        var ms =NormalMosterAI.Create(go, GameManager.MonsterSpawn[k]);
        AllMonster.Add(ms);
    }
    
}
[Serializable]
public class Wave
{
    public float LimitTime = 120;
    public float MaxLimit = 5;
    public List<GameObject> b = new();
    public void Add(GameObject go,int cnt)
    {
        for (int i = 0; i < cnt; i++)
            b.Add(go);
    }
    public void AddSlime(int cnt)
    {
        for (int i =0; i < cnt; i++)
        {
            var t = UnityEngine.Random.Range(0, 4);
            if (t == 0)
                Add(GameManager.Roles["WaterSlime"], 1);
            if (t == 1)
                Add(GameManager.Roles["FireSlime"], 1);
            if (t == 2)
                Add(GameManager.Roles["ThunderSlime"], 1);
            if (t == 3)
                Add(GameManager.Roles["IceSlime"], 1);
        }
    }
}
