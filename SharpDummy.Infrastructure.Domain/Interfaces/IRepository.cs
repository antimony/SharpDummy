using System.Collections.Generic;
using System.Linq;

namespace SharpDummy.Infrastructure.Domain.Interfaces
{
    ///<summary>
    /// Интерфейс возвращающий репозиторий по доменной модели
    ///</summary>
    public interface IQueryBuilder
    {
        IRepository<TResult> For<TResult>() ;
    }

    ///<summary>
    /// Интерфейс репозитория
    ///</summary>
    ///<typeparam name="TEntity">Тип сущности доменной модели</typeparam>
    public interface IRepository<TEntity>
    {
        ///<summary>
        /// Получить сущность по идентификатору. В ряде случаев использование Load более предпочтительно.
        ///</summary>
        ///<param name="id"></param>
        ///<returns>Сущность с указанным Id, если существует. Иначе - null.</returns>
        TEntity ById(int id);

        /// <summary>
        /// Получить сущность по идентификатору. Активно использует lazy-loading
        /// Создается объект-прокси, который удобно использовать, если необходимо только выставить reference у объекта,
        /// так как это не влечет за собой select-запрос к базе.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Сущность с указанным Id, если существует. Иначе - бросает исключение.</returns>
        TEntity Load(int id);

        /// <summary>
        /// Получить все сущности
        /// </summary>
        /// <param name="entity"></param>
        IEnumerable<TEntity> All();

        IQueryable<TEntity> Query();
    }
}