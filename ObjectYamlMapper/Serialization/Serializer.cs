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
            StringWriter buffer = new();
            int nestingLevel = 0;
            Serialize(obj, buffer, nestingLevel);
            return buffer.ToString();
        }

        private string Serialize(object obj, StringWriter buffer, int nestingLevel)
        {
            Type objType = obj.GetType();
            TypeInfo objTypeInfo = objType.GetTypeInfo();

            IEnumerable<FieldInfo> fields = objTypeInfo.DeclaredFields;

            foreach (var field in fields)
            {
                string fieldName = field.Name;
                fieldName = RemoveFieldTag(fieldName);

                if (field.FieldType.IsString() || field.FieldType.IsNumeric() || field.FieldType.IsEnum)
                {
                    buffer.WriteLine("{0}{1}: {2}", AddIndentations(nestingLevel), fieldName, field.GetValue(obj));
                }

                else if (field.FieldType.IsDateTime())
                {
                    buffer.WriteLine("{0}{1}: {2}", AddIndentations(nestingLevel), fieldName, field.GetValue(obj));
                }

                else if (field.FieldType.IsGenericType)
                {
                    Type genericType = field.FieldType.GetGenericArguments()[0];
                    if (field.GetValue(obj) is not IEnumerable<object> list || list.Any() == false)
                    {
                        buffer.WriteLine("{0}{1}: []", AddIndentations(nestingLevel), fieldName);
                    }
                    else
                    {
                        buffer.WriteLine("{0}{1}:", AddIndentations(nestingLevel), fieldName);

                        if (genericType.IsString() || genericType.IsNumeric())
                        {
                            foreach (var element in list)
                            {
                                buffer.WriteLine("{0}{1}-{1}{2}", AddIndentations(nestingLevel), AddIndentations(1), element.ToString());
                            }
                        }
                        else if (genericType.IsCustomObject())
                        {
                            foreach (var element in list)
                            {
                                object tempObj = field.GetValue(obj);
                                if (tempObj != null)
                                {
                                    buffer.WriteLine("{0}{1}-{1}{2}", AddIndentations(nestingLevel), AddIndentations(1), fieldName);
                                    Serialize(tempObj, buffer, nestingLevel + 1);
                                }
                            }
                        }
                    }
                }

                else if (field.FieldType.IsCustomObject())
                {
                    object tempObj = field.GetValue(obj);
                    if (tempObj != null)
                    {
                        buffer.WriteLine("{0}{1}:", AddIndentations(nestingLevel), fieldName);
                        Serialize(tempObj, buffer, nestingLevel + 1);
                    }
                }
            }
            return buffer.ToString();
        }

        private static string RemoveFieldTag(string fieldName)
        {
            if (fieldName.StartsWith('<') && fieldName.Contains('>'))
            {
                fieldName = fieldName[(fieldName.IndexOf('<') + 1)..fieldName.IndexOf('>')];
            }
            return fieldName;
        }

        private string AddIndentations(int nestingLevel)
        {
            return new string(' ', nestingLevel * 2);
        }
    }
}
