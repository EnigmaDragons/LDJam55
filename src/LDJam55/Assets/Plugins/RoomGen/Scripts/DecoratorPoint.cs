using UnityEngine;

namespace RoomGen
{

    public enum PointType
    {
        Default,
        Wall,
        Floor,
        Door,
        Window,
        Character,
        Roof
    }


    public class DecoratorPoint
    {


        public GameObject tileObject;
        public GameObject currentDecoration;
        public Vector3 point;
        public PointType pointType;
        public bool occupied;
        public int levelNumber;

        public DecoratorPoint(GameObject tileObject_, GameObject currentDecoration_, Vector3 point_, PointType pointType_, bool occupied_, int levelNumber_)
        {
            tileObject = tileObject_;
            currentDecoration = currentDecoration_;
            point = point_;
            pointType = pointType_;
            occupied = occupied_;
            levelNumber = levelNumber_;
        }


    }

}