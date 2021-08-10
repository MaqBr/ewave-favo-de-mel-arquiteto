using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Data.Dapper.Connection
{
    /// <summary>
    /// Auxilia no gerencimento de estado do objeto de conexão.
    /// </summary>
    public class ConnectionStateManager : IDisposable
    {
        #region Atributos

        private int _defaultCommandTimeout = 30;
        IDbConnection _connection;
        private IDbTransaction _transaction;

        #endregion Atributos

        #region Propriedades

        /// <summary>
        /// Retorna o valor padrão como timeout para execuções de instruções SQL.
        /// </summary>
        public int DefaultCommandTimeout
        {
            get { return _defaultCommandTimeout; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(DefaultCommandTimeout), value, "O valor não pode negativo;");
                _defaultCommandTimeout = value;
            }

        }

        /// <summary>
        /// Retorna o estado inicial da conexão no momento da criação de <see cref="ConnectionStateManager{TConnection}"/>.
        /// </summary>
        public ConnectionState InitialConnectionState { get; }

        #endregion Propriedades

        /// <summary>
        /// Inicializa a classe com base no objeto connection informado.
        /// </summary>
        /// <param name="connection"></param>
        public ConnectionStateManager(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            InitialConnectionState = connection.State;
            OpenConnectionIfClosed();
        }

        #region Métodos privados

        private void OpenConnectionIfClosed()
        {
            if (InitialConnectionState == ConnectionState.Closed)
                _connection.Open();
        }

        private void EnsureNotDisposed()
        {
            if (_connection == null) throw new InvalidOperationException("O objeto já foi liberado (Disposed).");
        }

        #endregion Métodos privados

        #region Métodos publicos

        /// <summary>
        /// Inicia uma nova transação.
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            EnsureNotDisposed();

            if (_connection.State != ConnectionState.Open)
                throw new InvalidOperationException("A Conexão precisa estar aberta para iniciar uma transação.");

            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        /// <summary>
        /// Inicia uma nova transação, com um determinado <see cref="IsolationLevel"/>.
        /// </summary>
        /// <param name="il">Nível de isolamento da transação.</param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            EnsureNotDisposed();

            if (_connection.State != ConnectionState.Open)
                throw new InvalidOperationException("A Conexão precisa estar aberta para iniciar uma transação.");

            _transaction = _connection.BeginTransaction(il);
            return _transaction;
        }

        /// <summary>
        /// Desfaz as operações realizadas desde o início da transação.
        /// </summary>
        public void Rollback()
        {
            EnsureNotDisposed();

            if (_transaction == null)
                throw new InvalidOperationException("Não há uma transação ativa.");

            _transaction.Rollback();
            _transaction = null;
        }

        /// <summary>
        /// Confirma todas as alterações realizadas desde a abertura da transação.
        /// </summary>
        public void Commit()
        {
            EnsureNotDisposed();

            if (_transaction == null)
                return;

            _transaction.Commit();
            _transaction = null;
        }

        /// <summary>
        /// Fecha a conexão e libera os recursos.
        /// </summary>
        /// <remarks>Não fechará a conexão caso a mesma já encontrava-se aberta no momento da criação do <see cref="ConnectionStateManager"/>.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null && _connection.State == ConnectionState.Open && InitialConnectionState == ConnectionState.Closed)
                {
                    _transaction?.Rollback();
                    _connection.Dispose();
                }

                _connection = null;
                _transaction = null;
            }
        }

        #endregion Métodos publicos

        #region Métodos auxiliares da conexão

        public Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return _connection.ExecuteAsync(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryAsync<T>(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {            
            return _connection.Query<T>(sql, param, _transaction, true, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return _connection.QueryMultipleAsync(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return _connection.QuerySingleOrDefaultAsync<T>(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return _connection.QuerySingleOrDefault<T>(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        public Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return _connection.QuerySingleAsync<T>(sql, param, _transaction, commandTimeout ?? DefaultCommandTimeout, commandType);
        }

        #endregion Métodos auxiliares da conexão
    }
}
