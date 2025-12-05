using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns the nearest parent in the hierarchy with the specified tag.
        /// Returns null if no matching object is found.
        /// </summary>
        public static GameObject FindNearestParentWithTag(this GameObject obj, string tag)
        {
            Transform current = obj.transform;

            while (current != null)
            {
                if (current.CompareTag(tag))
                    return current.gameObject;

                current = current.parent;
            }

            return null;
        }
    }

}
