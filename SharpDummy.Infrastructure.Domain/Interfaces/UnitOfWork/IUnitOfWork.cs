using System;

namespace SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork
{
    ///<summary>
    /// Единица работы
    ///</summary>
    public interface IUnitOfWork : IDisposable
    {
        ///<summary>
        /// Сохранить изменения в базу
        ///</summary>
        void Commit();

        ///<summary>
        /// Сохранить сущность
        ///</summary>
        ///<param name="entity"></param>
        void Save(object entity);

        ///<summary>
        /// Сохранить список сущностей
        ///</summary>
        ///<param name="entity"></param>
        void SaveAll(object[] entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity"></param>
        void Delete(object entity);

        ///<summary>
        /// Исполняет команду из строки
        ///</summary>
        ///<returns></returns>
        void ExecuteCommand(string command);
    }
}