using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class MeshUtils
    {
	    private static Mesh _unityCapsuleMesh = null;
	    private static Mesh _unityCubeMesh = null;
	    private static Mesh _unityCylinderMesh = null;
	    private static Mesh _unityPlaneMesh = null;
	    private static Mesh _unitySphereMesh = null;
	    private static Mesh _unityQuadMesh = null;
	    
        public static Mesh GetUnityPrimitiveMesh(PrimitiveType primitiveType)
        {
	        return primitiveType switch
	        {
		        PrimitiveType.Sphere => GetCachedPrimitiveMesh(ref _unitySphereMesh, primitiveType),
		        PrimitiveType.Capsule => GetCachedPrimitiveMesh(ref _unityCapsuleMesh, primitiveType),
		        PrimitiveType.Cylinder => GetCachedPrimitiveMesh(ref _unityCylinderMesh, primitiveType),
		        PrimitiveType.Cube => GetCachedPrimitiveMesh(ref _unityCubeMesh, primitiveType),
		        PrimitiveType.Plane => GetCachedPrimitiveMesh(ref _unityPlaneMesh, primitiveType),
		        PrimitiveType.Quad => GetCachedPrimitiveMesh(ref _unityQuadMesh, primitiveType),
		        _ => throw new ArgumentOutOfRangeException(nameof(primitiveType), primitiveType, null)
	        };
        }

		private static Mesh GetCachedPrimitiveMesh(ref Mesh primMesh, PrimitiveType primitiveType)
		{
			if (primMesh == null)
			{
				Debug.Log("Getting Unity Primitive Mesh: " + primitiveType);
				primMesh = Resources.GetBuiltinResource<Mesh>(GetPrimitiveMeshPath(primitiveType));

				if (primMesh == null)
				{
					Debug.LogError("Couldn't load Unity Primitive Mesh: " + primitiveType);
				}
			}

			return primMesh;
		}

		private static string GetPrimitiveMeshPath(PrimitiveType primitiveType)
		{
			return primitiveType switch
			{
				PrimitiveType.Sphere => "New-Sphere.fbx",
				PrimitiveType.Capsule => "New-Capsule.fbx",
				PrimitiveType.Cylinder => "New-Cylinder.fbx",
				PrimitiveType.Cube => "Cube.fbx",
				PrimitiveType.Plane => "New-Plane.fbx",
				PrimitiveType.Quad => "Quad.fbx",
				_ => throw new ArgumentOutOfRangeException(nameof(primitiveType), primitiveType, null)
			};
		}
    }
}