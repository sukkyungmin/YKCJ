using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace EyeControl
{
  /// <summary>
  /// The Sphere Class
  /// </summary>
  /// <remarks>
  /// This control is created by Niel M. Thomas in 2010.
  /// This control is covered by the CPOL license.
  /// </remarks>
  public class Sphere
  {
    #region -- Properties --
    // Four public properties allow access to private fields.
    /// <summary>
    /// Gets or sets the slices.
    /// </summary>
    /// <value>The slices.</value>
    public int Slices { get; set; }

    /// <summary>
    /// Gets or sets the stacks.
    /// </summary>
    /// <value>The stacks.</value>
    public int Stacks { get; set; }

    /// <summary>
    /// Gets or sets the center.
    /// </summary>
    /// <value>The center.</value>
    public Point3D Center { get; set; }

    /// <summary>
    /// Gets or sets the radius.
    /// </summary>
    /// <value>The radius.</value>
    public double Radius { get; set; }

    /// <summary>
    /// Gets the geometry.
    /// </summary>
    /// <value>The geometry.</value>
    public MeshGeometry3D Geometry
    {
      get
      {
        // Create a MeshGeometry3D.
        var mesh = new MeshGeometry3D();
        // Fill the vertices, normals, and textures collections.
        for (int stack = 0; stack <= Stacks; stack++)
        {
          double phi = Math.PI / 2 - stack * Math.PI / Stacks;
          double y = Radius * Math.Sin(phi);
          double scale = -Radius * Math.Cos(phi);
          for (int slice = 0; slice <= Slices; slice++)
          {
            double theta = slice * 2 * Math.PI / Slices;
            double x = scale * Math.Sin(theta);
            double z = scale * Math.Cos(theta);
            var normal = new Vector3D(x, y, z);
            mesh.Normals.Add(normal);
            mesh.Positions.Add(normal + Center);
            mesh.TextureCoordinates.Add(
                        new Point((double)slice / Slices,
                                  (double)stack / Stacks));
          }
        }
        // Fill the indices collection.
        for (var stack = 0; stack < Stacks; stack++)
        {
          int top = (stack + 0) * (Slices + 1);
          int bot = (stack + 1) * (Slices + 1);
          for (int slice = 0; slice < Slices; slice++)
          {
            if (stack != 0)
            {
              mesh.TriangleIndices.Add(top + slice);
              mesh.TriangleIndices.Add(bot + slice);
              mesh.TriangleIndices.Add(top + slice + 1);
            }
            if (stack != Stacks - 1)
            {
              mesh.TriangleIndices.Add(top + slice + 1);
              mesh.TriangleIndices.Add(bot + slice);
              mesh.TriangleIndices.Add(bot + slice + 1);
            }
          }
        }
        return mesh;
      }
    }
    #endregion

    #region -- Constructor --
    /// <summary>
    /// Initializes a new instance of the <see cref="Sphere"/> class.
    /// </summary>
    public Sphere()
    {
      Radius = 1;
      Stacks = 16;
      Slices = 32;
      Center = new Point3D();
    }
    #endregion



  }
}
