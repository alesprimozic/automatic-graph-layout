/*
Microsoft Automatic Graph Layout,MSAGL 

Copyright (c) Microsoft Corporation

All rights reserved. 

MIT License 

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Msagl.Core.Geometry;
using Point = Microsoft.Msagl.Core.Geometry.Point;
using Rectangle = Microsoft.Msagl.Core.Geometry.Rectangle;

namespace xCodeMap.xGraphControl {
    internal class CommonX {
        internal static System.Windows.Point WpfPoint(Microsoft.Msagl.Core.Geometry.Point p)
        {
            return new System.Windows.Point(p.X,p.Y);
        }

        static public Brush BrushFromMsaglColor(Microsoft.Msagl.Drawing.Color color) {

            Color avalonColor = new Color();
            avalonColor.A = color.A;
            avalonColor.B = color.B;
            avalonColor.G = color.G;
            avalonColor.R = color.R;
            return new SolidColorBrush(avalonColor);
        }

        internal static void PositionElement(FrameworkElement fe, Point origin, double scale)
        {
            fe.RenderTransform = new MatrixTransform(new Matrix(scale, 0, 0, -scale, origin.X, origin.Y));
            Panel.SetZIndex(fe, 1);
        }

        internal static void PositionElement(FrameworkElement fe, Rectangle object_box, Rectangle bounding_box, double scale)
        {
            Microsoft.Msagl.Core.Geometry.Point origin = bounding_box.Center;
            double desiredH = object_box.Height * scale - fe.Margin.Top - fe.Margin.Bottom,
                   desiredW = object_box.Width * scale + fe.Margin.Left + fe.Margin.Right;

            if (bounding_box.Height < desiredH)
            {
                origin.Y += object_box.Center.Y * scale - fe.Margin.Top;
            }
            else
            {
                if (fe.VerticalAlignment == VerticalAlignment.Top)
                {
                    origin.Y = bounding_box.Top;
                }
                else if (fe.VerticalAlignment == VerticalAlignment.Bottom)
                {
                    origin.Y -= bounding_box.Height / 2 - object_box.Height * scale;
                }
                else
                {
                    origin.Y += object_box.Center.Y * scale;
                }
            }
            if (bounding_box.Width < desiredW)
            {
                origin.X -= object_box.Center.X * scale;
            }
            else
            {
                if (fe.HorizontalAlignment == HorizontalAlignment.Left)
                {
                    origin.X -= bounding_box.Width / 2 - fe.Margin.Left;
                }
                else if (fe.HorizontalAlignment == HorizontalAlignment.Right)
                {
                    origin.X -= bounding_box.Width / 2 - object_box.Width * scale;
                }
                else
                {
                    origin.X -= object_box.Center.X * scale;
                }
            }
            PositionElement(fe, origin, scale);
        }

        internal static Size Measure(FrameworkElement fe)
        {
            fe.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            return fe.DesiredSize;
        }
    }
}