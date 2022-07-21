using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    /// <summary>
    /// µ±Ç°¹Ø¿¨
    /// </summary>
    public int Level;
    //public 
    //public  GameMapper Mapper = new GameMapper();
    //public void Init()
    //{
    //    Mapper.InitMap();
    //}

}
public struct MapPoint
{
    public MapPointType MapPointType;
}
public enum MapPointType
{
    Shop,Elite,Boss,Monster
}
