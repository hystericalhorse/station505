﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtensionMethods
{
	public static void RecursiveSetActive(this GameObject obj, bool state)
	{
		foreach (Transform child in obj.transform)
		{
			child.gameObject.RecursiveSetActive(state);
		}

		obj.SetActive(state);
	}

	public static T GetRandom<T>(this T[] array)
	{
		var i = Helpers.random.Next(0, (array.Length - 1));
		return array[i];
	}

	public static T GetRandom<T>(this List<T> array)
	{
		var i = Helpers.random.Next(0, array.Count);
		return array[i];
	}

	public static Vector2 UnNormalize(this Vector2 vec2)
	{
		Vector2 _ = new(0, 0);

		if (vec2.x != 0)
			_.x = (vec2.x > 0) ? 1 : -1;

		if (vec2.y != 0)
			_.y = (vec2.y > 0) ? 1 : -1;

		return _;
	}

	public enum Axis { Horizontal, Vertical }
	/// <param name="favoriteAxis">Which axis to favor if the x and y values are equal</param>
	/// <returns>A Vector2 snapped to a cardinal direction</returns>
	public static Vector2 Cardinalize(this Vector2 vec2, Axis favoriteAxis = Axis.Vertical)
	{
		Vector2 _ = new(0, 0);

		switch (favoriteAxis)
		{
			default:
			case Axis.Vertical:
				if (Mathf.Abs(vec2.x) > Mathf.Abs(vec2.y))
					_ = new(vec2.UnNormalize().x, 0);
				else
					_ = new(0, vec2.UnNormalize().y);
				break;
			case Axis.Horizontal:
				if (Mathf.Abs(vec2.x) >= Mathf.Abs(vec2.y))
					_ = new(vec2.UnNormalize().x, 0);
				else
					_ = new(0, vec2.UnNormalize().y);
				break;
		}

		return _;

	}

	public static Vector2 Clamp(this Vector2 vec2, Vector2 min, Vector2 max)
	{
		if (vec2.x > max.x)
			vec2.x = max.x;
		if (vec2.x < min.x)
			vec2.x = min.x;
		if (vec2.y > max.y)
			vec2.y = max.y;
		if (vec2.y < min.y)
			vec2.y = min.y;

		return vec2;
	}

	public static Vector3 Clamp(this Vector3 vec3, Vector3 min, Vector3 max)
	{
		if (vec3.x > max.x)
			vec3.x = max.x;
		if (vec3.x < min.x)
			vec3.x = min.x;
		if (vec3.y > max.y)
			vec3.y = max.y;
		if (vec3.y < min.y)
			vec3.y = min.y;
		if (vec3.z > max.z)
			vec3.z = max.z;
		if (vec3.z < min.z)
			vec3.z = min.z;

		return vec3;
	}

}