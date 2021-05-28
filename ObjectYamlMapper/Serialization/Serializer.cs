using ObjectYamlMapper.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ObjectYamlMapper.Serialization
{
    public class Serializer : ISerializer
    {
        public string Serialize(object obj)
        {
            Type objType = obj.GetType();
            TypeInfo objTypeInfo = objType.GetTypeInfo();

            IEnumerable<FieldInfo> fields = objTypeInfo.DeclaredFields;

            StringWriter buffer = new StringWriter();

            Console.WriteLine("*** Serializing object \"" + objType.Name + "\"...");

            foreach (var field in fields)
            {
                string fieldName = field.Name.ToLower();
                fieldName = RemoveFieldTag(fieldName);

                if (field.FieldType.IsString())
                {
                    buffer.WriteLine("{0}: \"{1}\"", fieldName, field.GetValue(obj));
                }
                else if (field.FieldType.IsNumeric())
                {
                    buffer.WriteLine("{0}: {1}", fieldName, field.GetValue(obj));
                }

                else if (field.FieldType.IsGenericType)
                {
                    Type genericType = field.FieldType.GetGenericArguments()[0];
                    IEnumerable<object> list = field.GetValue(obj) as IEnumerable<object>;
                    if (list == null || list.Any() == false)
                    {
                        buffer.WriteLine("{0}: []");
                    }
                    else if (genericType.IsString())
                    {
                        buffer.WriteLine("{0}:", fieldName);
                        foreach (var element in list)
                        {
                            buffer.WriteLine(" - {0}", element.ToString());
                        }
                    }
                    else if (genericType.IsNumeric())
                    {
                        buffer.WriteLine("{0}:", fieldName);
                        foreach (var element in list)
                        {
                            buffer.WriteLine(" - {0}", element.ToString());
                        }
                    }
                }

                else if (field.FieldType.IsCustomObject())
                {
                    object tempObj = field.GetValue(obj);
                    if (tempObj == null)
                    {
                        buffer.WriteLine("{0}: {}");
                    }
                    else
                    {
                        // Serialize(tempObj);
                    }
                }
            }
            return buffer.ToString();
        }

        private string RemoveFieldTag(string fieldName)
        {
            if (fieldName.StartsWith('<') && fieldName.Contains('>'))
            {
                fieldName = fieldName.Substring(fieldName.IndexOf('<') + 1, fieldName.IndexOf('>') - (fieldName.IndexOf('<') + 1));
            }
            return fieldName;
        }
    }
}
