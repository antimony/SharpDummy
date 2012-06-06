using System;

namespace SharpDummy.Infrastructure.Domain.Interfaces.UnitOfWork
{
    ///<summary>
    /// ������� ������
    ///</summary>
    public interface IUnitOfWork : IDisposable
    {
        ///<summary>
        /// ��������� ��������� � ����
        ///</summary>
        void Commit();

        ///<summary>
        /// ��������� ��������
        ///</summary>
        ///<param name="entity"></param>
        void Save(object entity);

        ///<summary>
        /// ��������� ������ ���������
        ///</summary>
        ///<param name="entity"></param>
        void SaveAll(object[] entity);

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="entity"></param>
        void Delete(object entity);

        ///<summary>
        /// ��������� ������� �� ������
        ///</summary>
        ///<returns></returns>
        void ExecuteCommand(string command);
    }
}