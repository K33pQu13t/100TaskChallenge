using System;
using Task2.Models;

namespace Task2
{
    //Консольный перехват ошибки
    //Постановка задачи: написать программу, в которой может случиться 5 случаев,
    //в случае которых может возникнуть ошибка.
    //Научиться эту ошибку перехватывать и не давать программе выключаться
    //(оповещать об ошибке, но не давать выключаться программе, продолжая её работу).

    class Program
    {
        static void Main(string[] args)
        {
            PostalParcelLetter letter;

            Console.WriteLine("Попробуем создать письмо с некорректным адресом отправления:");
            try
            {
                letter = new PostalParcelLetter("002874", "index:072124", 2);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nПопробуем создать письмо большого веса:");
            try
            {
                letter = new PostalParcelLetter("002874", "072124", 2028);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nТакже если вложить в письмо другое письмо, и сумма их весов будет превышать 200, получится та же ошибка.\n" +
                "Создадим валидное письмо:");
            letter = new PostalParcelLetter("002874", "072124", 3);
            Console.WriteLine($"#####################\n{letter}\n#####################\n");
            Console.WriteLine("Вложим в него другое, весом 198 грамм:");
            try
            {
                letter.PackBoxInside(198);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Вложим в письмо валидное вложение весом 2 грамма:");
            letter.PackBoxInside(2);
            Console.WriteLine($"#####################\n{letter}\n#####################\n");
            Console.WriteLine("Стоит обратить внимание что вес письма увеличился.");

            Console.WriteLine("\nОтправим в следующее отделение:");
            letter.Send("112233");
            Console.WriteLine($"#####################\n{letter}\n#####################\n");
            Console.WriteLine("Сейчас система считает письмо отправленным. Попробуем его отправить ещё куда-нибудь, " +
                "не указывая что оно дошло в предидущее отделение:");
            try
            {
                letter.Send("072124");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nУкажем что письмо дошло до почтового отделения. Посмотрим путь отправлений:");
            letter.ReachedThePoint();
            letter.TrackingList.ForEach(tracking => Console.WriteLine(tracking));
            Console.WriteLine("Письмо ещё не в отделении назначения. Попробуем выдать это письмо:");
            try
            {
                letter.Give();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nНаконец, отправим письмо до пункта назначения.");
            letter.Send(letter.IndexAddressTo);
            Console.WriteLine($"#####################\n{letter}\n#####################\n");
            Console.WriteLine("Примим письмо в отделении и выдадим адресату:");
            letter.ReachedThePoint();
            letter.Give();
            Console.WriteLine($"#####################\n{letter}\n#####################\n");
            Console.WriteLine("Как видно, письмо выдано. Попробуем отправить его куда-то ещё:");
            try
            {
                letter.Send(letter.IndexAddressFrom);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
