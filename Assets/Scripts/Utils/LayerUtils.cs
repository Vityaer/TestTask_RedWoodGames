using UnityEngine;

namespace Utils
{
    public class LayerUtils
    {
        public static bool CheckIntersection(GameObject target, int layerMask)
        {
            return ((1 << target.layer) & layerMask) > 0;
        }
    }
}
