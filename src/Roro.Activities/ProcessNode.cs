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
    public sealed class ProcessNode : Node
    {
        public ProcessNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new NextPort());
        }

        public override Guid Execute(ActivityContext context)
        {
            var activity = Activity.CreateInstance(this.ActivityId) as ProcessNodeActivity;
            activity.Inputs = this.ActivityInputs;
            activity.Outputs = this.ActivityOutputs;
            activity.Execute(context);
            return this.Ports.First().NextNodeId;
        }

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddRectangle(r);
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
