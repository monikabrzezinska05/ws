using Dapper;
using Npgsql;

namespace ws.Infrastructure;

public class Repository
{
    private NpgsqlDataSource _dataSource;
    
    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public DataModel CreateMessages(DataModel dataModel)
    {
        var sql = $@"INSERT INTO main.messages " +
                  "(username, message, roomid) " +
                  "VALUES (@username, @message, @roomid)" + "RETURNING *";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<DataModel>(sql, new {dataModel.message, dataModel.username, dataModel.roomid});
        }
    }
    
    public IEnumerable<DataModel> GetMessages(int roomid)
    {
        string sql = @$"
            SELECT message, username  
            FROM main.messages 
            WHERE roomid=@roomid;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<DataModel>(sql, new { roomid });
        }
    }
}