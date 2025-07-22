using System.Collections.Generic;

namespace Game.GameplayHandling
{
    public class CropComparer : IEqualityComparer<Crop>
    {
        public bool Equals(Crop x, Crop y)
        {
            if(x == null || y == null)
            {
                return false;
            }
            return x.Equals(y);
        }

        public int GetHashCode(Crop obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }
}
