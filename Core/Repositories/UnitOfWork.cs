﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories;
public class UnitOfWork : IUnitOfWork
{
  private readonly NpgsqlConnection connection;
  private NpgsqlTransaction transaction;

  public UnitOfWork(NpgsqlConnection connection)
  {
    this.connection = connection;
  }

  public void BeginTransaction()
  {
    connection.Open();
    transaction = connection.BeginTransaction();
  }

  public void Commit()
  {
    transaction?.Commit();
    connection?.Close();
  }

  public void Rollback()
  {
    transaction?.Rollback();
    connection?.Close();
  }

  public void Dispose()
  {
    transaction?.Dispose();
    connection?.Close();
  }
}
