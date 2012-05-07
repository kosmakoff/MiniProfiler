using System.Data;
using System.Data.Common;
using NHibernate.Driver;

namespace StackExchange.Profiling.NHibernate.Driver
{
    public class ProfiledSqlClientDriver : SqlClientDriver
    {
        public override System.Data.IDbCommand CreateCommand()
        {
            IDbCommand command = base.CreateCommand();

            if (MiniProfiler.Current != null)
                command = new ProfiledSqlCommand((DbCommand)command, MiniProfiler.Current);

            return command;
        }
    }
}
