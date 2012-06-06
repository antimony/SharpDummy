using System.Collections.Generic;
using System.Linq;

namespace SharpDummy.Infrastructure.Domain.Interfaces
{
    ///<summary>
    /// ��������� ������������ ����������� �� �������� ������
    ///</summary>
    public interface IQueryBuilder
    {
        IRepository<TResult> For<TResult>() ;
    }

    ///<summary>
    /// ��������� �����������
    ///</summary>
    ///<typeparam name="TEntity">��� �������� �������� ������</typeparam>
    public interface IRepository<TEntity>
    {
        ///<summary>
        /// �������� �������� �� ��������������. � ���� ������� ������������� Load ����� ���������������.
        ///</summary>
        ///<param name="id"></param>
        ///<returns>�������� � ��������� Id, ���� ����������. ����� - null.</returns>
        TEntity ById(int id);

        /// <summary>
        /// �������� �������� �� ��������������. ������� ���������� lazy-loading
        /// ��������� ������-������, ������� ������ ������������, ���� ���������� ������ ��������� reference � �������,
        /// ��� ��� ��� �� ������ �� ����� select-������ � ����.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>�������� � ��������� Id, ���� ����������. ����� - ������� ����������.</returns>
        TEntity Load(int id);

        /// <summary>
        /// �������� ��� ��������
        /// </summary>
        /// <param name="entity"></param>
        IEnumerable<TEntity> All();

        IQueryable<TEntity> Query();
    }
}