using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class MeshUtils
    {
	    private static Mesh _unityCapsuleMesh;
	    private static Mesh _unityCubeMesh;
	    private static Mesh _unityCylinderMesh;
	    private static Mesh _unityPlaneMesh;
	    private static Mesh _unitySphereMesh;
	    private static Mesh _unityQuadMesh;
	    
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
				primMesh = Resources.GetBuiltinResource<Mesh>(GetPrimitiveMeshPath(primitiveType));
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