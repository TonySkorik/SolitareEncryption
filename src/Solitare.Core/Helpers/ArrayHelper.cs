using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Solitare.Core.Helpers
{
	public static class ArrayHelper
	{
		public static void Move<T>(this T[] sourceArray, int fromIndex, int toIndex)
		{
			if (fromIndex == toIndex)
			{
				return;
			}

			if (fromIndex > toIndex)
			{
				sourceArray.MoveUp(fromIndex, toIndex);
			}
			else
			{
				//fromIndex < toIndex
				sourceArray.MoveDown(fromIndex, toIndex);				
			}
		}

		private static void MoveDown<T>(this T[] sourceArray, int fromIndex, int toIndex)
		{
			// here we assume that fromIndex > toINdex
			var tempArray = ArrayPool<T>.Shared.Rent(sourceArray.Length);

			var elementToMove = sourceArray[fromIndex];

			var sliceBeforeElement = sourceArray[0..fromIndex];
			var sliceAfterElement = sourceArray[(fromIndex + 1)..(toIndex + 1)];
			var sliceAfterInsert = sourceArray[(toIndex + 1)..];

			Array.Copy(sliceBeforeElement, 0, tempArray, 0, sliceBeforeElement.Length);

			Array.Copy(
				sliceAfterElement,
				0,
				tempArray,
				sliceBeforeElement.Length,
				sliceAfterElement.Length);

			tempArray[sliceBeforeElement.Length + sliceAfterElement.Length] = elementToMove;

			Array.Copy(sliceAfterInsert, 0, tempArray, sliceBeforeElement.Length + sliceAfterElement.Length + 1, sliceAfterInsert.Length);

			Array.Copy(tempArray, sourceArray, sourceArray.Length);

			ArrayPool<T>.Shared.Return(tempArray, true);
		}

		private static void MoveUp<T>(this T[] sourceArray, int fromIndex, int toIndex)
		{
			// here we assum that fromIndex > toINdex
			var tempArray = ArrayPool<T>.Shared.Rent(sourceArray.Length);

			var elementToMove = sourceArray[fromIndex];

			var sliceBeforeInsert = sourceArray[0..toIndex];
			var sliceAfterInsert = sourceArray[toIndex..fromIndex];
			var sliceAfterElement = sourceArray[(fromIndex + 1)..];

			Array.Copy(sliceBeforeInsert, 0, tempArray, 0, sliceBeforeInsert.Length);

			tempArray[sliceBeforeInsert.Length] = elementToMove;

			Array.Copy(
				sliceAfterInsert,
				0,
				tempArray,
				sliceBeforeInsert.Length + 1 /*for inserted element*/,
				sliceAfterInsert.Length);

			Array.Copy(
				sliceAfterElement,
				0,
				tempArray,
				sliceBeforeInsert.Length + 1 /*for inserted element*/ + sliceAfterInsert.Length,
				sliceAfterElement.Length);

			Array.Copy(tempArray, sourceArray, sourceArray.Length);

			ArrayPool<T>.Shared.Return(tempArray, true);
		}

		public static void SwapParts<T>(
			this T[] sourceArray,
			int firstPartStartIndex,
			int firstPartEndIndex,
			int secondPartStartIndex,
			int secondPartEndIndex)
		{
			var tempArray = ArrayPool<T>.Shared.Rent(sourceArray.Length);

			var firstPart = sourceArray[firstPartStartIndex..(firstPartEndIndex + 1)];
			var secondPart = sourceArray[secondPartStartIndex..(secondPartEndIndex + 1)];
			var middle = sourceArray[(firstPartEndIndex + 1)..secondPartStartIndex];

			Array.Copy(secondPart, 0, tempArray, 0, secondPart.Length);
			Array.Copy(middle, 0, tempArray, secondPart.Length, middle.Length);
			Array.Copy(firstPart, 0, tempArray, secondPart.Length + middle.Length, firstPart.Length);

			Array.Copy(tempArray, sourceArray, sourceArray.Length);

			ArrayPool<T>.Shared.Return(tempArray, true);
		}
	}
}
