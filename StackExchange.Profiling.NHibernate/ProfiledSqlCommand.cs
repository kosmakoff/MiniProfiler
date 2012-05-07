using System.Data.Common;
using System.Data.SqlClient;
using StackExchange.Profiling.Data;

namespace StackExchange.Profiling.NHibernate
{
    public class ProfiledSqlCommand : ProfiledDbCommand
    {
        private readonly SqlCommand _sqlCommand;

        public ProfiledSqlCommand(DbCommand command, IDbProfiler profiler)
            : base(command, null, profiler)
        {
            _sqlCommand = (SqlCommand)command;
        }

        public SqlCommand SqlCommand
        {
            get { return _sqlCommand; }
        }
    }
}
