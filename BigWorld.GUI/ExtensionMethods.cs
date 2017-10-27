﻿using System;
using engenious;

namespace BigWorld.GUI
{
    /// <summary>
    /// Stellt Hilfsmethoden als Erweiterungsmethoden bereit.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Ermittelt die Schnittfläche zweiter Rechtecke.
        /// </summary>
        /// <param name="r1">Rechteck 1</param>
        /// <param name="r2">Rechteck 2</param>
        /// <returns>Schnittmenge</returns>
        public static Rectangle Intersection(this Rectangle r1, Rectangle r2)
        {
            // Normalisieren
            Point r11 = new Point(Math.Min(r1.Left, r1.Right), Math.Min(r1.Top, r1.Bottom));
            Point r12 = new Point(Math.Max(r1.Left, r1.Right), Math.Max(r1.Top, r1.Bottom));
            Point r21 = new Point(Math.Min(r2.Left, r2.Right), Math.Min(r2.Top, r2.Bottom));
            Point r22 = new Point(Math.Max(r2.Left, r2.Right), Math.Max(r2.Top, r2.Bottom));

            // Schnittmenge finden
            Point r31 = new Point(Math.Max(r11.X, r21.X), Math.Max(r11.Y, r21.Y));
            Point r32 = new Point(Math.Min(r12.X, r22.X), Math.Min(r12.Y, r22.Y));
            Point dimensions = new Point(Math.Max(0, r32.X - r31.X), Math.Max(0, r32.Y - r31.Y));

            // Rückgabe
            return new Rectangle(r31, dimensions);
        }

        /// <summary>
        /// Transformiert ein Rechteck mit der angegebenen Transformationsmatrix.
        /// </summary>
        /// <param name="rectangle">Rechteck</param>
        /// <param name="transform">Transformationsmatrix</param>
        /// <returns>Transformiertes Rechteck</returns>
        public static Rectangle Transform(this Rectangle rectangle, Matrix transform)
        {
            // In Punkte umwandeln
            Vector3[] p = new[] {
                new Vector3(rectangle.Left, rectangle.Top,1),
                new Vector3(rectangle.Right, rectangle.Bottom,1)
            };

            // Transformieren
            p[0] = Vector3.Transform(p[0], transform);
            p[1] = Vector3.Transform(p[1], transform);



            // Rectangle bauen
            return new Rectangle(
                (int)p[0].X,
                (int)p[0].Y,
                (int)(p[1].X - p[0].X),
                (int)(p[1].Y - p[0].Y));
        }

        /// <summary>
        /// Gibt an ob der angegebene Punkt innerhalb des Rectangles liegt oder nicht.
        /// </summary>
        /// <param name="rectangle">Zu untersuchendes Rechteck</param>
        /// <param name="point">Punkt</param>
        /// <returns>Liegt drin oder nicht?</returns>
        public static bool Intersects(this Rectangle rectangle, Point point)
        {
            return point.X >= rectangle.Left && point.X < rectangle.Right &&
                point.Y >= rectangle.Top && point.Y < rectangle.Bottom;
        }
    }
}