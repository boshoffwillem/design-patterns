using DomainModel.Models;
using DomainModel.Models.Media;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DomainModel;

public class Gateway
{
    private const string CHECKOUT_SQL = "INSERT INTO CHECKOUT VALUES(@mediaId, @dueDate);";
    private const string FIND_MEDIA_SQL = "SELECT * FROM MEDIA LEFT JOIN CHECKOUT ON MEDIA.ID = CHECKOUT.MEDIAID WHERE id=@mediaId;";
    private const string CONNECTION_STRING = "Server=DYN-SA-LAP-59\\SQLEXPRESS;Database=TestDB;Trusted_Connection=true";
    private readonly MediaFactory _mediaFactory;

    public Gateway()
    {
        _mediaFactory = new();
    }

    public void InsertCheckout(int mediaId, DateTime dateTime)
    {
        throw new NotImplementedException();
    }


    public MediaBase FindMedia(int mediaId)
    {
        using var sqlConnection = new SqlConnection(CONNECTION_STRING);
        sqlConnection.Open();
        var sqlCommand = sqlConnection.CreateCommand();
        sqlCommand.CommandText = FIND_MEDIA_SQL;
        sqlCommand.Parameters.AddWithValue("@mediaId", mediaId);
        var data = new DataTable();

        using var sqlAdapter = new SqlDataAdapter(sqlCommand);
        sqlAdapter.Fill(data);
        var rawMedia = data.Rows;
        var media = _mediaFactory.CreateMedia((MediaType)rawMedia["Type"]);
        media.Id = mediaId;
        media.Name = (string)rawMedia["Name"];
        return new MediaBase();
    }
}

