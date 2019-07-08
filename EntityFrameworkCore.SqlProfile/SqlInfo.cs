using System;

namespace EntityFrameworkCore.SqlProfile
{
    public class SqlInfo
    {
        public int Index { get; set; }

        public string ExcuteTime { get; set; }

        public DateTime RequestTime { get; set; }

        public SqlTypeEnum Type { get; set; }

        public string Sql { get; set; }
    }
}