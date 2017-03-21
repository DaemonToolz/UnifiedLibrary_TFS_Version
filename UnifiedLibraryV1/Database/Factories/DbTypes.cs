using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Database.Factories {
    enum DbTypes {
        [Description("MySQL")]
        MySql = 0,
        [Description("TranscriptSQL")]
        TSql,
        [Description("OracleSQL")]
        OSql,
        [Description("MongoDB")]
        MongoDb,
        [Description("RedisDB")]
        Redis

    }
}
