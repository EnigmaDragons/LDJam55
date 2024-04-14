using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen
{
    public static class Tools
    {
        // Retrieve random objects
        public static Door GetWeightedDoor(List<Door> doors, int totalWeight)
        {
            int randomWeight = Random.Range(0, totalWeight);
            foreach (Door tile in doors)
            {
                if (randomWeight <= tile.weight)
                    return tile;
                randomWeight -= tile.weight;
            }

            return null;
        }

        public static Floor GetWeightedFloor(List<Floor> floors, int totalWeight)
        {
            int randomWeight = Random.Range(0, totalWeight);
            foreach (Floor tile in floors)
            {
                if (randomWeight <= tile.weight)
                    return tile;
                randomWeight -= tile.weight;
            }

            return null;
        }

        public static Roof GetWeightedRoof(List<Roof> roofs, int totalWeight)
        {
            int randomWeight = Random.Range(0, totalWeight);
            foreach (Roof tile in roofs)
            {
                if (randomWeight <= tile.weight)
                    return tile;
                randomWeight -= tile.weight;
            }

            return null;
        }

        public static Wall GetWeightedWall(List<Wall> walls, int totalWeight)
        {
            int randomWeight = Random.Range(0, totalWeight);
            foreach (Wall tile in walls)
            {
                if (randomWeight <= tile.weight)
                    return tile;
                randomWeight -= tile.weight;
            }

            return null;
        }

        public static Window GetWeightedWindow(List<Window> windows, int totalWeight)
        {
            int randomWeight = Random.Range(0, totalWeight);
            foreach (Window tile in windows)
            {
                if (randomWeight <= tile.weight)
                    return tile;
                randomWeight -= tile.weight;
            }

            return null;
        }

        public static Decoration RandomDecoration(List<Decoration> objects)
        {
            if (objects.Count == 0)
                return null;
            return objects[Random.Range(0, objects.Count)];
        }


        public static List<DecoratorPoint> GetValidPoints(List<DecoratorPoint> allPoints, bool occupiedStatus,
            PointType pointType, float? minNodeHeight = null, float? maxNodeHeight = null, int? levelNumber = null,
            Bounds? bounds = null)
        {
            //Store a copy of the points list. Every time we find a point that doesn't meet the criteria, remove it as an option.
            List<DecoratorPoint> validPoints = new List<DecoratorPoint>(allPoints);

            for (int i = validPoints.Count - 1; i >= 0; i--)
            {
                DecoratorPoint point = validPoints[i];

                if (point.occupied != occupiedStatus || point.pointType != pointType)
                {
                    validPoints.Remove(point);
                }
                else if (bounds == null || minNodeHeight == null || maxNodeHeight == null || levelNumber == null)
                {
                    continue;
                }
                else if (point.point.y + bounds?.min.y < minNodeHeight ||
                         point.point.y + bounds?.min.y > maxNodeHeight || point.levelNumber != levelNumber)
                {
                    validPoints.Remove(point);
                }
            }

            return validPoints;
        }

        /// <summary>
        /// Returns a random point within the supplied list of decorator points that matches the specified criteria.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="occupiedStatus"></param>
        /// <param name="pointType"></param>
        /// <returns></returns>
        public static DecoratorPoint RandomPoint(List<DecoratorPoint> objects, bool occupiedStatus, PointType pointType,
            GameObject comparisonObject)
        {
            if (objects.Count == 0)
            {
                return null;
            }

            int currentIteration = 0;
            DecoratorPoint point = objects[Random.Range(0, objects.Count)];
            while (point.pointType != pointType && point.occupied != occupiedStatus &&
                   comparisonObject.name == point.currentDecoration.name && currentIteration < objects.Count)
            {
                point = objects[Random.Range(0, objects.Count)];
            }

            return point;
        }


        // Transform adjustments
        public static void AdjustPosition(GameObject obj, Vector3 adjustment, bool adjustForRotation = false,
            int rotationIndex = 0)
        {
            Vector3 objPos = obj.transform.position;
            objPos += obj.transform.right * adjustment.x;
            objPos += obj.transform.up * adjustment.y;
            objPos += obj.transform.forward * adjustment.z;

            obj.transform.position = objPos;
        }

        public static Vector3 AdjustedPosition(GameObject obj, Vector3 adjustment)
        {
            Vector3 adjustedPos = obj.transform.position;
            adjustedPos += obj.transform.right * adjustment.x;
            adjustedPos += obj.transform.up * adjustment.y;
            adjustedPos += obj.transform.forward * adjustment.z;
            return adjustedPos;
        }


        public static void AdjustRotation(GameObject obj, Vector3 adjustment, bool randomRotation = false,
            int rotationIndex = 0)
        {
            if (randomRotation && rotationIndex >= 0)
            {
                Vector3 center = obj.GetComponentInChildren<Renderer>().bounds.center;
                obj.transform.RotateAround(center, Vector3.up, GetDirection(rotationIndex));
            }

            Vector3 adjustedRot = obj.transform.rotation.eulerAngles;
            
            adjustedRot += obj.transform.right * adjustment.x;
            adjustedRot += obj.transform.up * adjustment.y;
            adjustedRot += obj.transform.forward * adjustment.z;
            obj.transform.rotation = Quaternion.Euler(adjustedRot);
        }

        private static float GetDirection(int index)
        {
            switch (index)
            {
                case 0: return 0;
                case 1: return 90;
                case 2: return 180;
                case 3: return 270;
            }

            return 0;
        }

    }
}