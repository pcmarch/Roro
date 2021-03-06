﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class LoopEndNode : Node
    {
        [DataMember]
        internal Guid LoopStartNodeId { get; set; }

        public LoopEndNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new NextPort());
        }

        public override Guid Execute(ActivityContext context)
        {
            return this.Ports.First().NextNodeId;
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddPolygon(new Point[]
            {
                new Point(r.X, r.Y),
                new Point(r.Right, r.Y),
                new Point(r.Right, r.Bottom - PageRenderOptions.GridSize),
                new Point(r.Right - PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X + PageRenderOptions.GridSize, r.Bottom),
                new Point(r.X, r.Bottom - PageRenderOptions.GridSize)
            });
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
