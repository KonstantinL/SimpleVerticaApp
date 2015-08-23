using System;
using System.Collections.Generic;
using System.Data;
using SimpleVerticaApp.Models.Repository.RepVertica;
using Vertica.Data.VerticaClient;

namespace SimpleVerticaApp.Models.Repository
{
    public static class Db
    {
        /// <summary>
        /// Тип текущей базы данных
        /// </summary>
        public static DbType CurrDbType { get; set; }
        /// <summary>
        /// Текущее подключение к БД
        /// </summary>
        public static IDbConnection Connection { get; set; }

        /// <summary>
        /// Перечислитель возможных СУБД
        /// </summary>
        public enum DbType
        {
            Vertica, Oracle, SqlServer
        }


        /// <summary>
        /// Подключение к СУБД
        /// </summary>
        /// <param name="pCredentials"></param>
        public static void ConnectDb(DbCredentials pCredentials)
        {
            switch (Db.CurrDbType)
            {
                case DbType.Vertica:
                    Connection = DbVertica.ConnectDb(pCredentials);
                    break;
                default: throw new NotImplementedException("Нет реализации для данного типа СУБД");
            
            }
        }

        /// <summary>
        /// Запрос всех данных из таблицы SampleTable
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SampleTable> SelectSampleTable()
        {
            switch (Db.CurrDbType)
            {
                case DbType.Vertica:
                    return DbVertica.SelectSampleTable(Connection as VerticaConnection);
                    
                default: throw new NotImplementedException("Нет реализации для данного типа СУБД");

            }
        }

        /// <summary>
        /// Добавление строки в таблицу SampleTable
        /// </summary>
        /// <param name="pData">Объект SampleTable для добавления в таблицу</param>
        /// <returns></returns>
        public static int InsertSampleTable(SampleTable pData)
        {

            switch (Db.CurrDbType)
            {
                case DbType.Vertica:
                    return DbVertica.InsertSampleTable(pData, Connection as VerticaConnection);

                default: throw new NotImplementedException("Нет реализации для данного типа СУБД");

            }
        }

        /// <summary>
        /// Обновление строки в таблице SampleTable. Поле ID модифицировать нельзя.
        /// </summary>
        /// <param name="pData">Объект SampleTable для обновления в таблице</param>
        /// <returns></returns>
        public static int UpdateSampleTable(SampleTable pData)
        {

            switch (Db.CurrDbType)
            {
                case DbType.Vertica:
                    return DbVertica.UpdateSampleTable(pData, Connection as VerticaConnection);

                default: throw new NotImplementedException("Нет реализации для данного типа СУБД");

            }
        }

        /// <summary>
        /// Удаление строки из таблицы SampleTable.
        /// </summary>
        /// <param name="pData">Объект SampleTable, который нужно удалить</param>
        /// <returns></returns>
        public static int DeleteSampleTableById(SampleTable pData)
        {

            switch (Db.CurrDbType)
            {
                case DbType.Vertica:
                    return DbVertica.DeleteSampleTableById(pData, Connection as VerticaConnection);

                default: throw new NotImplementedException("Нет реализации для данного типа СУБД");

            }
        }


        #region CheckConnection
        /// <summary>
        /// Прроверка наличия подключения к текущей базе
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection()
        {
            return CheckConnection(Db.Connection);
        }

        /// <summary>
        /// Проверка наличия подключения к указанной базе
        /// </summary>
        /// <param name="pConnection">Подключение к БД для проверки</param>
        /// <returns></returns>
        public static bool CheckConnection(IDbConnection pConnection)
        {
            //var  = Connection;

            if (pConnection == null) return false;
            return pConnection.State == ConnectionState.Open;
        }
        #endregion
    }
}
