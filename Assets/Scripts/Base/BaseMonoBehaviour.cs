using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    void OnDestroy() 
    {
        FieldInfo[] info = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in info)
        {
            Type fieldType = field.FieldType;

            // Type이 리스트인지 확인?
            if (typeof(IList).IsAssignableFrom(fieldType))
            {
                IList list = field.GetValue(this) as IList;
                if(list != null)
                {
                    list.Clear();
                }
            }

            if (typeof(IDictionary).IsAssignableFrom(fieldType))
            {
                IDictionary dictionary = field.GetValue(this) as IDictionary;
                if (dictionary != null)
                {
                    dictionary.Clear();
                }
            }

            if (!fieldType.IsPrimitive)
            {
                field.SetValue(this, null);
            }
        }
    }
}
