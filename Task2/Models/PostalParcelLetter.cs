using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task2.Interfaces;

namespace Task2.Models
{
    class PostalParcelLetter : IBox
    {
        public int Weight 
        { 
            get 
            {
                int sum = _weigth;
                foreach(var parcel in BoxInsideList)
                {
                    sum += parcel.Weight;
                }
                return sum;
            }
            private set 
            {
                _weigth = value;
            }
        }
        private int _weigth;
        private int _maxWeight = 200;

        public string IndexAddressFrom { get; private set; }
        public string IndexAddressTo { get; private set; }
        public string IndexAddressToCurrent { get; set; }
        /// <summary>
        /// regex для проверки валидности индекса
        /// </summary>
        private readonly Regex _regexIndex = new Regex(@"^\d{6}$");

        public List<IBox> BoxInsideList { get; private set; }
        public IBox BoxParent { get; set; }

        public bool IsOnTheWay { get; private set; }
        public bool IsGivenToRecipient { get; private set; }

        public List<string> TrackingList { get; set; }

        /// <summary>
        /// Указанный индекс не существует
        /// </summary>
        readonly private Exception _exceptionWrongIndex = new Exception("Ошибка: Индекс должен быть в формате 000000");
        /// <summary>
        /// Письмо превышает 200г, используйте бандероль для отправления
        /// </summary>
        readonly private Exception _exceptionLetterIsTooWeight = new Exception("Ошибка: Письмо превышает 200г, используйте бандероль для отправления");
        /// <summary>
        /// Сперва нужно подписать письмо
        /// </summary>
        readonly private Exception _exceptionLetterMustBeSigned = new Exception("Ошибка: Сперва нужно подписать письмо");
        /// <summary>
        /// Посылка уже выдана получателю
        /// </summary>
        readonly private Exception _exceptionParcelIsGiven = new Exception("Ошибка: Посылка уже выдана получателю");
        /// <summary>
        /// Посылку можно выдать только в пункте назначения
        /// </summary>
        readonly private Exception _exceptionCantGiveAtNotFinal = new Exception("Ошибка: Посылку можно выдать только в пункте назначения");
        /// <summary>
        /// Посылку можно выдать только в пункте назначения
        /// </summary>
        readonly private Exception _exceptionCantSendFromFinalPoint = new Exception("Ошибка: Посылку нельзя отправить так как она уже дошла до назначенного отделения");
        /// <summary>
        /// Посылка ещё не дошла до почтового отделения
        /// </summary>
        readonly private Exception _exceptionParcelAlreadySended = new Exception("Ошибка: Посылка ещё не дошла до почтового отделения");

        public PostalParcelLetter(string indexAddressFrom, string indexAddressTo, int weight)
        {
            if (_regexIndex.IsMatch(indexAddressFrom))
                IndexAddressFrom = indexAddressFrom;
            else
                throw _exceptionWrongIndex;

            if (_regexIndex.IsMatch(indexAddressTo))
                IndexAddressTo = indexAddressTo;
            else
                throw _exceptionWrongIndex;

            if(weight < _maxWeight)
                Weight = weight;
            else
                throw _exceptionLetterIsTooWeight;

            TrackingList = new List<string>();
            BoxInsideList = new List<IBox>();
        }

        public void PackBoxInside(int weight)
        {
            CheckIsGivenToRecipient();
            if (Weight + weight < _maxWeight)
            {
                BoxInsideList.Add(new PostalParcelLetter(IndexAddressFrom, IndexAddressTo, weight));
            }
            else
                throw _exceptionLetterIsTooWeight;

        }


        public void Send(string index)
        {
            if (_regexIndex.IsMatch(index))
            {
                CheckIsGivenToRecipient();
                CheckIsOnTheWay();
                if ((TrackingList.Count > 0 && TrackingList.Last() != IndexAddressTo) ||
                    TrackingList.Count == 0)
                {
                    IsOnTheWay = true;
                    IndexAddressToCurrent = index;
                }
                else
                    throw _exceptionCantSendFromFinalPoint;
            }
            else 
                throw _exceptionWrongIndex;
        }

        public void ReachedThePoint()
        {
            CheckIsGivenToRecipient();
            TrackingList.Add(IndexAddressToCurrent);
            IndexAddressToCurrent = string.Empty;
            IsOnTheWay = false;
        }

        public void Give()
        {
            CheckIsOnTheWay();
            CheckIsGivenToRecipient();
            if (TrackingList.Last() == IndexAddressTo)
            {
                IndexAddressToCurrent = string.Empty;
                IsGivenToRecipient = true;
            }
            else
                throw _exceptionCantGiveAtNotFinal;
        }

        private void CheckIsGivenToRecipient()
        {
            if (IsGivenToRecipient)
                throw _exceptionParcelIsGiven;
        }
        private void CheckIsOnTheWay()
        {
            if (IsOnTheWay)
                throw _exceptionParcelAlreadySended;
        }

        public override string ToString()
        {
            string str = $"Письмо, из {IndexAddressFrom} в {IndexAddressTo}, {Weight} грамм(а)." +
                $"{(BoxInsideList.Count > 0 ? $"\nВ письме {BoxInsideList.Count} вложений." : "")}";

            if (IsOnTheWay)
                str += $"\nСейчас письмо на пути в {IndexAddressToCurrent}.";
            else if (IsGivenToRecipient)
                str += $"\nПисьмо выдано адресату в пункте назначения.";

            return str;
        }
    }
}
