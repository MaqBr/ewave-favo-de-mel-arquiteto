using Dapper;
using FavoDeMel.Catalogo.Data.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Data.Dapper;

namespace FavoDeMel.Catalogo.Data.Dapper.Repositorio
{
    /// <summary>
    /// Classe de repositório com métodos padrões para os demais.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        private static int CONNECTION_TIMEOUT = 900;
        /// <summary>
        /// Transação atual do repositório.
        /// </summary>
        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// Conexão com o banco de dados.
        /// </summary>
        public IDbConnection Connection { get { return Transaction.Connection; } }

        #region Construtores

        /// <summary>
        /// Construtor do repoistório.
        /// </summary>
        public RepositoryBase()
        { }

        /// <summary>
        /// Construtor do repoistório.
        /// </summary>
        /// <param name="transaction">Transação a ser utilizada pelo repositório.</param>
        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        #endregion

        #region Existe conexão

        public bool ExistConnection()
        {
            return Connection != null;
        }

        #endregion

        #region Executar SQL

        public bool ExecuteSql(string sqlComand, object parametros)
        {
            if (Debugger.IsAttached)
                Trace.WriteLine(string.Format("ExecuteSql: {0}", sqlComand));

            return Connection.Execute(sqlComand, parametros, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        #endregion

        #region Insert

        public int Insert<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
        {
            return connection.Insert(entity, Transaction, CONNECTION_TIMEOUT).Value;
        }

        public int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            return Connection.Insert(entity, Transaction, CONNECTION_TIMEOUT).Value;
        }

        public TKey Insert<TKey, TEntity>(TEntity entity) where TEntity : class
        {
            return Connection.Insert<TKey, TEntity>(entity, Transaction, CONNECTION_TIMEOUT);
        }

        #endregion

        #region Update

        public bool Update<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
        {
            return connection.Update(entity, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        public bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            return Connection.Update(entity, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        public bool Update<TEntity>(object entityProperties, object whereConditions) where TEntity : class
        {
            return Connection.UpdateList<TEntity>(entityProperties, whereConditions, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        /// <summary>
        /// Realiza o UPDATE em algumas propriedades da entidade.
        /// <para>Exemplo:</para>
        /// <para>UpdateProperties(minhaEntidade, new { Inativo = true, Descricao = "Exemplo" });</para>
        /// </summary>
        /// <typeparam name="TEntity">Entidade do banco de dados.</typeparam>
        /// <param name="entityProperties"></param>
        /// <param name="connection"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateProperties<TEntity>(TEntity entity, object entityProperties, IDbConnection connection) where TEntity : class
        {
            return connection.UpdateProperties(entity, entityProperties, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        /// <summary>
        /// Realiza o UPDATE em algumas propriedades da entidade.
        /// <para>Exemplo:</para>
        /// <para>UpdateProperties(minhaEntidade, new { Inativo = true, Descricao = "Exemplo" });</para>
        /// </summary>
        /// <typeparam name="TEntity">Entidade do banco de dados.</typeparam>
        /// <param name="entity"></param>
        /// <param name="entityProperties"></param>
        /// <returns></returns>
        public bool UpdateProperties<TEntity>(TEntity entity, object entityProperties) where TEntity : class
        {
            return Connection.UpdateProperties(entity, entityProperties, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        #endregion

        #region Delete

        public bool Delete<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
        {
            return connection.Delete(entity, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            return Connection.Delete(entity, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        public bool Delete<TEntity>(object whereConditions = null) where TEntity : class
        {
            return Connection.DeleteList<TEntity>(whereConditions, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        #endregion

        #region Consulta

        public bool Exists<TEntity>(object whereConditions = null) where TEntity : class
        {
            return Connection.RecordCount<TEntity>(whereConditions, Transaction, CONNECTION_TIMEOUT) > 0;
        }

        public TEntity Get<TEntity>(int id) where TEntity : class
        {
            return Connection.Get<TEntity>(id, Transaction, CONNECTION_TIMEOUT);
        }

        public TEntity Get<TEntity>(object whereConditions) where TEntity : class
        {
            return Connection.Get<TEntity>(whereConditions, Transaction, CONNECTION_TIMEOUT);
        }

        public TEntity GetProperties<TEntity>(int id, object properties) where TEntity : class
        {
            return Connection.GetProperties<TEntity>(id, properties, Transaction, CONNECTION_TIMEOUT);
        }

        public IEnumerable<TEntity> GetList<TEntity>(object whereConditions = null) where TEntity : class
        {
            return Connection.GetList<TEntity>(whereConditions, Transaction, CONNECTION_TIMEOUT);
        }

        public IEnumerable<TEntity> GetList<TEntity>(string sqlQuery, object parameters = null) where TEntity : class
        {
            if (Debugger.IsAttached)
                Trace.WriteLine(string.Format("GetList<{0}>: {1}", typeof(TEntity), sqlQuery));

            return Connection.Query<TEntity>(sqlQuery, parameters, Transaction, true, CONNECTION_TIMEOUT);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
