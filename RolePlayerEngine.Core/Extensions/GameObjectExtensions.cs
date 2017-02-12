using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RolePlayerEngine.Core.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject GetChildGameObject(this GameObject fromObject, string name)
        {
            var child = fromObject.transform.FindChild(name);
            return child != null 
                ? child.gameObject 
                : fromObject.GetChildren().FirstOrDefault(x => x.gameObject.name == name);
        }

        public static IEnumerable<GameObject> GetChildren(this GameObject fromObject)
        {
            IList<GameObject> list = new List<GameObject>();
            for (int index = 0; index < fromObject.transform.childCount; ++index)
                list.Add(fromObject.transform.GetChild(index).gameObject);

            return list.ToArray();
        }

        public static IEnumerable<GameObject> GetChildrenWithComponent<T>(this GameObject fromObject)
        {
            IList<GameObject> list = new List<GameObject>();
            for (int index = 0; index < fromObject.transform.childCount; ++index)
            {
                var child = fromObject.transform.GetChild(index).gameObject;
                if (child.GetComponent<T>() != null)
                    list.Add(child);
            }

            return list.ToArray();
        }

        public static bool HasComponent<T>(this GameObject fromObject)
        {
            return fromObject.GetComponent<T>() != null;
        }

        public static void AttachToObject(this GameObject fromObject, GameObject toObject, Quaternion rotation, Vector3 position)
        {
            var transform = fromObject.transform;

            // Reset
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            transform.SetParent(toObject.transform);
            transform.position = toObject.transform.position;
            transform.localRotation = rotation;
            transform.localPosition = position;
        }
    }
}