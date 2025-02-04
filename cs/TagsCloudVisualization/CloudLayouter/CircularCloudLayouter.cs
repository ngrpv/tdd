﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.PointGenerator;

namespace TagsCloudVisualization.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<RectangleF> tagCloud;
        private readonly IPointGenerator pointGenerator;
        public RectangleF CloudRectangle { get; private set; }
        public SizeF SizeF => new SizeF(CloudRectangle.Width, CloudRectangle.Height);
        public PointF Center => pointGenerator.Center;

        public CircularCloudLayouter(IPointGenerator pointGenerator)
        {
            tagCloud = new List<RectangleF>();
            this.pointGenerator = pointGenerator;
        }

        public RectangleF PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Size parameters should be positive");
            var tag = GetNextRectangle(rectangleSize);
            tagCloud.Add(tag);
            UpdateCloudBorders(tag);
            return tag;
        }

        public RectangleF[] GetCloud() => tagCloud.ToArray();

        private void UpdateCloudBorders(RectangleF newTag)
        {
            CloudRectangle = RectangleF.Union(CloudRectangle, newTag);
        }

        private RectangleF GetNextRectangle(Size size)
        {
            var points = pointGenerator.GetPoints(size);
            var rectangles = points.Select(p => p.GetRectangle(size));
            var suitableRectangle = rectangles.First(r => !IsIntersectWithCloud(r));
            return suitableRectangle;
        }

        private bool IsIntersectWithCloud(RectangleF newTag) => tagCloud.Any(tag => tag.IntersectsWith(newTag));
    }
}