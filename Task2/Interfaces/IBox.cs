using System.Collections.Generic;

namespace Task2.Interfaces
{
    interface IBox
    {
        /// <summary>
        /// вес посылки в граммах
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// адрес отправления
        /// </summary>
        public string IndexAddressFrom { get; }

        /// <summary>
        /// конечный адрес получения
        /// </summary>
        public string IndexAddressTo { get; }

        /// <summary>
        /// промежуточный адрес направления
        /// </summary>
        public string IndexAddressToCurrent { get; set; }

        /// <summary>
        /// Вложенные посылки
        /// </summary>
        public List<IBox> BoxInsideList { get; }

        /// <summary>
        /// посылка, в которую вложена эта посылка
        /// </summary>
        public IBox BoxParent { get; set; }

        /// <summary>
        /// true если посылка в пути
        /// </summary>
        public bool IsOnTheWay { get; }

        /// <summary>
        /// true если посылка выдана получателю
        /// </summary>
        public bool IsGivenToRecipient { get; }

        /// <summary>
        /// список перемещений посылки
        /// </summary>
        public List<string> TrackingList { get; set; }

        /// <summary>
        /// запаковать внутрь посылку
        /// </summary>
        /// <param name="box"></param>
        public void PackBoxInside(int weight);

        /// <summary>
        /// отправить посылку в следующее отделение
        /// </summary>
        public void Send(string index);

        /// <summary>
        /// посылка дошла до пункта
        /// </summary>
        public void ReachedThePoint();

        /// <summary>
        /// выдать посылку получателю
        /// </summary>
        public void Give();
    }
}
