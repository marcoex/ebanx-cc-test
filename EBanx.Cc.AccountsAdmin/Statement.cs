using System;
using System.Linq;
using System.Collections.Generic;

namespace EBanx.Cc.AccountsAdmin
{
	/// <summary>
	/// Representa uma lista de eventos da Conta. 
	/// </summary>
	/// <remarks>Fiz pra demonstrar um pouco do conhecimento em OOP</remarks>
	public class Statement : List<StatementItem>
	{
		/// <summary>
		/// Items value total.
		/// </summary>
		public float Sum;

		//
		// Summary:
		//     Adds an object to the end of the System.Collections.Generic.List`1.
		//
		// Parameters:
		//   item:
		//     The object to be added to the end of the System.Collections.Generic.List`1. The
		//     value can be null for reference types.
		public new void Add(StatementItem item)
		{
			base.Add(item);
			Sum += item.Value;
		}

		//
		// Summary:
		//     Adds an object to the end of the System.Collections.Generic.List`1.
		//
		// Parameters:
		//   item:
		//     The object to be added to the end of the System.Collections.Generic.List`1. The
		//     value can be null for reference types.
		public void Add(string desc, DateTime dt, float value)
		{
			Add(new StatementItem() { DateTime = dt, Description = desc, Value = value });
		}

		public new void Clear()
		{
			base.Clear();
			Sum = 0;
		}

		//
		// Summary:
		//     Removes the first occurrence of a specific object from the System.Collections.Generic.List`1.
		//
		// Parameters:
		//   item:
		//     The object to remove from the System.Collections.Generic.List`1. The value can
		//     be null for reference types.
		//
		// Returns:
		//     true if item is successfully removed; otherwise, false. This method also returns
		//     false if item was not found in the System.Collections.Generic.List`1.
		public new bool Remove(StatementItem item)
		{
			var result = base.Remove(item);
			Sum -= item.Value;
			return result;
		}

		//
		// Summary:
		//     Removes all the elements that match the conditions defined by the specified predicate.
		//
		// Parameters:
		//   match:
		//     The System.Predicate`1 delegate that defines the conditions of the elements to
		//     remove.
		//
		// Returns:
		//     The number of elements removed from the System.Collections.Generic.List`1.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//     match is null.
		public new int RemoveAll(Predicate<StatementItem> match)
		{
			var count = 0;
			foreach (var item in base.FindAll(match)) {
				Remove(item);
				count++;
			}
			return count;
		}
		//
		// Summary:
		//     Removes the element at the specified index of the System.Collections.Generic.List`1.
		//
		// Parameters:
		//   index:
		//     The zero-based index of the element to remove.
		//
		// Exceptions:
		//   T:System.ArgumentOutOfRangeException:
		//     index is less than 0. -or- index is equal to or greater than System.Collections.Generic.List`1.Count.
		public new void RemoveAt(int index)
		{
			Remove(base[index]);
		}
		//
		// Summary:
		//     Removes a range of elements from the System.Collections.Generic.List`1.
		//
		// Parameters:
		//   index:
		//     The zero-based starting index of the range of elements to remove.
		//
		//   count:
		//     The number of elements to remove.
		//
		// Exceptions:
		//   T:System.ArgumentOutOfRangeException:
		//     index is less than 0. -or- count is less than 0.
		//
		//   T:System.ArgumentException:
		//     index and count do not denote a valid range of elements in the System.Collections.Generic.List`1.
		public new void RemoveRange(int index, int count)
		{
			foreach (var item in base.GetRange(index, count)) {
				Remove(item);
			}
		}

		//
		// Summary:
		//     Adds the elements of the specified collection to the end of the System.Collections.Generic.List`1.
		//
		// Parameters:
		//   collection:
		//     The collection whose elements should be added to the end of the System.Collections.Generic.List`1.
		//     The collection itself cannot be null, but it can contain elements that are null,
		//     if type T is a reference type.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//     collection is null.
		public new void AddRange(IEnumerable<StatementItem> collection)
		{
			//um pouco diferente para mostrar um pouco de funcional
			collection.ToList().ForEach(x => Add(x));
		}

		//
		// Summary:
		//     Sets the capacity to the actual number of elements in the System.Collections.Generic.List`1,
		//     if that number is less than a threshold value.
		public new void TrimExcess()
		{
			throw new NotImplementedException();
		}


		//
		// Summary:
		//     Inserts an element into the System.Collections.Generic.List`1 at the specified
		//     index.
		//
		// Parameters:
		//   index:
		//     The zero-based index at which item should be inserted.
		//
		//   item:
		//     The object to insert. The value can be null for reference types.
		//
		// Exceptions:
		//   T:System.ArgumentOutOfRangeException:
		//     index is less than 0. -or- index is greater than System.Collections.Generic.List`1.Count.
		public new void Insert(int index, StatementItem item)
		{
			throw new NotImplementedException();
		}

		//
		// Summary:
		//     Inserts the elements of a collection into the System.Collections.Generic.List`1
		//     at the specified index.
		//
		// Parameters:
		//   index:
		//     The zero-based index at which the new elements should be inserted.
		//
		//   collection:
		//     The collection whose elements should be inserted into the System.Collections.Generic.List`1.
		//     The collection itself cannot be null, but it can contain elements that are null,
		//     if type T is a reference type.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//     collection is null.
		//
		//   T:System.ArgumentOutOfRangeException:
		//     index is less than 0. -or- index is greater than System.Collections.Generic.List`1.Count.
		public new void InsertRange(int index, IEnumerable<StatementItem> collection)
		{
			throw new NotImplementedException();
		}

	}
}