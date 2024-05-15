namespace CRM.Common.Core.Repositories;
public interface IUnitOfWork
{
  void BeginTransaction();
  void Commit();
  void Rollback();
}