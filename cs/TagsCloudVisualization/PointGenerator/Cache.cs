﻿using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.PointGenerator
{
    public class Cache
    {
        private readonly Dictionary<Size, float> sizeToCircleParameter = new Dictionary<Size, float>();

        public float SafeGetParameter(Size size)
        {
            if (!sizeToCircleParameter.ContainsKey(size))
                sizeToCircleParameter[size] = 0;
            return sizeToCircleParameter[size];
        }

        public void UpdateParameter(Size size, float radius)
        {
            sizeToCircleParameter[size] = radius;
        }
    }
}