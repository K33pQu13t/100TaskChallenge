using System;

namespace Task1
{
    //Консольная шпаргалка
    //Постановка задачи: красиво вывести информацию о типах данных(целочисленные, строки)
    //и их переменных(int, string) и других существующих в вашем языке типе данных.
    //Оформить всё аккуратно и красиво.
    class Program
    {
        static void Main(string[] args)
        {
            ShowTypeInfo("bool", typeof(bool), sizeof(bool));

            ShowTypeInfo("char", typeof(char), sizeof(char), char.MinValue.ToString(), char.MaxValue.ToString());
            ShowTypeInfo("string", typeof(string));

            ShowTypeInfo("byte", typeof(byte), sizeof(byte), byte.MinValue.ToString(), byte.MaxValue.ToString());
            ShowTypeInfo("sbyte", typeof(sbyte), sizeof(sbyte), sbyte.MinValue.ToString(), sbyte.MaxValue.ToString());
            ShowTypeInfo("short", typeof(short), sizeof(short), short.MinValue.ToString(), short.MaxValue.ToString());
            ShowTypeInfo("ushort", typeof(ushort), sizeof(ushort), ushort.MinValue.ToString(), ushort.MaxValue.ToString());
            ShowTypeInfo("int", typeof(int), sizeof(int), int.MinValue.ToString(), int.MaxValue.ToString());
            ShowTypeInfo("uint", typeof(uint), sizeof(uint), uint.MinValue.ToString(), uint.MaxValue.ToString());
            ShowTypeInfo("long", typeof(long), sizeof(long), long.MinValue.ToString(), long.MaxValue.ToString());
            ShowTypeInfo("ulong", typeof(ulong), sizeof(ulong), ulong.MinValue.ToString(), ulong.MaxValue.ToString());

            ShowTypeInfo("float", typeof(float), sizeof(float), float.MinValue.ToString(), float.MaxValue.ToString());
            ShowTypeInfo("double", typeof(double), sizeof(double), double.MinValue.ToString(), double.MaxValue.ToString());
            ShowTypeInfo("decimal", typeof(decimal), sizeof(decimal), decimal.MinValue.ToString(), decimal.MaxValue.ToString());
        }

        static void ShowTypeInfo(string typeName, Type type, int sizeOf = default, string minValue = default, string maxValue = default)
        {
            Console.Write("Тип данных: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(typeName);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"Пространство имён: {type.FullName}");
            if (sizeOf != default)
            {
                Console.WriteLine($"Сколько байт занимают переменные этого типа: {sizeOf}");
            }
            if (!string.IsNullOrEmpty(minValue) && !string.IsNullOrEmpty(maxValue))
            {
                Console.WriteLine($"Минимальное принимаемое значение: {minValue}");
                Console.WriteLine($"Максимальное принимаемое значение: {maxValue}");
            }
            Console.WriteLine("=============================================\n");
        }
    }
}
