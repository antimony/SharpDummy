namespace SharpDummy.Infrastructure.Domain.Interfaces
{
    /// <summary>
    /// ��������� �������� �������� ������
    /// </summary>
    public interface IId<T>
    {
        ///<summary>
        ///</summary>
        T Id { get; set; }
    }
}