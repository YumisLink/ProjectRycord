using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapper
{
    public List<MapPoint> mapPoints = new();
    Vector2 initPositoin = new(0, 350);
    public GameObject Map;
    public void InitMap()
    {

        List<int > SpanList = new();
        SpanList.Add(GameManager.Config.MonseterSpanProbility);
        SpanList.Add(GameManager.Config.EliteSpanProbility);
        SpanList.Add(GameManager.Config.ShopSpanProbility);

        mapPoints.Clear();

        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Monster });
        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Monster });
        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Monster });
        for (int i = 1; i <= 35; i++)
            mapPoints.Add(new MapPoint { MapPointType = (MapPointType)WindChimeEngnie.Lib.GetPositionByWeight(SpanList)});
        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Shop });
        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Shop });
        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Shop });
        mapPoints.Add(new MapPoint { MapPointType = MapPointType.Boss });

       

    }
}
