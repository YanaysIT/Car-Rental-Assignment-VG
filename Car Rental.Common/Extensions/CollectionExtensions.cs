using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace Car_Rental.Common.Extensions;

public static class CollectionExtensions
{
    public static List<T> GetFieldOfType<T>(this List<T> list, Object collection)
    {
        FieldInfo[] fieldInfoCollection = collection.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        var field = fieldInfoCollection.FirstOrDefault(f => f.FieldType.Equals(typeof(List<T>))) ?? 
            throw new InvalidOperationException("No fields matching the requested type found");

        var fieldValue = field.GetValue(collection) ?? throw new InvalidDataException();
        return (List<T>)fieldValue;
    }
    public static void AddItemToCollection<T>(this Object collection,T item)
    {
        var list = new List<T>();
        var fieldValue = GetFieldOfType<T>(list, collection);
        fieldValue.Add(item);
    }
}