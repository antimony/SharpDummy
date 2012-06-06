namespace SharpDummy.Infrastructure.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сущности доменной модели
    /// </summary>
    public interface IId<T>
    {
        ///<summary>
        ///</summary>
        T Id { get; set; }
    }
}